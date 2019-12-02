using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThesisReview.Data;
using ThesisReview.Data.Models;
using ThesisReview.Data.Services;
using ThesisReview.ViewModels;

namespace ThesisReview.Controllers
{
  public class AccountController : Controller
  {

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly AppDbContext _appDbContext;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext appDbContext)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _appDbContext = appDbContext;
    }



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
      if (user != null)
      {
        var result = await _signInManager.PasswordSignInAsync(user, logInViewModel.Password, false, false);
        if (result.Succeeded)
        {
          if (string.IsNullOrEmpty(logInViewModel.ReturnUrl))
            return RedirectToAction("Index", "Home");
          return Redirect(logInViewModel.ReturnUrl);
        }
      }
      ModelState.AddModelError("wrongform", "Nieprawidłowy email lub hasło");
      return View(logInViewModel);
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Register(string email, string name, string department, string title)
    {
      if (name != null)
      {
        var registerViewModel = new RegisterViewModel
        {
          Fullname = name,
          Email = email,
          Departments = new SelectList(StringGenerator.DepartmentFiller()),
          Titles = new SelectList(StringGenerator.TitlesFiller())
        };
        foreach (var item in registerViewModel.Departments)
        {
          if (item.Value == department)
          {
            item.Selected = true;
            break;
          }
        }
        foreach (var item in registerViewModel.Titles)
        {
          if (item.Value == title)
          {
            item.Selected = true;
            break;
          }
        }
        return View(registerViewModel);
      }
      var rVM = new RegisterViewModel
      {
        Departments = new SelectList(StringGenerator.DepartmentFiller()),
        Titles = new SelectList(StringGenerator.TitlesFiller())
      };
      return View(rVM);
    }

    public ActionResult RequestForm()
    {
      var rVM = new RequestViewModel
      {
        Departments = new SelectList(StringGenerator.DepartmentFiller()),
        Titles = new SelectList(StringGenerator.TitlesFiller())
      };
      return View(rVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult RequestForm(RequestViewModel requestViewModel)
    {
      if (ModelState.IsValid)
      {
        var requestForm = new RequestForm
        {
          Department = requestViewModel.Department,
          Email = requestViewModel.Email,
          Fullname = requestViewModel.Fullname
        };
        _appDbContext.RequestForms.Add(requestForm);
        _appDbContext.SaveChanges();
        return RedirectToAction("Index", "Home");
      }
      else
      {
        ModelState.AddModelError("wrongform", "Źle wpisane dane");
        requestViewModel.Departments = new SelectList(StringGenerator.DepartmentFiller());
        requestViewModel.Titles = new SelectList(StringGenerator.TitlesFiller());
        return View(requestViewModel);
      }
      
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
      string content;
      if (ModelState.IsValid)
      {
        var user = new ApplicationUser() { UserName = registerViewModel.UserName, Email = registerViewModel.Email, Department = registerViewModel.Department, Fullname = registerViewModel.Fullname, Title = registerViewModel.Title };
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
          content = "Witaj " + registerViewModel.Fullname + "!\nTwój mail: " + registerViewModel.Email + " został pomyślnie zarejestrowany w naszym serwisie. \nTwoj login to: " + registerViewModel.Email + "\nHasło: " + registerViewModel.Password + "\nZmienić hasło możesz w ustawieniach użytkownika po zalogowaniu";
          EmailSender.Send(registerViewModel.Email, "ThesisReview - Pomyślna Rejestracja", content);
          UserList userList = new UserList
          {
            Department = registerViewModel.Department,
            Fullname = registerViewModel.Fullname,
            Mail = registerViewModel.Email,
            Title = registerViewModel.Title
          };
          _appDbContext.UserLists.Add(userList);
          _appDbContext.SaveChanges();
          return RedirectToAction("Index", "Home");
        }
        else
        {
          ViewData["Message"] = "Your application description page.";
        }

      }
      registerViewModel.Departments = new SelectList(StringGenerator.DepartmentFiller());
      registerViewModel.Titles = new SelectList(StringGenerator.TitlesFiller());
      ModelState.AddModelError("wrongform", "Źle wprowadzone dane");
      return View(registerViewModel);
    }


    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "Home");
    }

  }
}
