using System.Collections.Generic;
using ThesisReview.Data.Models;

namespace ThesisReview.Data.Interface
{
  public interface IUserListRepository
  {
    IEnumerable<UserList> GetAllUser();
  }
}
