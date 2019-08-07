﻿using System;
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
    

  }
}