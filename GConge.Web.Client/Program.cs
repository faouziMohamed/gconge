using Blazored.LocalStorage;
using GConge.Web.Client;
using GConge.Web.Client.Services;
using GConge.Web.Client.Services.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// string apiServerUri = Utils.GetConfig<ApiService>("appsettings.json").BaseUrl;
builder.Services.AddScoped(static _ => new HttpClient
  {
    // BaseAddress = new Uri("https://localhost:44382"),
    BaseAddress = new Uri("https://localhost:7161"),
    Timeout = Timeout.InfiniteTimeSpan
  }
);

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<IUserLocalStorageService, UserLocalStorageService>();
builder.Services.AddScoped<IEmployeeLocalStorageService, EmployeeLocalStorageService>();
builder.Services.AddScoped<ILeaveRequestLocalStorageService, LeaveRequestLocalStorageService>();

await builder.Build().RunAsync();
