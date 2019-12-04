using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using ThesisReview.Data.Models;

namespace ThesisReview.ViewModels
{
  public class FormViewModel : Form
  {
    public SelectList DepartmentList { get; set; }
    public SelectList ReviewTypeList { get; set; }
    public bool NoError { get; set; }
    public string ErrorMessage { get; set; }
    [BindProperty]
    public BufferedSingleFileUploadDb FileUpload { get; set; }

  }

  public class BufferedSingleFileUploadDb
  {
    [Required]
    [Display(Name = "File")]
    public IFormFile FormFile { get; set; }
  }
}
