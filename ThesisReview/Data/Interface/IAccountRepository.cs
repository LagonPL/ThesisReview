using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Models;
using ThesisReview.ViewModels;

namespace ThesisReview.Data.Interface
{
  public interface IAccountRepository
  {
    void SendRequest(RequestViewModel requestViewModel);
    void AddUserToList(RegisterViewModel registerViewModel, ApplicationUser user);
  }
}
