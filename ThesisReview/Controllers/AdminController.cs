using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThesisReview.Data.Interface;
using ThesisReview.Data.Models;
using ThesisReview.ViewModels;

namespace ThesisReview.Controllers
{
  public class AdminController : Controller
  {

    private readonly IAdminRepository _adminRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(IAdminRepository adminRepository, UserManager<ApplicationUser> userManager)
    {
      _adminRepository = adminRepository;
      _userManager = userManager;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
      string user = await GetCurrentUser();
      var aVM = new AdminViewModel
      {
        UsersList = _adminRepository.GetAllUserNoYou(user)
      };
      return View(aVM);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Report(string datestart, string datefinish)
    {
      ReportViewModel rVM = new ReportViewModel
      {
        Reports = _adminRepository.GetReports(datefinish, datestart)
      };
      
      DateTime dateTimeStart = Convert.ToDateTime(datestart);
      DateTime dateTimeFinish = Convert.ToDateTime(datefinish);

      return View(rVM);
    }

    public IActionResult Delete(string id)
    {
      _adminRepository.DeleteUser(id);
      var aVM = new AdminViewModel
      {
        UsersList = _adminRepository.GetAllUser()
      };
      return RedirectToAction("Index", "Admin");
    }

    private async Task<string> GetCurrentUser()
    {
      var user = await _userManager.GetUserAsync(HttpContext.User);
      var email = _userManager.GetEmailAsync(user);
      string mail = "";
      try
      {
        mail = user.Email;
      }
      catch (NullReferenceException)
      {
        return mail;
      }

      return mail;
    }
  }
}