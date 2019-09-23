using System.Collections.Generic;
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

    public IEnumerable<UserList> GetAllUser() => _appDbContext.UserLists;
  }
}
