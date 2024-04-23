using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeRecordsController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public TimeRecordsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/TimeRecords/{id}
        [HttpGet("{id}", Name = "GetTimeRecordById")]
        public async Task<ActionResult<TimeRecord>> GetTimeRecordById(int id)
        {
            var timeRecord = await _context.TimeRecords.FindAsync(id);
            if (timeRecord == null)
            {
                return NotFound();
            }
            return Ok(timeRecord);
        }

        [HttpPost("start")]
        public async Task<ActionResult<TimeRecord>> StartTimeTracking( int employeeId)
        {
            var employeeExists = await _context.Employees.AnyAsync(e => e.Id == employeeId);
            if (!employeeExists)
            {
                return NotFound($"No employee found with ID {employeeId}");
            }

            // Check if the employee already has an active time record
            var activeTimeRecord = await _context.TimeRecords.FirstOrDefaultAsync(tr => tr.EmployeeId == employeeId && tr.EndTime == null);
            if (activeTimeRecord != null)
            {
                return BadRequest($"Time tracking is already active for employee with ID {employeeId}");
            }

            var timeRecord = new TimeRecord
            {
                EmployeeId = employeeId,
                StartTime = DateTime.Now,
                EndTime = null
            };

            _context.TimeRecords.Add(timeRecord);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetTimeRecordById", new { id = timeRecord.Id }, timeRecord);
        }

        [HttpPut("stop")]
        public async Task<IActionResult> EndTimeTracking(int employeeId)
        {
            var activeTimeRecord = await _context.TimeRecords.FirstOrDefaultAsync(tr => tr.EmployeeId == employeeId && tr.EndTime == null);
            if (activeTimeRecord == null)
            {
                return BadRequest($"No active time tracking found for employee with ID {employeeId}");
            }

            activeTimeRecord.EndTime = DateTime.Now;
            _context.Entry(activeTimeRecord).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
