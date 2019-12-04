using System;
using System.IO;
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

    public FormController(UserManager<ApplicationUser> userManager, IFormRepository formRepository)
    {
      _userManager = userManager;
      _formRepository = formRepository;
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
    public async Task<IActionResult> Create(FormViewModel fVM)
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
      using (var memoryStream = new MemoryStream())
      { 
        if (fVM.FileUpload.FormFile.Length < 5242880 && fVM.FileUpload.FormFile.FileName.Contains(".pdf"))
        {
          await fVM.FileUpload.FormFile.CopyToAsync(memoryStream);
          form.ThesisFile = memoryStream.ToArray();
        }
        else
        {
          fVM.NoError = false;
          fVM.ReviewTypeList = new SelectList(StringGenerator.ReviewTypesFiller());
          fVM.DepartmentList = new SelectList(StringGenerator.DepartmentFiller());
          fVM.ErrorMessage = "Niepradłowy plik z pracą";
          return View(fVM);
        }
      }

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
      form = _formRepository.GetFormByMail(id, mail);
      var suma = new Sum();
      
      var questions = StringGenerator.GetQuestions(form.ReviewType);
      if (form.ReviewType.Equals("Praca Magisterska"))
        suma = Util.Sum(form);
      var fdVM = new FormDetailViewModel
      {
        Form = form,
        Mail = mail,
        ReviewType = form.ReviewType,
        QuestionList = questions,
        Answers = StringGenerator.AnswersGenerator(),
        Sum = suma,
        Archive = false
      };
       var span = DateTime.Now.Subtract(form.DateTimeStart);
      if ((int)span.TotalDays > 60 && form.Status != "Oceniono")
      {
        fdVM.Archive = true;
      }
      return View(fdVM);
    }

    public ActionResult View(string id, string password)
    {
      Form form = new Form();
      TimeSpan span;
      DateTime dateTime = DateTime.Now;
      form = _formRepository.GetFormView(id, password);
      if (form == null)
        return RedirectToAction("Error", "Error", new { @statusCode = 1 });
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
        SumGuardian = sumaGuardian,
        Archive = false
      };
      span = form.DateTimeStart.Subtract(dateTime);
      if ((int)span.TotalDays > 60)
      {
        fdVM.Archive = true;
      }

      return View(fdVM);
    }

    [HttpPost]
    public IActionResult DownloadFile(FormDetailViewModel fdVM)
    {
      var form = _formRepository.GetForm(fdVM.Form.FormURL);
      string filename = form.StudentName.Replace(" ", "") + form.DateTimeStart.Year.ToString() + ".pdf";
      return File(form.ThesisFile, "application/force-download", filename);
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
        Mail = mail,
        Status = "Otwarto"
      };
      _formRepository.UpdateFormEntity(questions);
      return RedirectToAction("Index", "List");
    }

    [HttpPost]
    public IActionResult ArchiveForm(FormDetailViewModel fdVM)
    {
      _formRepository.ArchiveFormEntity(fdVM.Form.FormURL);
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
        Finished = true,
        Status = "Oceniono"
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
      UserList user1;
      UserList user2;
      if (reviewtype.Equals("Praca Podyplomowa"))
      {
        user1 = _formRepository.GetUser(mail2);
        try
        {
          if (String.IsNullOrEmpty(user1.Mail))
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
        if (String.IsNullOrEmpty(user1.Mail) || String.IsNullOrEmpty(user2.Mail) || user1.Equals(user2))
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