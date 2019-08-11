using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThesisReview.Data.Interface;
using ThesisReview.Data.Models;
using ThesisReview.ViewModels;

namespace ThesisReview.Controllers
{
  public class AdminController : Controller
  {

    private readonly IAdminRepository _adminRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(IAdminRepository adminRepository, UserManager<ApplicationUser> userManager)
    {
      _adminRepository = adminRepository;
      _userManager = userManager;
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
      var aVM = new AdminViewModel
      {
        UsersList = _adminRepository.GetAllUser()
      };
      return View(aVM);
    }

    public IActionResult Delete(string id)
    {
      _adminRepository.DeleteUser(id);
      var aVM = new AdminViewModel
      {
        UsersList = _adminRepository.GetAllUser()
      };
      return RedirectToAction("Index", "Admin");
    }
  }
}