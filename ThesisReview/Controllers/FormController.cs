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
      string content;
      if (ModelState.IsValid)
      {
        AddForm(form.Title, form.ShortDescription, form.StudentMail, form.ReviewerName, form.GuardianName);
        content = "Witaj, udało ci się pomyślnie wysłać zgłoszenie w naszym serwisie.";
        EmailSender.Send(form.StudentMail, "Stworzyłeś formularz", content);
        return RedirectToAction("Index", "Home");
      }
      else
      {
        ViewData["Message"] = "Your application description page.";
      }
      return View(form);
      
    }

    public void AddForm(string title, string sc, string studentmail, string reviewer, string guardian)
    {
      using (SqlConnection connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=ThesisReview;Trusted_Connection=True;MultipleActiveResultSets=true"))
      {
        string sql = $"Insert Into Forms (Title, ShortDescription, StudentMail, ReviewerName, GuardianName) Values ('{title}', '{sc}','{studentmail}','{reviewer}','{guardian}')"; using (SqlCommand command = new SqlCommand(sql, connection))
        {
          command.CommandType = CommandType.Text;
          connection.Open();
          command.ExecuteNonQuery();
          connection.Close();
        }
      }

    }


  }
}