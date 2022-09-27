using GConge.Models.DTOs.Auth;
using GConge.Models.DTOs.LeaveRequest;
using GConge.Web.Client.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GConge.Web.Client.Shared.Components;
#pragma warning disable CS8618

public class DisplayRowOfLeaveRequestBase : ComponentBase, IAsyncDisposable
{

  protected string ModalId = $"updateLeaveRequestModal{Guid.NewGuid():N}";

  protected IJSObjectReference? module;

  [Parameter]
  public LeaveRequestDto Row { get; set; }

  [Parameter]
  public UserDto CurrentUser { get; set; }

  [Inject]
  public IJSRuntime JsRuntime { get; set; }

  [Inject]
  public ILeaveRequestService LeaveRequestService { get; set; }

  [Inject]
  public ILeaveRequestLocalStorageService LocalStorageService { get; set; }

  protected CreateLeaveRequestDto LeaveReqToModify { get; set; }

  [Parameter]
  public EventCallback<LeaveRequestDto> OnDelete { get; set; }

  [Parameter]
  public EventCallback<LeaveRequestDto> OnUpdate { get; set; }

  protected string ErrorMessage { get; set; }

  protected bool IsLoading { get; set; }

  async ValueTask IAsyncDisposable.DisposeAsync()
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
      module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./Js/utils.js");
    }
  }
  public async Task CloseModal()
  {
    if (module != null)
    {
      // call the function CloseModal from the imported module
      await module.InvokeVoidAsync("CloseModal", ModalId);
    }
  }

  protected async Task OnSubmit()
  {
    IsLoading = true;

    try
    {
      Row.StartDate = LeaveReqToModify.StartDate;
      Row.EndDate = LeaveReqToModify.EndDate;
      Row.LeaveType = LeaveReqToModify.LeaveType;
      await OnUpdate.InvokeAsync(Row);
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

  protected async Task OnDeleteRequest()
  {
    IsLoading = true;

    try
    {
      await OnDelete.InvokeAsync(Row);
      await CloseModal();

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
