using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using ThesisReview.Data.Interface;
using ThesisReview.Data.Models;

namespace ThesisReview.Data.Repositories
{
  public class AdminRepository : IAdminRepository
  {
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminRepository(AppDbContext appDbContext, UserManager<ApplicationUser> userManager)
    {
      _appDbContext = appDbContext;
      _userManager = userManager;
    }

    public void DeleteUser(string userId)
    {
      var deleteduser = _appDbContext.Users.FirstOrDefault(p => p.Email == userId);
      var userlist = _appDbContext.UserLists.FirstOrDefault(p => p.Mail == userId);
      _appDbContext.UserLists.Remove(userlist);
      deleteduser.IsActive = false;
      _appDbContext.SaveChanges();
    }

    public void ActivateUser(string userId)
    {
      var user = _appDbContext.Users.FirstOrDefault(p => p.Email == userId);
      var userlist = new UserList
      {
        ApplicationUser = user,
        Department = user.Department,
        Fullname = user.Fullname,
        Mail = user.Email,
        Title = user.Title
      };
      _appDbContext.UserLists.Add(userlist);
      user.IsActive = true;
      _appDbContext.SaveChanges();
    }

    public void DeleteRequest(string email)
    {
      var delete = _appDbContext.RequestForms.FirstOrDefault(p => p.Email == email);
      _appDbContext.RequestForms.Remove(delete);
      _appDbContext.SaveChanges();
    }

    public IEnumerable<ApplicationUser> GetAllUser() => _appDbContext.Users;

    public IEnumerable<ApplicationUser> GetAllUserNoYou(string user) => _appDbContext.Users
      .Where(p => (p.Email != user) && (p.Department != "Administrator Główny"));

    public IEnumerable<Report> GetReports(DateTime datestart, DateTime datefinish) => _appDbContext.Reports
      .Where(t => t.Date >= datestart && t.Date <= datefinish);

    public IEnumerable<RequestForm> GetRequest() => _appDbContext.RequestForms;
    
  }
}
