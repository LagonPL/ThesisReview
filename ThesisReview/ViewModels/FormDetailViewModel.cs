using System.Collections.Generic;
using ThesisReview.Data.Models;

namespace ThesisReview.ViewModels
{
  public class FormDetailViewModel
  {
    public Form Form { get; set; }
    public string Mail { get; set; }
    public string Fullname { get; set; }
    public string ReviewType { get; set; }
    public bool Archive { get; set; }
    public Questions QuestionList { get; set; }
    public IEnumerable<Answers> Answers { get; set; }
    public Sum Sum { get; set; }
    public Sum SumGuardian { get; set; }
    public byte[] File { get; set; }
  }
}
