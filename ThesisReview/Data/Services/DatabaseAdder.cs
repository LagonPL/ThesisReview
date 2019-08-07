using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisReview.Data.Services
{
  public class DatabaseAdder
  {

    private readonly static string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ThesisReview;Trusted_Connection=True;MultipleActiveResultSets=true";

    public static void AddForm(string type, string title, string shortdesc, string studentmail, string reviewer, string guardian)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        string sql = $"Insert Into Forms (Title, ShortDescription, StudentMail, ReviewerName, GuardianName) Values ('{title}', '{shortdesc}','{studentmail}','{reviewer}','{guardian}')"; using (SqlCommand command = new SqlCommand(sql, connection))
        {
          command.CommandType = CommandType.Text;
          connection.Open();
          command.ExecuteNonQuery();
          connection.Close();
        }
      }
    }


  }
}
