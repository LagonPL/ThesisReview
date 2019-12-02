using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ThesisReview.Data.Interface;
using ThesisReview.ViewModels;

namespace ThesisReview.Controllers
{
  public class UserListController : Controller
  {

    private readonly IUserListRepository _userListRepository;

    public UserListController(IUserListRepository userListRepository)
    {
      _userListRepository = userListRepository;
    }

    public IActionResult Index(string searchString)
    {
      ViewData["CurrentFilter"] = searchString;
      var list = _userListRepository.GetAllUser();
      var temp = list;



      if (!String.IsNullOrEmpty(searchString))
      {
        temp = list.Where(s => s.Fullname.Contains(searchString, StringComparison.OrdinalIgnoreCase))
          .Concat(list.Where(s => s.Mail.Contains(searchString, StringComparison.OrdinalIgnoreCase)))
            .Concat(list.Where(s => s.Department.Contains(searchString, StringComparison.OrdinalIgnoreCase)))
              .Concat(list.Where(s => s.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
      }


      var uLVM = new UserListViewModel
      {
        UsersList = temp.Distinct()
      };
      return View(uLVM);
    }
  }
}
