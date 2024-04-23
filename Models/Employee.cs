using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization; // Add this namespace

namespace EmployeeManagement.Models
{
    public class Employee
    {
         [Required]
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }
        
        [Required]
        public string? LastName { get; set; }
        
        public DateTime LastUpdateDate { get; set; }

        [JsonIgnore] 
        public DateTime CreationDate { get; set; }
    }
}
