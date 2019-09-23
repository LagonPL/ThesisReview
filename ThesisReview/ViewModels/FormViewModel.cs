using Microsoft.AspNetCore.Mvc.Rendering;
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
