using System.ComponentModel.DataAnnotations;

namespace GConge.Models.Validator;

/// <summary>
///   This class is used to validate the date of birth of the employee
/// </summary>
/// <remarks>
///   [DateRange("01/01/1900", "01/01/2010", ErrorMessage = "Date of birth must be between 01/01/1900 and 01/01/2010")]
///   public DateTime DateOfBirth { get; set; }
/// </remarks>
public class DateRangeAttribute : RangeAttribute
{
  public DateRangeAttribute(string maximum) :
    base(type: typeof(DateTime), minimum: DateTime.Now.ToString("d"), maximum)
  {
  }
}
