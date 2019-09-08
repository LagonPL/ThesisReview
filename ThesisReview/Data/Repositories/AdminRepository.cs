using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public void DeleteUser(string useId)
    {
      var deleteduser = _appDbContext.Users.FirstOrDefault(p => p.Email == useId);
      _appDbContext.Users.Remove(deleteduser);
      _appDbContext.SaveChanges();
    }

    public IEnumerable<ApplicationUser> GetAllUser() => _appDbContext.Users;

    public IEnumerable<ApplicationUser> GetAllUserNoYou(string user) => _appDbContext.Users.Where(p => (p.Email != user) && (p.Department != "Administrator Główny"));

    public IEnumerable<Report> GetReports(DateTime datestart, DateTime datefinish) => _appDbContext.Reports.Where(t => t.Date >= datestart && t.Date <= datefinish);

  }
}
