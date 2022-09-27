using Blazored.LocalStorage;
using GConge.Models.DTOs.Auth;
using GConge.Models.DTOs.LeaveRequest;
using GConge.Models.Models;
using GConge.Web.Client.Services.Contracts;
using Microsoft.AspNetCore.Components;

#pragma warning disable CS8618

namespace GConge.Web.Client.Pages;

public class LeaveRequestBase : ComponentBase

{
  [Inject] protected IUserLocalStorageService UserLocalStorage { get; set; }
  [Inject] protected IUserLocalStorageService EmployeeLocalStorage { get; set; }
  [Inject] protected IEmployeeService EmployeeService { get; set; }
  [Inject] protected NavigationManager Router { get; set; }
  [Inject] protected ILeaveRequestService LeaveRequestService { get; set; }
  [Inject] protected ILeaveRequestLocalStorageService LeaveRequestLocalStorage { get; set; }
  protected UserDto? ConnectedUser { get; set; }
  protected string? ErrorMessage { get; set; }
  protected bool IsLoading { get; set; }
  protected List<LeaveRequestDto> LeaveRequests { get; set; } = new();
  protected CreateLeaveRequestDto NewAddedLeaveRequest { get; set; } = new();

  [Inject]
  public ILocalStorageService LocalStorageService { get; set; }

  protected async Task Disconnect()
  {
    string uri = Router.ToBaseRelativePath(Router.Uri);
    await LocalStorageService.ClearAsync();
    var redirectQuery = $"?redirectTo=/{uri}";
    Router.NavigateTo(uri: $"/login{(uri == "" ? "" : redirectQuery)}", true);
  }

  protected async override Task OnInitializedAsync()
  {
    IsLoading = true;

    try
    {
      string uri = Router.ToBaseRelativePath(Router.Uri);
      bool isConnected = await UserLocalStorage.AssertUserIsLoggedInOrRedirectToLogin($"/{uri}");

      if (!isConnected)
      {
        await Disconnect();
        return;
      }

      await LeaveRequestLocalStorage.ClearLocalStorage();
      ConnectedUser = await UserLocalStorage.GetUserFromLocalStorage();

      LeaveRequests = ConnectedUser?.Role switch
      {
        UserRole.Admin => await LeaveRequestService.GetLeaveRequests(),
        _ => await LeaveRequestService.GetLeaveRequestsByEmployeeId(ConnectedUser!.EmployeeId)
      };

      await LeaveRequestLocalStorage.SaveToLocalStorage(LeaveRequests);
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
  protected async Task OnSubmit()
  {
    NewAddedLeaveRequest.RequestingEmployeeId = ConnectedUser!.EmployeeId;
    var added = await LeaveRequestService.CreateLeaveRequest(NewAddedLeaveRequest);
    // update the list of leave requests in the local storage and the ui
    LeaveRequests.Add(added);
    await LeaveRequestLocalStorage.UpdateLocalStorage(LeaveRequests);
    NewAddedLeaveRequest = new CreateLeaveRequestDto();
  }
}
