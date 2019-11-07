using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ThesisReview.ViewModels
{
  public class RequestViewModel
  {
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    public string Fullname { get; set; }
    public string Department { get; set; }
    public string Title { get; set; }
    public SelectList Titles { get; set; }
    public SelectList Departments { get; set; }

  }
}
