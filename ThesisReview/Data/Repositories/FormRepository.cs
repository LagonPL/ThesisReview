using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Interface;
using ThesisReview.Data.Models;

namespace ThesisReview.Data.Repositories
{
  public class FormRepository : IFormRepository
  {
    private readonly AppDbContext _appDbContext;

    public FormRepository(AppDbContext appDbContext)
    {
      _appDbContext = appDbContext;
    }

    public ApplicationUser GetUser(string mail) => _appDbContext.Users.FirstOrDefault(p => p.Email == mail);
  }
}
