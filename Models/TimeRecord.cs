using System;

namespace EmployeeManagement.Models
{
    public class TimeRecord
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public Employee? Employee { get; set; }
        public double Hours { get; set; }
        public string? Date { get; set; }

    }
}
