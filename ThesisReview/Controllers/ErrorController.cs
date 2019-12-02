using Microsoft.AspNetCore.Mvc;

namespace ThesisReview.Controllers
{
  public class ErrorController : Controller
  {
    [Route("Error/{statusCode}")]
    public IActionResult Error(int statusCode)
    {
      if(statusCode == 404)
      {
        ViewData["Error"] = "Brak uprawnień lub podana strona nie istnieje!";

      }
      else if(statusCode == 0)
      {
        ViewData["Error"] = "Nieprawidłowy link do recenzji!";
      }
      else
      {
        ViewData["Error"] = "Wygląda na to, że wystąpił problem. Wyślij na mail administratora co robiłeś zanim się tu dostaleś aby pomóc w rozwoju serwisu i poprawie blędów.";
      }
      return View();
    }
  }
}