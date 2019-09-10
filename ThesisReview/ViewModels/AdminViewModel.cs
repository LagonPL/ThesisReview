using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Models;

namespace ThesisReview.ViewModels
{
  public class AdminViewModel
  {
    public IEnumerable<ApplicationUser> UsersList { get; set; }
    public IEnumerable<RequestForm> RequestForms { get; set; }
  }
}
