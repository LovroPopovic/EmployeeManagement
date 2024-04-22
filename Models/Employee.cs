using Microsoft.EntityFrameworkCore; 

namespace EmployeeManagement.Models 
{
    public class Employee
    {
          public int Id { get; set; }
          public string? Name { get; set; }
          public string? LastName { get; set; }
         
    }
}
