using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisReview.Data.Models
{
  public class Form
  {
    public int FormId { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    [Required]
    public string ReviewType { get; set; }

    [Required]
    public string ShortDescription { get; set; }

    public string Status { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string StudentMail { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string ReviewerName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string GuardianName { get; set; }

    public string FormURL { get; set; }

    public string Password { get; set; }

    public Questions Questions { get; set; }

    public Questions QuestionsGuardian { get; set; }
  }
}
