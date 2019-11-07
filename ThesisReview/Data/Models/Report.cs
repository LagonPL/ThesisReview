using System;

namespace ThesisReview.Data.Models
{
  public class Report
  {
    public int ReportId { get; set; }
    public String Guardian { get; set; }
    public String Reviewer { get; set; }
    public String Student { get; set; }
    public string GradeGuardian { get; set; }
    public string GradeReviewer {get; set; }
    public DateTime Date { get; set; }
  }
}
