using System.Reflection;
using GConge.Models.DTOs.Auth;
using GConge.Models.DTOs.Employees;
using GConge.Models.DTOs.LeaveRequest;
using GConge.Models.Forms;
using GConge.Models.Models;
using GConge.Web.Client.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GConge.Web.Client.Shared.Components;

public class NewLeaveRequestBase : ComponentBase
{

  protected readonly AddNewLeaveRequest AddNewLeave = new();
  protected readonly List<string> LeaveTypesValues = new();
  private IJSObjectReference? module;
  [Parameter] public bool IsModification { get; set; }
  [Parameter] public string ModalId { get; set; }
  [Parameter] public string ModalTitle { get; set; }
  [Inject] public IEmployeeService EmployeesService { get; set; } = null!;
  [Inject] public IEmployeeLocalStorageService EmployeesLocalStorageService { get; set; } = null!;

  [Parameter]
  public CreateLeaveRequestDto Leave { get; set; }

  [Parameter]
  public EventCallback<CreateLeaveRequestDto> LeaveChanged { get; set; }

  [Parameter] public EventCallback OnSubmit { get; set; }
  protected bool IsLoading { get; set; }
  protected string? ErrorMessage { get; set; }

  [Parameter]
  public UserDto ConnectedUser { get; set; }

  [Parameter] public string SubmitBtnText { get; set; } = "Ok";

  // inject js runtime
  [Inject]
  public IJSRuntime JsRuntime { get; set; }

  protected List<EmployeeDto> EmployeesList { get; set; } = new();


  protected async override Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      // import the './Js/utils.js' module
      module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./Js/utils.js");
    }
  }

  protected async Task OnSave()
  {
    await OnValidSubmit();

    if (ErrorMessage == null)
    {
      if (module is not null)
      {
        // call the function CloseModal from the imported module
        await module.InvokeVoidAsync("CloseModal", ModalId);
      }
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
      Leave.EndDate = AddNewLeave.EndDate;
      Leave.StartDate = AddNewLeave.StartDate;
      Leave.LeaveType = AddNewLeave.LeaveType;
      await LeaveChanged.InvokeAsync(Leave);
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
}
