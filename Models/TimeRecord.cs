using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System;

namespace EmployeeManagement.Models
{
  public class TimeRecord
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    [Required]
    public DateTimeOffset StartTime { get; set; }

    public DateTimeOffset? EndTime { get; set; } 

    [JsonIgnore]
    public Employee? Employee { get; set; }
    [Required]

    public double Hours { get; set; }
    

    public string? Date { get; set; }
  }
}