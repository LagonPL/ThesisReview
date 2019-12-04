using System;
using System.Collections.Generic;
using ThesisReview.Data.Models;

namespace ThesisReview.Data.Interface
{
  public interface IAdminRepository
  {

    void DeleteUser(string useId);
    void ActivateUser(string useId);
    void DeleteRequest(string email);
    IEnumerable<ApplicationUser> GetAllUser();
    IEnumerable<ApplicationUser> GetAllUserNoYou(string user);
    IEnumerable<Report> GetReports(DateTime datestart, DateTime datefinish);
    IEnumerable<RequestForm> GetRequest();
  }
}
