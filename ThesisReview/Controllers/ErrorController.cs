using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ThesisReview.Controllers
{
  public class ErrorController : Controller
  {
    public IActionResult Error()
    {
      ViewBag.ErrorMessage = "Nieprawidłowy link.\nSprawdź czy poprawnie skopiowałeś link z maila.";
      return View();
    }
  }
}