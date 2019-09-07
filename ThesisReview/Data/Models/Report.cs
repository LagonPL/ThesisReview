using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisReview.Data.Models
{
  public class Report
  {
    public int ReportId { get; set; }
    public String Guardian { get; set; }
    public String Reviewer { get; set; }
    public String Student { get; set; }
    public string Grade { get; set; }
    public DateTime Date { get; set; }
  }
}
