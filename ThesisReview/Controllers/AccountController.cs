using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThesisReview.Data.Services;
using ThesisReview.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThesisReview.Controllers
{
  public class AccountController : Controller
  {

    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
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
          if (string.IsNullOrEmpty(logInViewModel.ReturnUrl))
            return RedirectToAction("Index", "Home");
          return Redirect(logInViewModel.ReturnUrl);
        }
      }
      ModelState.AddModelError("", "Email/password not fount");
      return View(logInViewModel);
    }

    public ActionResult Register()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
      string content;
      if (ModelState.IsValid)
      {
        var user = new IdentityUser() { UserName = registerViewModel.UserName, Email = registerViewModel.Email };
        var result = await _userManager.CreateAsync(user, registerViewModel.Password);

        if (result.Succeeded)
        {
          content = "Witaj " + registerViewModel.UserName + ", udało ci się pomyślnie zarejstrować w naszym serwisie.";
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
