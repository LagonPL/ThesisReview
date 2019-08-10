using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Models;

namespace ThesisReview.Data.Services
{
  public class DatabaseAction
  {

    private readonly static string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ThesisReview;Trusted_Connection=True;MultipleActiveResultSets=true";

    public static void AddForm(Form form, string url)
    {

      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        string sql = $"Insert Into Forms (Title, ShortDescription, StudentMail, ReviewerName, GuardianName, FormURL, ReviewType, Status) Values ('{form.Title}', '{form.ShortDescription}','{form.StudentMail}','{form.ReviewerName}','{form.GuardianName}','{url}','{form.ReviewType}','{form.Status}'); Insert Into Questions (FormURL, Mail) Values ('{url}', '{form.ReviewerName}'); Insert Into Questions (FormURL, Mail) Values ('{url}', '{form.GuardianName}')";
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
          command.CommandType = CommandType.Text;
          connection.Open();
          command.ExecuteNonQuery();
          connection.Close();
        }
      }
    }

    public static void UpdateForm(Questions questions, string url)
    {

      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        string sql = $"Update Questions SET Question1='{questions.Question1}', Question2='{questions.Question2}', Question3='{questions.Question3}', Question4='{questions.Question4}', Question5='{questions.Question5}', Question6='{questions.Question6}', Question7='{questions.Question7}', Question8='{questions.Question8}', LongReview='{questions.LongReview}', Grade='{questions.Grade}'  Where FormURL='{url}'";
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
          connection.Open();
          command.ExecuteNonQuery();
          connection.Close();
        }
      }
    }

    public static void UpdateStatus(string status, string url)
    {

      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        string sql = $"Update Forms SET Status='{status}'  Where FormURL='{url}'";
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
          connection.Open();
          command.ExecuteNonQuery();
          connection.Close();
        }
      }
    }


    public static Form ReadForm(string id)
    {
      Form form = new Form();
      Questions questions = new Questions();
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        //SqlDataReader
        connection.Open();

        string sql = "select * from Forms, Questions where Forms.FormURL = '" + id + "' And Questions.FormURL = '" + id + "'";
        SqlCommand command = new SqlCommand(sql, connection);
        using (SqlDataReader dataReader = command.ExecuteReader())
        {
          while (dataReader.Read())
          {
            form.FormURL = Convert.ToString(dataReader["FormURL"]);
            form.Title = Convert.ToString(dataReader["Title"]);
            form.StudentMail = Convert.ToString(dataReader["StudentMail"])
            form.ReviewerName = Convert.ToString(dataReader["ReviewerName"]);
            form.GuardianName = Convert.ToString(dataReader["GuardianName"]);
            form.ReviewType = Convert.ToString(dataReader["ReviewType"]);
            questions.Question1 = Convert.ToString(dataReader["Question1"]);
            questions.Question2 = Convert.ToString(dataReader["Question2"]);
            questions.Question3 = Convert.ToString(dataReader["Question3"]);
            questions.Question4 = Convert.ToString(dataReader["Question4"]);
            questions.Question5 = Convert.ToString(dataReader["Question5"]);
            questions.Question6 = Convert.ToString(dataReader["Question6"]);
            questions.Question7 = Convert.ToString(dataReader["Question7"]);
            questions.Question8 = Convert.ToString(dataReader["Question8"]);
            questions.LongReview = Convert.ToString(dataReader["LongReview"]);
            questions.Grade = Convert.ToString(dataReader["Grade"]);
            form.Questions = questions;
          }
        }
        connection.Close();
      }
      return form;
    }


  }
}
