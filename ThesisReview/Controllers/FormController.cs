﻿using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThesisReview.Data;
using ThesisReview.Data.Interface;
using ThesisReview.Data.Models;
using ThesisReview.Data.Services;
using ThesisReview.ViewModels;

namespace ThesisReview.Controllers
{
  public class FormController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IFormRepository _formRepository;
    private readonly AppDbContext _appDbContext;

    public FormController(UserManager<ApplicationUser> userManager, IFormRepository formRepository, AppDbContext appDbContext)
    {
      _userManager = userManager;
      _formRepository = formRepository;
      _appDbContext = appDbContext;
    }

    public async Task<IActionResult> Create()
    {
      var mail = await GetCurrentUser();
      var fVM = new FormViewModel
      {
        ReviewTypeList = new SelectList(StringGenerator.ReviewTypesFiller()),
        DepartmentList = new SelectList(StringGenerator.DepartmentFiller()),
        NoError = true,
        StudentMail = mail
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
          GuardianName = fVM.GuardianName,
          Department = fVM.Department,
          StudentName = fVM.StudentName
        };
      if (ModelState.IsValid)
      {
        if (!EmailExist(fVM.ReviewerName, fVM.GuardianName, fVM.ReviewType))
        {
          fVM.NoError = false;
          fVM.ReviewTypeList = new SelectList(StringGenerator.ReviewTypesFiller());
          fVM.DepartmentList = new SelectList(StringGenerator.DepartmentFiller());
          fVM.ErrorMessage = "Brakuje maili w bazie lub mail opiekuna i recenzenta jest taki sam";
          return View(fVM);
        }
        if(form.ReviewType.Equals("Praca Podyplomowa"))
        {
          form.ReviewerName = "";
        }
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

        content = "Witaj " + fVM.StudentName + "\nUdało ci się pomyślnie wysłać zgłoszenie w naszym serwisie. \nLink: " + url;
        EmailSender.Send(form.StudentMail, "ThesisReview - Stworzyłeś formularz", content);
       
        _formRepository.AddFormEntity(form, guid.ToString(), "0", password.ToString(), url);
        return RedirectToAction("Index", "Home", new { @id = form.FormURL });
      }
      fVM.ReviewTypeList = new SelectList(StringGenerator.ReviewTypesFiller());
      fVM.DepartmentList = new SelectList(StringGenerator.DepartmentFiller());
      fVM.NoError = false;
      fVM.ErrorMessage = "Źle wypełniony formularz";
      return View(fVM);

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
        mail = mail,
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
      if (String.IsNullOrEmpty(form.FormURL))
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
        Grade = fdVM.Form.Questions.Grade,
        FormURL = fdVM.Form.FormURL,
        Mail = mail
      };
      _formRepository.UpdateFormEntity(questions);
      return RedirectToAction("Index", "List");
    }

    [HttpPost]
    public async Task<IActionResult> FinishForm(FormDetailViewModel fdVM)
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
        Grade = fdVM.Form.Questions.Grade,
        FormURL = fdVM.Form.FormURL,
        Mail = mail,
        Finished = true
      };
      _formRepository.FinishFormEntity(questions);
      return RedirectToAction("Index", "List");
    }

    private async Task<string> GetCurrentUser()
    {
      var user = await _userManager.GetUserAsync(HttpContext.User);
      var email = _userManager.GetEmailAsync(user);
      string mail = "";
      try
      {
        mail = user.Email;
      }
      catch (NullReferenceException)
      {
        return mail;
      }

      return mail;
    }

    public bool EmailExist(string mail1, string mail2, string reviewtype)
    {
      ApplicationUser user1;
      ApplicationUser user2;
      if (reviewtype.Equals("Praca Podyplomowa"))
      {
        user1 = _formRepository.GetUser(mail2);
        try
        {
          if (String.IsNullOrEmpty(user1.Email))
          {
            return false;
          }
        }
        catch (NullReferenceException)
        {
          return false;
        }
        return true;
      }
      user1 = _formRepository.GetUser(mail1);
      user2 = _formRepository.GetUser(mail2);
      try
      {
        if (String.IsNullOrEmpty(user1.Email) || String.IsNullOrEmpty(user2.Email) || user1.Equals(user2))
        {
          return false;
        }
      }
      catch (NullReferenceException)
      {
        return false;
      }
      return true;
    }
  }
}