using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
