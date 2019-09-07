using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

      DatabaseAction.UpdateStatus("Otwarta", questions.FormURL);
      _appDbContext.SaveChanges();

    }
    public void FinishFormEntity(Questions questions)
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
      _appDbContext.SaveChanges();
      bool isOver = DatabaseAction.ReadStatus(questions.FormURL);
      if (isOver)
      {
        AddReport(questions.FormURL);
      }
    }
    public void AddReport(string id)
    {
      string reviewer;
      var form = _appDbContext.Forms.FirstOrDefault(p => p.FormURL == id);
      var user1 = _appDbContext.UserLists.FirstOrDefault(p => p.Mail == form.GuardianName);
      var user2 = _appDbContext.UserLists.FirstOrDefault(p => p.Mail == form.ReviewerName);
      var question = _appDbContext.Questions.FirstOrDefault(p => (p.FormURL == id) && (p.Mail == user1.Mail));
      if (user2 == null)
      {
        reviewer = "";
      }
      else
      {
        reviewer = user2.Fullname;
      }
      Report report = new Report
      {
        Date = form.DateTimeFinish,
        Grade = question.Grade,
        Student = form.StudentMail,
        Guardian = user1.Fullname,
        Reviewer = reviewer
      };
      
      _appDbContext.Reports.Add(report);
      _appDbContext.SaveChanges();
    }
  }
}
