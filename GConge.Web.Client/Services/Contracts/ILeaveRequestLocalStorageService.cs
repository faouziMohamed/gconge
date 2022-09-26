using Blazored.LocalStorage;
using GConge.Models.DTOs.LeaveRequest;

namespace GConge.Web.Client.Services.Contracts;

public interface ILeaveRequestLocalStorageService
{
  Task<List<LeaveRequestDto>> GetFromLocalStorage();
  Task<List<LeaveRequestDto>> SaveToLocalStorage(LeaveRequestDto leaveRequest);
  Task<List<LeaveRequestDto>> UpdateLocalStorage(LeaveRequestDto leaveRequest);
  Task<List<LeaveRequestDto>> DeleteFromLocalStorage(LeaveRequestDto leaveRequest);
  Task SaveToLocalStorage(List<LeaveRequestDto> leaveRequest);
  Task UpdateLocalStorage(List<LeaveRequestDto> leaveRequest);
  Task ClearLocalStorage();
}

public sealed class LeaveRequestLocalStorageService : ILeaveRequestLocalStorageService
{
  private const string Key = "LeaveRequests";
  private readonly ILocalStorageService _localStorageService;
  public LeaveRequestLocalStorageService(ILocalStorageService localStorageService)
  {
    _localStorageService = localStorageService;
  }
  public async Task<List<LeaveRequestDto>> GetFromLocalStorage()
  {
    return await _localStorageService.GetItemAsync<List<LeaveRequestDto>>(Key);
  }
  public async Task<List<LeaveRequestDto>> SaveToLocalStorage(LeaveRequestDto leaveRequest)
  {
    List<LeaveRequestDto> leaveRequests = await GetFromLocalStorage();
    // make sure it doesn't already exist
    if (leaveRequests.Any(lvr => lvr.Equals(leaveRequest))) return leaveRequests;
    leaveRequests.Add(leaveRequest);
    await SaveToLocalStorage(leaveRequests);
    return leaveRequests;
  }
  public async Task<List<LeaveRequestDto>> UpdateLocalStorage(LeaveRequestDto leaveRequest)
  {
    List<LeaveRequestDto> leaveRequests = await GetFromLocalStorage();
    leaveRequests.Remove(leaveRequests.First(lvr => lvr.Equals(leaveRequest)));
    leaveRequests.Add(leaveRequest);
    await SaveToLocalStorage(leaveRequests);
    return leaveRequests;
  }
  public async Task<List<LeaveRequestDto>> DeleteFromLocalStorage(LeaveRequestDto leaveRequest)
  {
    List<LeaveRequestDto> leaveRequests = await GetFromLocalStorage();
    leaveRequests.Remove(leaveRequests.First(lvr => lvr.Equals(leaveRequest)));
    await SaveToLocalStorage(leaveRequests);
    return leaveRequests;
  }
  public async Task SaveToLocalStorage(List<LeaveRequestDto> leaveRequest)
  {
    await _localStorageService.SetItemAsync(Key, leaveRequest);
  }
  public async Task UpdateLocalStorage(List<LeaveRequestDto> leaveRequest)
  {
    await _localStorageService.SetItemAsync(Key, leaveRequest);
  }
  public async Task ClearLocalStorage()
  {
    await _localStorageService.RemoveItemAsync(Key);
  }
}
