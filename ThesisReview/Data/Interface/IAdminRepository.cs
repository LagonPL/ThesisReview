using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Models;

namespace ThesisReview.Data.Interface
{
  public interface IAdminRepository
  {

    void DeleteUser(string useId);
    void EditUser(string useId);

    IEnumerable<ApplicationUser> GetAllUser();
    IEnumerable<ApplicationUser> GetAllUserNoYou(string user);
    IEnumerable<Report> GetReports(string datestart, string datefinish);
  }
}
