using GConge.web.api.Extensions;
using GConge.web.api.Repositories;
using GConge.web.api.Repositories.Contracts;
using GConge.web.api.Services;
using GConge.web.api.Services.Contracts;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMySqlContext(builder.Configuration);
builder.Services.AddJwtService(builder.Configuration);

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
builder.Services.AddSingleton<IJwtAuthenticationService, JwtAuthenticationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors(static policy => policy
  .WithOrigins(
    "https://localhost:44330",
    "http://localhost:44330",
    "https://localhost:7093",
    "http://localhost:7093"
  )
  .AllowAnyMethod()
  .WithHeaders(HeaderNames.ContentType)
);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
