namespace GConge.Models.Models;

public record LeaveTypes
{
  public const string AnnualLeave = "Congé annuel";
  public const string SickLeave = "Congé maladie";
  public const string MaternityLeave = "Congé maternité";
  public const string PaternityLeave = "Congé paternité";
  public const string ParentalLeave = "Congé parental";
  public const string UnpaidLeave = "Congé sans solde";
  public const string VacationLeave = "Congé vacances";
  public const string OtherLeave = "Autre congé";
}
