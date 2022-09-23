using System.ComponentModel.DataAnnotations;
using GConge.Models.Models.Entities;

namespace GConge.web.api.Repositories.Contracts;

public interface IUserRepository
{
  Task<User?> GetUserById(int userId);
  Task<User?> GetUserByEmail(string email);
  Task<User> CreateUser(User user);
  Task<User> UpdateUser(User user);
  Task<User> DeleteUser(int userId);
  Task<bool> UserExists(int userId);
  Task<bool> UserExists([EmailAddress] string email);
}
