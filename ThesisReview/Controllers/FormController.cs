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
        DatabaseAdder.AddForm("",form.Title, form.ShortDescription, form.StudentMail, form.ReviewerName, form.GuardianName);
        content = "Witaj, udało ci się pomyślnie wysłać zgłoszenie w naszym serwisie.";
        EmailSender.Send(form.StudentMail, "Stworzyłeś formularz", content);
        return RedirectToAction("Index", "Home");
      }
      return View(form);
      
    }

    public ActionResult CreationComplete(string id)
    {
      string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ThesisReview;Trusted_Connection=True;MultipleActiveResultSets=true";
      //List<Form> formList = new List<Form>();
      string formularz = string.Empty;
      string nazwa = string.Empty;
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        //SqlDataReader
        connection.Open();

        string sql = "select * from Forms where FormId = " + id;
        SqlCommand command = new SqlCommand(sql, connection);
        using (SqlDataReader dataReader = command.ExecuteReader())
        {
          while (dataReader.Read())
          {
            Form form = new Form();
            //form.FormId = Convert.ToInt32(dataReader["FormId"]);
            //formularz = Convert.ToString(dataReader["FormId"]);
            nazwa = Convert.ToString(dataReader["Title"]);
          }
        }
        connection.Close();
      }

      var fdVM = new FormDetailViewModel
      {
        Title = nazwa
      };

      return View(fdVM);
    }


  }
}