using System.ComponentModel.DataAnnotations;

namespace ThesisReview.ViewModels
{
  public class SettingViewModel
  {

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public string Token { get; set; }

    [DataType(DataType.Password)]
    public string OldPassword { get; set; }

    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }

    public bool AnyError { get; set; }

  }
}
