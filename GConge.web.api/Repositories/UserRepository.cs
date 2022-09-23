using GConge.Models.Models.Entities;
using GConge.web.api.Data;
using GConge.web.api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GConge.web.api.Repositories;

public sealed class UserRepository : IUserRepository
{
  private readonly GCongeDbContext _db;
  public UserRepository(GCongeDbContext db)
  {
    _db = db;
  }
  public async Task<User?> GetUserById(int userId)
  {
    return await _db.Users.SingleOrDefaultAsync(u => u.Id == userId);
  }
  public Task<User?> GetUserByEmail(string email)
  {
    return _db.Users.SingleOrDefaultAsync(u => u.Email == email);
  }
  public async Task<User> CreateUser(User user)
  {
    // make sure the user doesn't already exist
    if (await UserExists(user.Email))
    {
      throw new Exception("The email address is already in use by another user.");
    }

    EntityEntry<User> entryUser = await _db.Users.AddAsync(user);
    await _db.SaveChangesAsync();
    return entryUser.Entity;
  }
  public async Task<User> UpdateUser(User user)
  {
    // make sure the user exists
    if (!await UserExists(user.Id))
    {
      throw new Exception("The user does not exist.");
    }

    EntityEntry<User> entryUser = _db.Users.Update(user);
    await _db.SaveChangesAsync();
    return entryUser.Entity;
  }
  public async Task<User> DeleteUser(int userId)
  {
    // make sure the user exists
    if (!await UserExists(userId))
    {
      throw new Exception("The user does not exist.");
    }

    var user = await GetUserById(userId);
    _db.Users.Remove(user!);
    await _db.SaveChangesAsync();
    return user!;
  }
  public async Task<bool> UserExists(int userId)
  {
    return await _db.Users.AnyAsync(u => u.Id == userId);
  }
  public async Task<bool> UserExists(string email)
  {
    return await _db.Users.AnyAsync(u => u.Email == email);
  }
}
