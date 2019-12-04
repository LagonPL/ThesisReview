using ThesisReview.Data.Models;

namespace ThesisReview.Data.Interface
{
  public interface IFormRepository
  {
    ApplicationUser GetUser(string mail);
    Form GetForm(string id);
    Form GetFormView(string mail, string password);
    Form GetFormByMail(string id, string mail);
    void AddFormEntity(Form form, string id, string zero, string password, string link);
    void UpdateFormEntity(Questions questions);
    void FinishFormEntity(Questions questions);
    void ArchiveFormEntity(string id);
    void AddReport(Form form);
  }
}

