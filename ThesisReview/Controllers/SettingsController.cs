using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThesisReview.Data.Models;
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
      if (settingViewModel.NewPassword.Equals(settingViewModel.ConfirmPassword))
      {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user != null)
        {
          await _userManager.ChangePasswordAsync(user, settingViewModel.OldPassword, settingViewModel.NewPassword);
        }
      }
      else
      {
        settingViewModel.AnyError = true;
        return View(settingViewModel);
      }
      return View();
    }

  }
}