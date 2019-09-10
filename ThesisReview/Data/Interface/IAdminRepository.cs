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
    void DeleteRequest(string email);
    IEnumerable<ApplicationUser> GetAllUser();
    IEnumerable<ApplicationUser> GetAllUserNoYou(string user);
    IEnumerable<Report> GetReports(DateTime datestart, DateTime datefinish);
    IEnumerable<RequestForm> GetRequest();
  }
}
