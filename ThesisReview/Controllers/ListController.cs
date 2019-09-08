using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThesisReview.Data.Interface;
using ThesisReview.Data.Models;
using ThesisReview.ViewModels;

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

    public async Task<ActionResult> Index(string sortOrder, string currentOrder)
    {
      
      string mail = await GetCurrentUser();
      ViewData["CurrentSort"] = sortOrder;
      ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
      
      var revieweritems = _listRepository.GetReviewerForms(mail).Concat(_listRepository.GetGuardianForms(mail));
      switch (sortOrder)
      {
        case "Date":
          revieweritems = revieweritems.OrderBy(p => p.DateTimeStart);
          break;

        default:
          revieweritems = revieweritems.OrderByDescending(p => p.DateTimeStart);
          break;

      }
      

      var finished = revieweritems.Where(p => p.Status == "Oceniono");
      revieweritems = revieweritems.Where(p => p.Status != "Oceniono");
      var fLVM = new ListViewModel
      {
        Forms = revieweritems,
        ArchiveForms = finished
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
