using System.ComponentModel.DataAnnotations;
using GConge.Models.Models;
using GConge.Models.Validator;

namespace GConge.Models.DTOs.LeaveRequest;

public sealed record CreateLeaveRequestDto
{
  [Required] [Range(1, int.MaxValue)] public int RequestingEmployeeId { get; set; }

  [Required] [DateRange("01-01-2023", ErrorMessage = "La date doit être en la date d'aujourd'hui et 01-01-2023")]
  [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
  public DateTime StartDate { get; set; } = DateTime.Now;

  [Required] public DateTime EndDate { get; set; } = DateTime.Now;
  [Required] public string LeaveType { get; set; } = LeaveTypes.VacationLeave;
}
