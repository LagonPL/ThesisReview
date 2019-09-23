using System.Collections.Generic;
using ThesisReview.Data.Models;

namespace ThesisReview.ViewModels
{
  public class AdminViewModel
  {
    public IEnumerable<ApplicationUser> UsersList { get; set; }
    public IEnumerable<RequestForm> RequestForms { get; set; }
  }
}
