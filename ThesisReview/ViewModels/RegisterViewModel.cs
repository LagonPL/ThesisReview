using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ThesisReview.ViewModels
{
  public class RegisterViewModel
  {
    [Required]
    [Display(Name = "User Name")]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    public string Fullname { get; set; }
    public string Department { get; set; }
    public string Title { get; set; }
    public SelectList Departments { get; set; }
    public SelectList Titles { get; set; }
    public string ReturnUrl { get; set; }
    public bool IsAdmin { get; set; }
  }
}
