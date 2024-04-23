using Microsoft.EntityFrameworkCore;

 namespace EmployeeManagement.Models;

   public class EmployeeContext : DbContext
{
    public EmployeeContext(DbContextOptions options) : base(options) { }
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<TimeRecord> TimeRecords { get; set; }
}
