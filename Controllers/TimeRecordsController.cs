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
            var time_hr = new DateTimeOffset(DateTime.Now, new TimeSpan(2, 0, 0));
            var timeRecord = new TimeRecord
            {
                EmployeeId = employeeId,
                StartTime = time_hr,
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

            var endTime = new DateTimeOffset(DateTime.Now, new TimeSpan(2, 0, 0));
            activeTimeRecord.EndTime = endTime;

            // Calculate hours worked directly in the controller
            var timeSpan = endTime - activeTimeRecord.StartTime;
            activeTimeRecord.Hours = timeSpan.TotalHours;
            activeTimeRecord.Date = activeTimeRecord.StartTime.ToString("yyyy-MM-dd"); // Format date as YYYY-MM-DD

            _context.Entry(activeTimeRecord).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/TimeRecords/employee/{employeeId}/from/{startDate}/to/{endDate}
        [HttpGet("employee/{employeeId}/from/{startDate}/to/{endDate}")]
        public async Task<ActionResult<Dictionary<int, double>>> GetWorkingHoursForEmployee(int employeeId, DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Start date cannot be after end date.");
            }

            var timeRecords = await _context.TimeRecords.ToListAsync();

            var employeeHours = timeRecords.Where(tr => tr.EmployeeId == employeeId && 
                                                        tr.StartTime.Date >= startDate.Date && 
                                                        tr.StartTime.Date <= endDate.Date)
                                        .GroupBy(tr => tr.EmployeeId)
                                        .Select(group => new { EmployeeId = group.Key, TotalHours = group.Sum(t => t.Hours) })
                                        .ToDictionary(x => x.EmployeeId, x => x.TotalHours);

            if (!employeeHours.Any())
            {
                return NotFound("No time records found for the specified employee and date range.");
            }

            return Ok(employeeHours);
        }



        // GET: api/TimeRecords/all/from/{startDate}/to/{endDate}
        [HttpGet("all/from/{startDate}/to/{endDate}")]
        public async Task<ActionResult<IEnumerable<TimeRecord>>> GetWorkingHoursForAllEmployees(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Start date cannot be after end date.");
            }

            var timeRecords = await _context.TimeRecords.ToListAsync();

            var filteredRecords = timeRecords.Where(tr => tr.StartTime.Date >= startDate.Date && 
                                                        tr.StartTime.Date <= endDate.Date)
                                            .OrderByDescending(tr => tr.Hours);

            if (!filteredRecords.Any())
            {
                return NotFound("No time records found for the specified date range.");
            }

            return Ok(filteredRecords);
        }

    }
}
