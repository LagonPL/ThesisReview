using System.Collections.Generic;
using System.Linq;
using ThesisReview.Data.Interface;
using ThesisReview.Data.Models;

namespace ThesisReview.Data
{
  public class ListRepository : IListRepository
  {

    private readonly AppDbContext _appDbContext;

    public ListRepository(AppDbContext appDbContext)
    {
      _appDbContext = appDbContext;
    }

    public IEnumerable<Form> GetReviewerForms(string mail) => _appDbContext.Forms.Where(p => p.ReviewerName == mail);

    public IEnumerable<Form> GetGuardianForms(string mail) => _appDbContext.Forms.Where(p => p.GuardianName == mail);

    public IEnumerable<Form> GetAll() => _appDbContext.Forms;

    public Form GetFormById(int formId) => _appDbContext.Forms.FirstOrDefault(p => p.FormId == formId);

    public bool isAdmin(string mail)
    {
      var admin = _appDbContext.Users.FirstOrDefault(p => p.Department == "Administrator Główny");
      if (admin.Email.Equals(mail))
        return true;

      return false;

    }
  }
}
