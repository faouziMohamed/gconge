using GConge.Models.Models.Entities;
using GConge.web.api.Data;
using GConge.web.api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GConge.web.api.Repositories;

public sealed class EmployeeRepository : IEmployeeRepository
{
  private readonly GCongeDbContext _db;
  public EmployeeRepository(GCongeDbContext db)
  {
    _db = db;

  }
  public async Task<Employee?> GetEmployeeById(int employeeId)
  {
    return await _db.Employees
      .Include(static u => u.User)
      .FirstOrDefaultAsync(e => e.Id == employeeId);
  }
  public Task<Employee?> GetEmployeeByUserId(int userId)
  {
    return _db.Employees
      .Include(static u => u.User)
      .FirstOrDefaultAsync(e => e.UserId == userId);
  }


  public async Task<List<Employee>> GetEmployees()
  {
    return await _db.Employees
      .Include(static u => u.User)
      .ToListAsync();
  }

  public async Task<Employee> CreateEmployee(Employee employee)
  {
    // check if the employee already exists
    bool existingEmployee = await EmployeeExists(employee.Id);

    if (existingEmployee)
    {
      throw new Exception("Employee already exists");
    }

    EntityEntry<Employee> newEntryEmployee = await _db.Employees.AddAsync(employee);
    await _db.SaveChangesAsync();
    return newEntryEmployee.Entity;
  }
  public async Task<Employee> UpdateEmployee(Employee employee)
  {
    // check if the employee already exists
    bool existingEmployee = await EmployeeExists(employee.Id);

    if (!existingEmployee)
    {
      throw new Exception("Employee does not exist");
    }

    _db.Employees.Update(employee);
    await _db.SaveChangesAsync();
    return employee;
  }
  public async Task<Employee?> DeleteEmployee(int id)
  {
    // check if the employee already exists
    bool existingEmployee = await EmployeeExists(id);

    if (!existingEmployee)
    {
      throw new Exception("Employee does not exist");
    }

    var employee = await _db.Employees
      .Include(static u => u.User)
      .FirstOrDefaultAsync(e => e.Id == id);

    _db.Employees.Remove(employee!);
    await _db.SaveChangesAsync();
    return employee;
  }
  public async Task<bool> EmployeeExists(int id)
  {
    return await _db.Employees.AnyAsync(e => e.Id == id);
  }
}
