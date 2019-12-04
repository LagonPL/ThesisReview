using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThesisReview.Data.Models;
using ThesisReview.Data.Services;
using ThesisReview.ViewModels;

namespace ThesisReview.Controllers
{
  public class SettingsController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;


    public SettingsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    public ActionResult Account()
    {
      var sVM = new SettingViewModel
      {
        AnyError = false
      };
      return View(sVM);
    }

    [HttpPost]
    public async Task<IActionResult> Account(SettingViewModel settingViewModel)
    {
      SettingViewModel svm = new SettingViewModel
      {
        AnyError = true
      };

      if (!ModelState.IsValid)
        return View(svm);

      if (settingViewModel.NewPassword.Equals(settingViewModel.ConfirmPassword))
      {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user != null)
        {
          var results = await _userManager.ChangePasswordAsync(user, settingViewModel.OldPassword,
            settingViewModel.NewPassword);

          if (!results.Succeeded)
          {
            return View(svm);
          }
        }
      }
      else
      {
        settingViewModel.AnyError = true;
        return View(svm);
      }
      return RedirectToAction("Index", "Home", new { @id = "sukces" });
    }

    [HttpPost]
    public async Task<IActionResult> Email(SettingViewModel settingViewModel)
    {
      string url;
      if (ModelState.IsValid)
      {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        var token = await _userManager.GenerateChangeEmailTokenAsync(user, settingViewModel.Email);
        var uri = new UriBuilder
        {
          Scheme = Request.Scheme,
          Host = Request.Host.ToString(),
          Path = "/Settings/Mail/"
        };
        url = StringGenerator.LinkGenerator(uri, "", "");
        EmailSender.Send(user.Email, "ThesisReview - Zmiana Maila", "Wysłaleś zgłoszenie o zmiane maila.\nKliknij w poniższy link aby potwierdzić\n");
      }
      else
      {
        settingViewModel.AnyError = true;
        return View(settingViewModel);
      }
      return View(settingViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> MailChange(SettingViewModel settingViewModel)
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        var token = await _userManager.GenerateChangeEmailTokenAsync(user, settingViewModel.Email);
        var uri = new UriBuilder
        {
          Scheme = Request.Scheme,
          Host = Request.Host.ToString(),
          Path = "/Settings/Mail/"
        };
        EmailSender.Send(user.Email, "ThesisReview - Zmiana Maila", "Wysłaleś zgłoszenie o zmiane maila.\nKliknij w poniższy link aby potwierdzić\n");
      }
      else
      {
        settingViewModel.AnyError = true;
        return View(settingViewModel);
      }
      return View(settingViewModel);
    }

  }
}