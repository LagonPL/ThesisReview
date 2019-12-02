using System.Collections.Generic;
using ThesisReview.Data.Models;

namespace ThesisReview.Data.Interface
{
  public interface IListRepository
  {
    bool isAdmin(string mail);
    IEnumerable<Form> GetReviewerForms(string mail);
    IEnumerable<Form> GetGuardianForms(string mail);
    IEnumerable<Form> GetAll();
    Form GetFormById(int formId);
  }
}
