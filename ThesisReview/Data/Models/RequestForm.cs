using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisReview.Data.Models
{
  public class RequestForm
  {
    public int RequestFormId { get; set; }
    public string Email { get; set; }
    public string Fullname { get; set; }
    public string Department { get; set; }
  }
}
