using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Models;

namespace ThesisReview.ViewModels
{
  public class ReportViewModel
  {
    public IEnumerable<Report> Reports { get; set; }
  }
}
