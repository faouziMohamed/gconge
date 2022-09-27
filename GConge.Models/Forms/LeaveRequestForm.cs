using System.ComponentModel.DataAnnotations;
using GConge.Models.Models;
using GConge.Models.Validator;

namespace GConge.Models.Forms;

public sealed record AddNewLeaveRequest
{
  [Required]
  public string LeaveType { get; set; } = LeaveTypes.VacationLeave;

  [Required]
  [DateRange("01-01-2023", ErrorMessage = "La date doit être en la date d'aujourd'hui et 01-01-2023")]
  [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
  public DateTime StartDate { get; set; } = DateTime.Now;

  [Required]
  [DateRange("01-01-2023", ErrorMessage = "La date doit être en la date d'aujourd'hui et 01-01-2023")]
  [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
  public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);

  public string? EmployeeId { get; set; }
}
