using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThesisReview.Data;
using ThesisReview.Data.Interface;
using ThesisReview.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThesisReview.Controllers
{
  public class UserListController : Controller
  {

    private readonly IUserListRepository _userListRepository;

    public UserListController(IUserListRepository userListRepository)
    {
      _userListRepository = userListRepository;
    }

    public IActionResult Index()
    {
      var uLVM = new UserListViewModel
      {
        UsersList = _userListRepository.GetAllUser()
      };
      return View(uLVM);
    }
  }
}
