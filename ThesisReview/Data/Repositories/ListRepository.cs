using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public IEnumerable<Form> GetYourForms(string mail) => _appDbContext.Forms.Where(p => p.ReviewerName == mail);

    public Form GetFormById(int formId) => _appDbContext.Forms.FirstOrDefault(p => p.FormId == formId);
  }
}
