using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThesisReview.Data.Models;
using ThesisReview.Data.Services;
using ThesisReview.ViewModels;

namespace ThesisReview.Controllers
{
    public class FormController : Controller
    {


        public IActionResult CreateForm()
        {
            return View();
        }

    [HttpPost]
    public IActionResult CreateForm(Form form)
    {
      string content, url;
      Guid guid = Guid.NewGuid();
      var uri = new UriBuilder
      {
        Scheme = Request.Scheme,
        Host = Request.Host.ToString(),
        Path = "/Form/CreationComplete/"
      };
      url = StringGenerator.LinkGenerator(uri, guid.ToString());
      if (ModelState.IsValid)
      {
        DatabaseAdder.AddForm(form, guid.ToString());
        
        content = "Witaj, udało ci się pomyślnie wysłać zgłoszenie w naszym serwisie. \nLink: " + url;
        EmailSender.Send(form.StudentMail, "Stworzyłeś formularz", content);
        return RedirectToAction("Index", "Home");
      }
      return View(form);
      
    }

    public ActionResult CreationComplete(string id)
    {
      Form form = new Form();
      form = DatabaseAdder.ReadForm(id);

      var fdVM = new FormDetailViewModel
      {
        Title = form.Title,
        FormURL = form.FormURL
      };

      return View(fdVM);
    }


  }
}