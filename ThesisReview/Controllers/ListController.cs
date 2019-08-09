using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThesisReview.Data.Interface;
using ThesisReview.Data.Models;
using ThesisReview.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThesisReview.Controllers
{
  public class ListController : Controller
  {

    private readonly IListRepository _listRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public ListController(IListRepository listRepository, UserManager<ApplicationUser> userManager)
    {
      _listRepository = listRepository;
      _userManager = userManager;
    }

    public async Task<ActionResult> Index()
    {

      string mail = await GetCurrentUser();
      var revieweritems = _listRepository.GetReviewerForms(mail).Concat(_listRepository.GetGuardianForms(mail));
      //var guardianitems = _listRepository.GetGuardianForms(mail);
      var fLVM = new ListViewModel
      {
        Forms = revieweritems
      };

      return View(fLVM);
    }

    private async Task<string> GetCurrentUser()
    {
      var user = await _userManager.GetUserAsync(HttpContext.User);
      var email = _userManager.GetEmailAsync(user);
      string mail = user.Email;
      return mail;
    }

  }
}
