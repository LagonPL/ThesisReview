using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThesisReview.Data.Interface;
using ThesisReview.Data.Models;
using ThesisReview.Data.Services;
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
        UsersList = _adminRepository.GetAllUserNoYou(user),
        RequestForms = _adminRepository.GetRequest()
      };
      return View(aVM);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Report(string datestart, string datefinish)
    {
      ReportViewModel rVM = new ReportViewModel
      {
        Reports = _adminRepository.GetReports(Convert.ToDateTime(datestart), Convert.ToDateTime(datefinish))
      };
      
      

      return View(rVM);
    }

    public async Task<IActionResult> Delete(string id)
    {
      _adminRepository.DeleteUser(id);
      string user = await GetCurrentUser();
      var aVM = new AdminViewModel
      {
        UsersList = _adminRepository.GetAllUserNoYou(user),
        RequestForms = _adminRepository.GetRequest()
      };
      return RedirectToAction("Index", "Admin");
    }

    public async Task<IActionResult> DeleteRequest(string email)
    {
      _adminRepository.DeleteRequest(email);
      string user = await GetCurrentUser();
      var aVM = new AdminViewModel
      {
        UsersList = _adminRepository.GetAllUserNoYou(user),
        RequestForms = _adminRepository.GetRequest()
      };
      return RedirectToAction("Index", "Admin");
    }

    public async Task<IActionResult> Reset(string id)
    {
      var applicationUser = _userManager.FindByEmailAsync(id);
      string user = await GetCurrentUser();
      var token = _userManager.GeneratePasswordResetTokenAsync(applicationUser.Result);
      var guid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
      await _userManager.ResetPasswordAsync(applicationUser.Result, token.Result, guid.ToString());
      EmailSender.Send(id, "ThesisReview - Reset Hasła", "Administrator serwisu zresetował twoje hasło, obecne to: " + guid.ToString());
      var aVM = new AdminViewModel
      {
        UsersList = _adminRepository.GetAllUserNoYou(user),
        RequestForms = _adminRepository.GetRequest()
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