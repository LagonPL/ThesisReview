using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    public string Department { get; set; }
    public SelectList Departments { get; set; }
    public string ReturnUrl { get; set; }
    public bool IsAdmin { get; set; }
  }
}
