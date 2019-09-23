using System.Collections.Generic;
using ThesisReview.Data.Models;

namespace ThesisReview.Data.Interface
{
  public interface IListRepository
  {
    IEnumerable<Form> GetReviewerForms(string mail);
    IEnumerable<Form> GetGuardianForms(string mail);
    Form GetFormById(int formId);
  }
}
