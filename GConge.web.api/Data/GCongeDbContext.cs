using GConge.Models.Models;
using Microsoft.EntityFrameworkCore;

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
    // Add admin user to the database
    modelBuilder.Entity<User>().HasData(new User
      {
        Id = 1,
        Firstname = "Admin",
        Lastname = "Admin",
        Email = "admin.email@email.com",
        Role = "Admin",
        Phone = "+212123456789",
        Password = "admin"


      }
    );
  }
}
