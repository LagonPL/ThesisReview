using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Models;

namespace ThesisReview.Data.Interface
{
  public interface IFormRepository
  {
    ApplicationUser GetUser(string mail);

    void AddFormEntity(Form form, string id, string zero, string password, string link);
    void UpdateFormEntity(Questions questions);
    void FinishFormEntity(Questions questions);
  }
}

