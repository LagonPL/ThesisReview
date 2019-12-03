using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Interface;
using ThesisReview.Data.Models;
using ThesisReview.ViewModels;

namespace ThesisReview.Data.Repositories
{
  public class AccountRepository : IAccountRepository
  {

    private readonly AppDbContext _appDbContext;

    public AccountRepository(AppDbContext appDbContext)
    {
      _appDbContext = appDbContext;
    }

    public void AddUserToList(RegisterViewModel registerViewModel, ApplicationUser user)
    {
      var userList = new UserList
      {
        Department = registerViewModel.Department,
        Fullname = registerViewModel.Fullname,
        Mail = registerViewModel.Email,
        Title = registerViewModel.Title,
        ApplicationUser = user
      };
      _appDbContext.UserLists.Add(userList);
      _appDbContext.SaveChanges();
    }

    public void SendRequest(RequestViewModel requestViewModel)
    {
      var requestForm = new RequestForm
      {
        Department = requestViewModel.Department,
        Email = requestViewModel.Email,
        Fullname = requestViewModel.Fullname,
        Title = requestViewModel.Title
      };
      _appDbContext.RequestForms.Add(requestForm);
      _appDbContext.SaveChanges();
    }
  }
}
