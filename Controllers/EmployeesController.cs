using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        
        // PATCH: api/Employees/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            var time_hr = new DateTimeOffset(DateTime.Now, new TimeSpan(2, 0, 0));
            employee.CreationDate = existingEmployee.CreationDate; 
            employee.LastUpdateDate = time_hr;

            _context.Entry(existingEmployee).CurrentValues.SetValues(employee);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }





        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            var time_hr = new DateTimeOffset(DateTime.Now, new TimeSpan(2, 0, 0));
            employee.CreationDate = time_hr;
            employee.LastUpdateDate = time_hr;

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }




        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }


                [HttpPost("{employeeId}/start")]
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







        [HttpPost("{employeeId}/stop")]
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
    }
}
