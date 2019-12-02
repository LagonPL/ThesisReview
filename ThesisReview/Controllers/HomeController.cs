using Microsoft.AspNetCore.Mvc;

namespace ThesisReview.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index(string id)
    {
      if(id == "sukces")
      {
        ViewData["Succes"] = "Twoje hasło zostało poprawnie zmienione!";
      }
      else if(id != null)
      {
        ViewData["Succes"] = "Twój formularz o kodzie: <b>" + id + "</b> został prawidłowo stworzony i wysłany na twój mail!";
      }

      return View();
    }
    
    public IActionResult Privacy()
    {
      return View();
    }
  }
}
