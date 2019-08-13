using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Models;

namespace ThesisReview.ViewModels
{
  public class ListViewModel
  {

    public IEnumerable<Form> Forms { get; set; }
    public IEnumerable<Form> ArchiveForms { get; set; }
    public bool IsOrder { get; set; }

  }
}
