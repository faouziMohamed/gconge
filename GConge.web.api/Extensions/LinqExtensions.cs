using GConge.Models.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace GConge.web.api.Extensions;

static public class LinqExtensions
{
  static public IIncludableQueryable<LeaveRequest, User> IncludeEmployees(this IQueryable<LeaveRequest> leaveRequests)
  {
    return leaveRequests.Include(static x => x.RequestingEmployee).ThenInclude(static e => e.User);
  }
}
