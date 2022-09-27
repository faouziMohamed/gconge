using GConge.Models.DTOs.Auth;
using GConge.Models.DTOs.LeaveRequest;
using GConge.Models.Models;
using GConge.Web.Client.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GConge.Web.Client.Shared.Components;

public class DisplayLeaveRequestStatusBase : ComponentBase, IAsyncDisposable
{

  protected string ModalId = $"modalChangeStatus-{Guid.NewGuid():N}";

  protected string ModalTitle;

  protected IJSObjectReference? module;

  [Parameter]
  public LeaveRequestDto Row { get; set; }

  [Parameter]
  public EventCallback<LeaveRequestDto> RowChanged { get; set; }

  [Parameter]
  public UserDto CurrentUser { get; set; }

  [Inject]
  public IJSRuntime JsRuntime { get; set; }

  protected string? ErrorMessage { get; set; }
  [Inject] public ILeaveRequestService LeaveService { get; set; }
  protected bool IsLoading { get; set; }

  protected string ModalText { get; set; }
  protected string ModalIcon { get; set; }

  protected string ModalTextColor { get; set; }
  protected Action OnSubmit { get; set; }

  [Parameter]
  public EventCallback<LeaveRequestDto> OnApproveRequest { get; set; }

  [Parameter]
  public EventCallback<LeaveRequestDto> OnRejectRequest { get; set; }

  public async ValueTask DisposeAsync()
  {
    if (module is not null)
    {
      await module.DisposeAsync();
    }
  }

  protected void OnRejectPrompt()
  {
    ModalTitle = "Rejet de la demande";
    ModalText = "Vous êtes sur le point de rejeter cette demande. Voulez-vous continuer ?";
    ModalIcon = "fas fa-times-circle";
    ModalTextColor = "text-danger";

    async void RejectAction()
    {
      IsLoading = true;

      try
      {
        Row.Status = LeaveRequestStatus.Rejected;
        await LeaveService.RejectLeaveRequest(Row.LeaveRequestId);
        await RowChanged.InvokeAsync(Row);

        await CloseModal(ModalId);
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

    OnSubmit = RejectAction;
  }


  protected void OnApprovePrompt()
  {
    ModalTitle = "Cofirmation de la demande";
    ModalText = "Vous êtes sur le point d'approuver cette demande. Voulez-vous continuer ?";
    ModalIcon = "fas fa-check-circle";
    ModalTextColor = "text-success";

    async void ApproveAction()
    {
      IsLoading = true;

      try
      {
        Row.Status = LeaveRequestStatus.Approved;
        await LeaveService.ApproveLeaveRequest(Row.LeaveRequestId);
        await RowChanged.InvokeAsync(Row);
        await CloseModal(ModalId);
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

    OnSubmit = ApproveAction;
  }

  protected async Task OnCancelPrompt()
  {
    ModalTitle = "Annuler la demande de congés";
    ModalText = $"Êtes-vous sûr de vouloir annuler cette demande de congés initié le {Row.DateRequested:dd/MM/yy} ?";
    ModalIcon = "fa fa-exclamation-triangle";
    ModalTextColor = "text-danger";

    async void CancelAction()
    {
      IsLoading = true;

      try
      {
        Row.Status = LeaveRequestStatus.Cancelled;
        await LeaveService.CancelLeaveRequest(Row.LeaveRequestId, CurrentUser.EmployeeId);
        await RowChanged.InvokeAsync(Row);
        await CloseModal(ModalId);
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

    OnSubmit = CancelAction;
  }
  protected async Task CancelLeaveRequest()
  {
    IsLoading = true;

    try
    {
      Row.Status = LeaveRequestStatus.Cancelled;
      await LeaveService.CancelLeaveRequest(Row.LeaveRequestId, CurrentUser.EmployeeId);
      await RowChanged.InvokeAsync(Row);
      await CloseModal(ModalId);

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

  protected async override Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./Js/utils.js");
    }
  }
  public async Task CloseModal(string id)
  {
    if (module != null)
    {
      // call the function CloseModal from the imported module
      await module.InvokeVoidAsync("CloseModal", id);
    }
  }

  protected sealed record ColIcon(string Color, string Icon);
}
