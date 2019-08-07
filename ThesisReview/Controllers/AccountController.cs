using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThesisReview.Data.Models;
using ThesisReview.Data.Services;
using ThesisReview.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThesisReview.Controllers
{
  public class AccountController : Controller
  {

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }



    // GET: /<controller>/
    public IActionResult Login(string returnUrl)
    {
      return View(new LogInViewModel()
      {
        ReturnUrl = returnUrl
      });
    }
    [HttpPost]
    public async Task<IActionResult> Login(LogInViewModel logInViewModel)
    {
      if (!ModelState.IsValid)
        return View(logInViewModel);

      var user = await _userManager.FindByEmailAsync(logInViewModel.Email);

      if(user != null)
      {
        var result = await _signInManager.PasswordSignInAsync(user, logInViewModel.Password, false, false);
        if (result.Succeeded)
        {
          //var role = await _userManager.GetRolesAsync(user);
          //EmailSender.Send(logInViewModel.Email, "Pomyślna Rejestracja", role[0]);
          if (string.IsNullOrEmpty(logInViewModel.ReturnUrl))
            return RedirectToAction("Index", "Home");
          return Redirect(logInViewModel.ReturnUrl);
        }
      }
      ModelState.AddModelError("", "Email/password not fount");
      return View(logInViewModel);
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Register()
    {
      var rVM = new RegisterViewModel();
      rVM.Departments = new SelectList(ListFiller.DepartmentFiller());
      return View(rVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
      string content;
      if (ModelState.IsValid)
      {
        var user = new ApplicationUser() { UserName = registerViewModel.UserName, Email = registerViewModel.Email, Department = registerViewModel.Department};
        var result = await _userManager.CreateAsync(user, registerViewModel.Password);
        if (registerViewModel.IsAdmin)
        {
          _userManager.AddToRoleAsync(user, "Admin").Wait();
        }
        else
        {
          _userManager.AddToRoleAsync(user, "Reviewer").Wait();
        }

        if (result.Succeeded)
        {
          content = "Witaj " + registerViewModel.UserName + ", zostałeś pomyślnie zarejestrowany w naszym serwisie.";
          EmailSender.Send(registerViewModel.Email, "Pomyślna Rejestracja", content);
          return RedirectToAction("Index", "Home");
        }
        else
        {
          ViewData["Message"] = "Your application description page.";
        }

      }
      return View(registerViewModel);
    }


    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "Home");
    }

  }
}
