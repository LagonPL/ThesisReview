using System.Collections.Generic;
using ThesisReview.Data.Models;

namespace ThesisReview.ViewModels
{
  public class ListViewModel
  {

    public IEnumerable<Form> Forms { get; set; }
    public IEnumerable<Form> ArchiveForms { get; set; }
    public string mail { get; set; }
    public bool IsOrder { get; set; }

  }
}
