﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThesisReview.Data.Models;
using ThesisReview.Data.Services;
using ThesisReview.ViewModels;

namespace ThesisReview.Controllers
{
  public class FormController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;

    public FormController( UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
    }

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
      var guid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
      var password = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
      var uri = new UriBuilder
      {
        Scheme = Request.Scheme,
        Host = Request.Host.ToString(),
        Path = "/Form/View/"
      };
      url = StringGenerator.LinkGenerator(uri, guid.ToString(), password.ToString());
      if (ModelState.IsValid)
      {
        DatabaseAction.AddForm(form, guid.ToString(), "0", password.ToString());

        content = "Witaj, udało ci się pomyślnie wysłać zgłoszenie w naszym serwisie. \nLink: " + url;
        EmailSender.Send(form.StudentMail, "Stworzyłeś formularz", content);
        return RedirectToAction("Index", "Home");
      }
      return View(form);

    }

    public async Task<ActionResult> Edit(string id)
    {
      Form form = new Form();
      var mail = await GetCurrentUser();
      form = DatabaseAction.ReadForm(id, mail);
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

    public ActionResult View(string id, string password)
    {
      Form form = new Form();
      form = DatabaseAction.ReadFormView(id, password);
      if(String.IsNullOrEmpty(form.FormURL))
        return RedirectToAction("Error", "Error");
      var suma = new Sum();
      var sumaGuardian = new Sum();
      var questions = StringGenerator.GetQuestions(form.ReviewType);
      if (form.ReviewType.Equals("Praca Magisterska"))
      {
        suma = Util.Sum(form);
        sumaGuardian = Util.SumGuardian(form);
      }
        
      var fdVM = new FormDetailViewModel
      {
        Form = form,
        ReviewType = form.ReviewType,
        QuestionList = questions,
        Answers = StringGenerator.AnswersGenerator(),
        Sum = suma,
        SumGuardian = sumaGuardian
      };

      return View(fdVM);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateForm(FormDetailViewModel fdVM)
    {
      var mail = await GetCurrentUser();
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
      DatabaseAction.UpdateForm(questions, fdVM.Form.FormURL, mail);
      DatabaseAction.UpdateStatus("Otwarta",fdVM.Form.FormURL);
      return RedirectToAction("Index", "List");
    }

    private async Task<string> GetCurrentUser()
    {
      var user = await _userManager.GetUserAsync(HttpContext.User);
      var email = _userManager.GetEmailAsync(user);
      string mail = user.Email;
      return mail;
    }

  }
}