using GConge.Models.Models;
using GConge.Models.Models.Entities;
using GConge.Models.Utils;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618

namespace GConge.web.api.Data;

public class GCongeDbContext : DbContext
{
  public GCongeDbContext(DbContextOptions<GCongeDbContext> options) : base(options)
  {
  }
  public DbSet<User> Users { get; set; }
  public DbSet<Employee> Employees { get; set; }
  public DbSet<LeaveRequest> LeaveRequests { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    byte[] pwd = Utils.HashPassword("password", salt: out byte[] salt);
    // Add admin user to the database
    modelBuilder.Entity<User>().HasData(new User
      {
        Id = 1,
        Firstname = "Admin",
        Lastname = "Admin",
        Email = "admin.email@email.com",
        Role = UserRole.Admin,
        PhoneNumber = "+212123456789",
        Password = pwd,
        PasswordSalt = salt
      }
    );
  }
}
