using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Models;

namespace ThesisReview.ViewModels
{
  public class FormViewModel : Form
  {
    public SelectList DepartmentList { get; set; }
    public SelectList ReviewTypeList { get; set; }
    public bool NoError { get; set; }
    public string ErrorMessage { get; set; }

  }
}
