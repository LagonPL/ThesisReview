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
    [StringLength(10)]
    public string Title { get; set; }
    [Required]
    public string ReviewType { get; set; }
    [Required]
    [StringLength(10)]
    public string ShortDescription { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string StudentMail { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    //[StringLength(10)]
    public string ReviewerName { get; set; }
    [Required]
    //[DataType(DataType.EmailAddress)]
    //[StringLength(10)]
    public string GuardianName { get; set; }
    public string FormURL { get; set; }
    

  }
}
