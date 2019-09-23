using Microsoft.AspNetCore.Identity;

namespace ThesisReview.Data.Models
{
  public class ApplicationUser : IdentityUser
  {
    public string Department { get; set; }
    public string Fullname { get; set; }
  }
}
