using System.ComponentModel.DataAnnotations;

namespace ThesisReview.ViewModels
{
  public class LogInViewModel
  {
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string Fullname { get; set; }
    public string ReturnUrl { get; set; }
  }
}
