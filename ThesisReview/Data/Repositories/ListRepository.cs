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

    public Form GetFormById(int formId) => _appDbContext.Forms.FirstOrDefault(p => p.FormId == formId);
  }
}
