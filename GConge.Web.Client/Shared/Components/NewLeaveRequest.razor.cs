using System.ComponentModel.DataAnnotations;
using System.Reflection;
using GConge.Models.DTOs.Auth;
using GConge.Models.DTOs.Employees;
using GConge.Models.DTOs.LeaveRequest;
using GConge.Models.Models;
using GConge.Models.Validator;
using GConge.Web.Client.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GConge.Web.Client.Shared.Components;

public class NewLeaveRequestBase : ComponentBase, IAsyncDisposable

{
  protected readonly AddNewLeaveRequest AddNewLeave = new();
  protected readonly List<string> LeaveTypesValues = new();
  private IJSObjectReference? module;
  [Inject] protected IEmployeeService EmployeesService { get; set; } = null!;
  [Inject] protected IEmployeeLocalStorageService EmployeesLocalStorageService { get; set; } = null!;

  [Parameter]
  public CreateLeaveRequestDto AddedLeave { get; set; }

  [Parameter]
  public EventCallback<CreateLeaveRequestDto> AddedLeaveChanged { get; set; }

  [Parameter] public EventCallback OnSubmit { get; set; }
  protected bool IsLoading { get; set; }
  protected string? ErrorMessage { get; set; }

  [Parameter]
  public UserDto ConnectedUser { get; set; }

  // inject js runtime
  [Inject]
  private IJSRuntime JsRuntime { get; set; }

  public List<EmployeeDto> EmployeesList { get; set; } = new();

  public async ValueTask DisposeAsync()
  {
    if (module is not null)
    {
      await module.DisposeAsync();
    }
  }

  protected async override Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      // import the './Js/NewLeaveRequest.razor.js' module
      module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./Js/NewLeaveRequest.js");
    }
  }
  protected async Task OnSave()
  {
    await OnValidSubmit();

    if (ErrorMessage == null && module != null)
    {
      // call the function CloseModal from the imported module
      await module.InvokeVoidAsync("CloseModal");
    }
  }
  protected async override Task OnInitializedAsync()
  {
    var leavesTypes = new LeaveTypes();
    FieldInfo[] fields = leavesTypes.GetType().GetFields(BindingFlags.Static | BindingFlags.Public);

    foreach (var item in fields)
    {
      LeaveTypesValues.Add(item.GetValue(null)!.ToString()!);
    }

    if (ConnectedUser.Role == UserRole.Admin)
    {
      await EmployeesLocalStorageService.ClearEmployees();
      EmployeesList = (await EmployeesService.GetEmployees())
        .Where(emp => emp.EmployeeId != ConnectedUser.EmployeeId).ToList();

      await EmployeesLocalStorageService.SaveEmployees(EmployeesList);
    }
  }

  protected async Task OnValidSubmit()
  {
    IsLoading = true;

    try
    {
      AddedLeave.EndDate = AddNewLeave.EndDate;
      AddedLeave.StartDate = AddNewLeave.StartDate;
      AddedLeave.LeaveType = AddNewLeave.LeaveType;
      await AddedLeaveChanged.InvokeAsync(AddedLeave);
      await OnSubmit.InvokeAsync();
    }
    catch (Exception e)
    {
      ErrorMessage = e.Message;
    }
    finally
    {
      IsLoading = false;
    }
  }

  protected sealed record AddNewLeaveRequest
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
}
