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