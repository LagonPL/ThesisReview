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
      ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
      ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
      ViewData["TitleSortParm"] = sortOrder == "Title" ? "title_desc" : "Title";
      ViewData["TypeSortParm"] = sortOrder == "Type" ? "type_desc" : "Type";
      ViewData["StatusSortParm"] = sortOrder == "Status" ? "status_desc" : "Status";

      var revieweritems = _listRepository.GetReviewerForms(mail);
      revieweritems = SortForms(sortOrder, revieweritems);
      

      var finished = revieweritems.Where(p => p.Status == "Oceniono");
      finished = SortForms(sortOrder, finished);
      revieweritems = revieweritems.Where(p => p.Status != "Oceniono");
      var fLVM = new ListViewModel
      {
        Forms = revieweritems,
        ArchiveForms = finished,
        mail = mail
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

    private IEnumerable<Form> SortForms(String sortOrder, IEnumerable<Form> revieweritems)
    {
      switch (sortOrder)
      {
        case "Date":
          revieweritems = revieweritems.OrderBy(p => p.DateTimeStart);
          break;
        case "Name":
          revieweritems = revieweritems.OrderBy(p => p.StudentMail);
          break;
        case "name_desc":
          revieweritems = revieweritems.OrderByDescending(p => p.StudentMail);
          break;
        case "Title":
          revieweritems = revieweritems.OrderBy(p => p.Title);
          break;
        case "title_desc":
          revieweritems = revieweritems.OrderByDescending(p => p.Title);
          break;
        case "Type":
          revieweritems = revieweritems.OrderBy(p => p.ReviewType);
          break;
        case "type_desc":
          revieweritems = revieweritems.OrderByDescending(p => p.ReviewType);
          break;
        case "Status":
          revieweritems = revieweritems.OrderBy(p => p.Status);
          break;
        case "status_desc":
          revieweritems = revieweritems.OrderByDescending(p => p.Status);
          break;
        default:
          revieweritems = revieweritems.OrderByDescending(p => p.DateTimeStart);
          break;

      }
      return revieweritems;
    }

  }
}
