using System;
using System.Collections.Generic;
using ThesisReview.Data.Models;

namespace ThesisReview.ViewModels
{
  public class UserListViewModel
  {
    public IEnumerable<UserList> UsersList { get; set; }
    public List<String> Photos { get; set; }
  }

  
}
