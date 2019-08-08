using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Models;

namespace ThesisReview.Data.Services
{
  public class DatabaseAdder
  {

    private readonly static string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ThesisReview;Trusted_Connection=True;MultipleActiveResultSets=true";

    public static void AddForm(string type, string title, string shortdesc, string studentmail, string reviewer, string guardian, string url)
    {

      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        string sql = $"Insert Into Forms (Title, ShortDescription, StudentMail, ReviewerName, GuardianName, FormURL) Values ('{title}', '{shortdesc}','{studentmail}','{reviewer}','{guardian}','{url}')"; using (SqlCommand command = new SqlCommand(sql, connection))
        {
          command.CommandType = CommandType.Text;
          connection.Open();
          command.ExecuteNonQuery();
          connection.Close();
        }
      }
    }

    public static Form ReadForm(string id)
    {
      Form form = new Form();
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        //SqlDataReader
        connection.Open();

        string sql = "select * from Forms where FormURL = '" + id + "'";
        SqlCommand command = new SqlCommand(sql, connection);
        using (SqlDataReader dataReader = command.ExecuteReader())
        {
          while (dataReader.Read())
          {
            form.FormURL = Convert.ToString(dataReader["FormURL"]);
            form.Title = Convert.ToString(dataReader["Title"]);
          }
        }
        connection.Close();
      }
      return form;
    }


  }
}
