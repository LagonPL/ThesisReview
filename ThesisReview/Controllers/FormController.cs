using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThesisReview.Data.Models;
using ThesisReview.Data.Services;
using ThesisReview.ViewModels;

namespace ThesisReview.Controllers
{
  public class FormController : Controller
  {


    public IActionResult Create()
    {
      var fVM = new FormViewModel
      {
        ReviewTypeList = new SelectList(StringGenerator.ReviewTypesFiller())
      };

      return View(fVM);
    }

    [HttpPost]
    public IActionResult Create(FormViewModel fVM)
    {
      Form form = new Form
      {
        Title = fVM.Title,
        ReviewType = fVM.ReviewType,
        ShortDescription = fVM.ShortDescription,
        StudentMail = fVM.StudentMail,
        Status = "Nowa",
        ReviewerName = fVM.ReviewerName,
        GuardianName = fVM.GuardianName        
      };
      string content, url;
      Guid guid = Guid.NewGuid();
      var uri = new UriBuilder
      {
        Scheme = Request.Scheme,
        Host = Request.Host.ToString(),
        Path = "/Form/Details/"
      };
      url = StringGenerator.LinkGenerator(uri, guid.ToString());
      if (ModelState.IsValid)
      {
        DatabaseAction.AddForm(form, guid.ToString(), "0");

        content = "Witaj, udało ci się pomyślnie wysłać zgłoszenie w naszym serwisie. \nLink: " + url;
        EmailSender.Send(form.StudentMail, "Stworzyłeś formularz", content);
        return RedirectToAction("Index", "Home");
      }
      return View(form);

    }

    public ActionResult Details(string id)
    {
      Form form = new Form();
      form = DatabaseAction.ReadForm(id);
      var suma = new Sum();
      var questions = StringGenerator.GetQuestions(form.ReviewType);
      if (form.ReviewType.Equals("Praca Magisterska"))
        suma = Util.Sum(form);
      var fdVM = new FormDetailViewModel
      {
        Form = form,
        ReviewType = form.ReviewType,
        QuestionList = questions,
        Answers = StringGenerator.AnswersGenerator(),
        Sum = suma
      };

      return View(fdVM);
    }

    [HttpPost]
    public IActionResult UpdateForm(FormDetailViewModel fdVM)
    {
      Questions questions = new Questions
      {
        Question1 = fdVM.Form.Questions.Question1,
        Question2 = fdVM.Form.Questions.Question2,
        Question3 = fdVM.Form.Questions.Question3,
        Question4 = fdVM.Form.Questions.Question4,
        Question5 = fdVM.Form.Questions.Question5,
        Question6 = fdVM.Form.Questions.Question6,
        Question7 = fdVM.Form.Questions.Question7,
        Question8 = fdVM.Form.Questions.Question8,
        Question9 = fdVM.Form.Questions.Question9,
        Question0 = fdVM.Form.Questions.Question0,
        LongReview = fdVM.Form.Questions.LongReview,
        Grade = fdVM.Form.Questions.Grade
      };
      DatabaseAction.UpdateForm(questions, fdVM.Form.FormURL);
      DatabaseAction.UpdateStatus("Otwarta",fdVM.Form.FormURL);
      return RedirectToAction("Index", "List");
    }


  }
}