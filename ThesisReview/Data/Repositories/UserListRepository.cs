using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ThesisReview.Data.Interface;
using ThesisReview.Data.Models;

namespace ThesisReview.Data.Repositories
{
  public class UserListRepository : IUserListRepository
  {

    private readonly AppDbContext _appDbContext;

    public UserListRepository(AppDbContext appDbContext)
    {
      _appDbContext = appDbContext;
    }

    public IEnumerable<UserList> GetAllUser()
    {
      _appDbContext.UserLists.Load();
      var form = _appDbContext.UserLists
        .Include(b => b.ApplicationUser).Where(b => b.ApplicationUser.IsActive == true);
      return form;
    } 
  }
}
