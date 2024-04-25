using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }
        
        [Required]
        public string? LastName { get; set; }
        
        public DateTimeOffset LastUpdateDate { get; set; }

        [JsonIgnore]
        public DateTimeOffset CreationDate { get; set; }
    }
}
