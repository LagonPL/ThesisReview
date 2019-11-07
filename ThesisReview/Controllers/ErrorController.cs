using Microsoft.AspNetCore.Mvc;

namespace ThesisReview.Controllers
{
  public class ErrorController : Controller
  {
    public IActionResult Error()
    {
      return View();
    }
  }
}