using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using ThesisReview.Data.Services;
using ThesisReview.Data.Models;

namespace ThesisReview.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult About()
    {
      ViewData["Message"] = "Your application description page.";
      return View();
    }

    public IActionResult Contact()
    {
      

      return View();
    }

    public IActionResult Default()
    {

      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }
  }
}
