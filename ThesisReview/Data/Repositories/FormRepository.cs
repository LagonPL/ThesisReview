using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ThesisReview.Data.Interface;
using ThesisReview.Data.Models;
using ThesisReview.Data.Services;

namespace ThesisReview.Data.Repositories
{
  public class FormRepository : IFormRepository
  {
    private readonly AppDbContext _appDbContext;

    public FormRepository(AppDbContext appDbContext)
    {
      _appDbContext = appDbContext;
    }

    public ApplicationUser GetUser(string mail) => _appDbContext.Users.FirstOrDefault(p => p.Email == mail);

    public Form GetForm(string id) => _appDbContext.Forms.FirstOrDefault(p => p.FormURL == id);

    public Form GetFormView(string id, string password)
    {
      _appDbContext.Forms.Load();
      var form = _appDbContext.Forms.Where(p => (p.FormURL == id) && (p.Password == password)).Include(b => b.Questions).Include(b => b.QuestionsGuardian).FirstOrDefault();
      if (form == null)
        return form;

      return form;
    }

    public Form GetFormByMail(string id, string mail)
    {
      var form = _appDbContext.Forms.FirstOrDefault(p => p.FormURL == id);
      if (form == null)
        return form;
      var question = _appDbContext.Questions.FirstOrDefault(p => (p.Mail == mail) && (p.FormURL == form.FormURL));
      form.Questions = question;
      return form;
    }

    public void AddFormEntity(Form form, string id, string zero, string password, string link)
    {
      DateTime dateTime = DateTime.Now;
      form.DateTimeStart = dateTime;
      form.Link = link;
      form.Password = password;
      form.FormURL = id;
      if (form.ReviewType.Equals("Praca Magisterska")) {
        form.QuestionsGuardian = StringGenerator.AdvanceTemplate(id, form.GuardianName);
        form.Questions = StringGenerator.AdvanceTemplate(id, form.ReviewerName);
      }
      else if (form.ReviewType.Equals("Praca Podyplomowa"))
      {
        form.QuestionsGuardian = StringGenerator.BasicTemplate(id, form.GuardianName);
      }
      else 
      {
        form.QuestionsGuardian = StringGenerator.BasicTemplate(id, form.GuardianName);
        form.Questions = StringGenerator.BasicTemplate(id, form.ReviewerName);
      }
      _appDbContext.Forms.Add(form);
      _appDbContext.SaveChanges();
    }

    public void UpdateFormEntity(Questions questions)
    {
      var result = _appDbContext.Questions.SingleOrDefault(b => (b.FormURL == questions.FormURL) && (b.Mail == questions.Mail));
      result.Question0 = questions.Question0;
      result.Question1 = questions.Question1;
      result.Question2 = questions.Question2;
      result.Question3 = questions.Question3;
      result.Question4 = questions.Question4;
      result.Question5 = questions.Question5;
      result.Question6 = questions.Question6;
      result.Question7 = questions.Question7;
      result.Question8 = questions.Question8;
      result.Question9 = questions.Question9;
      result.Points = questions.Points;
      result.LongReview = questions.LongReview;
      result.Grade = questions.Grade;
      result.Finished = questions.Finished;
      result.Status = questions.Status;

      var form = _appDbContext.Forms.FirstOrDefault(b => b.FormURL == questions.FormURL);
      form.Status = "Otwarto";
      _appDbContext.SaveChanges();

    }
    public void FinishFormEntity(Questions questions)
    {
      var result = _appDbContext.Questions.SingleOrDefault(b => (b.FormURL == questions.FormURL) && (b.Mail == questions.Mail));
      DateTime dateTime = DateTime.Now;
      result.Question0 = questions.Question0;
      result.Question1 = questions.Question1;
      result.Question2 = questions.Question2;
      result.Question3 = questions.Question3;
      result.Question4 = questions.Question4;
      result.Question5 = questions.Question5;
      result.Question6 = questions.Question6;
      result.Question7 = questions.Question7;
      result.Question8 = questions.Question8;
      result.Question9 = questions.Question9;
      result.Points = questions.Points;
      result.LongReview = questions.LongReview;
      result.Grade = questions.Grade;
      result.Status = questions.Status;
      result.Finished = questions.Finished;
      var form = _appDbContext.Forms.FirstOrDefault(b => b.FormURL == questions.FormURL);
      var otherQuestion = _appDbContext.Questions.FirstOrDefault(b => (b.FormURL == questions.FormURL) && (b.Mail != questions.Mail));
      if (otherQuestion == null || otherQuestion.Finished)
      {
        form.Status = "Oceniono";
        form.DateTimeFinish = dateTime;
        EmailSender.Send(form.StudentMail, "ThesisReview - Zakończono Oceniania", "Zakończono Ocenianie twojego zgłoszenia\nOcena końcowa: " + result.Grade+ " oraz " + questions.Grade +"\nLink: " + form.Link);
        AddReport(form);
      }
      _appDbContext.SaveChanges();
    }
    public void AddReport(Form form)
    {
      string reviewer, grade;
      var user1 = _appDbContext.UserLists.FirstOrDefault(p => p.Mail == form.GuardianName);
      var user2 = _appDbContext.UserLists.FirstOrDefault(p => p.Mail == form.ReviewerName);
      var question1 = _appDbContext.Questions.FirstOrDefault(p => (p.FormURL == form.FormURL) && (p.Mail == user1.Mail));
      
      if (user2 == null)
      {
        reviewer = "";
        grade = "";
      }
      else
      {
        reviewer = user2.Fullname;
        var question2 = _appDbContext.Questions.FirstOrDefault(p => (p.FormURL == form.FormURL) && (p.Mail == user2.Mail));
        grade = question2.Grade;
      }
      Report report = new Report
      {
        Date = form.DateTimeFinish,
        GradeGuardian = question1.Grade,
        Student = form.StudentMail + " - " + form.StudentName,
        Guardian = user1.Fullname,
        Reviewer = reviewer,
        GradeReviewer = grade,
        Form = form
      };
      
      _appDbContext.Reports.Add(report);
      _appDbContext.SaveChanges();
    }

    public void ArchiveFormEntity(string id)
    {
      var form = _appDbContext.Forms.FirstOrDefault(p => p.FormURL == id);
      form.Status = "Oceniono";
      _appDbContext.SaveChanges();
    }

  }
}
