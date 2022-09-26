using System.Text;
using GConge.Models.Models.Identity;
using GConge.Models.Utils;
using GConge.web.api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GConge.web.api.Extensions;

static public class Configurations
{
  static public IServiceCollection AddMySqlContext(this IServiceCollection services, IConfiguration configuration)
  {
    string? connectionString = configuration.GetConnectionString("MyDbConnection");
    var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));
    services.AddDbContext<GCongeDbContext>(options =>
      options.UseMySql(connectionString, serverVersion)
        // .LogTo(Console.WriteLine, LogLevel.Information)
        // .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
    );

    return services;
  }

  static public IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration configuration)
  {
    bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
    var jwtSettings = Utils.GetConfig<JwtSettings>(isDevelopment);
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
          };
        }
      );

    return services;
  }
}
