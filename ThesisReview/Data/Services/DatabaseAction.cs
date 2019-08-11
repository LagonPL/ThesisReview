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

    public static void AddForm(Form form, string url, string zero, string password)
    {
      string sql;
      if(form.ReviewType.Equals("Praca Magisterska"))
      {
        sql = $"Insert Into Forms (Title, ShortDescription, StudentMail, ReviewerName, GuardianName, FormURL, ReviewType, Status, Password) Values ('{form.Title}', '{form.ShortDescription}','{form.StudentMail}','{form.ReviewerName}','{form.GuardianName}','{url}','{form.ReviewType}','{form.Status}','{password}');  Insert Into Questions (FormURL, Mail, Question0, Question1, Question2, Question3, Question4, Question5, Question6, Question7, Question8, Question9, Points) Values ('{url}', '{form.ReviewerName}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}'); Insert Into Questions (FormURL, Mail, Question0, Question1, Question2, Question3, Question4, Question5, Question6, Question7, Question8, Question9, Points) Values ('{url}', '{form.GuardianName}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}')";
      }
      else
      {
        sql = $"Insert Into Forms (Title, ShortDescription, StudentMail, ReviewerName, GuardianName, FormURL, ReviewType, Status, Password) Values ('{form.Title}', '{form.ShortDescription}','{form.StudentMail}','{form.ReviewerName}','{form.GuardianName}','{url}','{form.ReviewType}','{form.Status}','{password}'); Insert Into Questions (FormURL, Mail, Points) Values ('{url}', '{form.ReviewerName}', '{zero}'); Insert Into Questions (FormURL, Mail, Points) Values ('{url}', '{form.GuardianName}', '{zero}')";
      }
      
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
          command.CommandType = CommandType.Text;
          connection.Open();
          command.ExecuteNonQuery();
          connection.Close();
        }
      }
    }

    public static void UpdateForm(Questions questions, string url, string mail)
    {

      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        string sql = $"Update Questions SET Question1='{questions.Question1}', Question2='{questions.Question2}', Question3='{questions.Question3}', Question4='{questions.Question4}', Question5='{questions.Question5}', Question6='{questions.Question6}', Question7='{questions.Question7}', Question8='{questions.Question8}', Question9='{questions.Question9}', Question0='{questions.Question0}', LongReview='{questions.LongReview}', Grade='{questions.Grade}'  Where FormURL='{url}'  And Mail = '{mail}'";
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


    public static Form ReadForm(string id, string mail)
    {
      Form form = new Form();
      Questions questions = new Questions();
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        //SqlDataReader
        connection.Open();

        string sql = "select * from Forms, Questions where Forms.FormURL = '" + id + "' And Questions.FormURL = '" + id + "' And Questions.Mail = '" + mail + "'";
        SqlCommand command = new SqlCommand(sql, connection);
        using (SqlDataReader dataReader = command.ExecuteReader())
        {
          while (dataReader.Read())
          {
            form.FormURL = Convert.ToString(dataReader["FormURL"]);
            form.Title = Convert.ToString(dataReader["Title"]);
            form.StudentMail = Convert.ToString(dataReader["StudentMail"]);
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
            questions.Question9 = Convert.ToString(dataReader["Question9"]);
            questions.Question0 = Convert.ToString(dataReader["Question0"]);
            questions.LongReview = Convert.ToString(dataReader["LongReview"]);
            questions.Grade = Convert.ToString(dataReader["Grade"]);
            form.Questions = questions;
          }
        }
        connection.Close();
      }
      return form;
    }
    public static Form ReadFormView(string id, string password)
    {
      Form form = new Form();
      Questions questions = new Questions();
      Questions questions2 = new Questions();
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        //SqlDataReader
        connection.Open();
        string sql1 = "select * from Forms where Forms.FormURL = '" + id + "' And Forms.Password = '" + password + "'";
        SqlCommand command = new SqlCommand(sql1, connection);
        using (SqlDataReader dataReader = command.ExecuteReader())
        {
          while (dataReader.Read())
          {
            form.FormURL = Convert.ToString(dataReader["FormURL"]);
            form.Title = Convert.ToString(dataReader["Title"]);
            form.StudentMail = Convert.ToString(dataReader["StudentMail"]);
            form.ReviewerName = Convert.ToString(dataReader["ReviewerName"]);
            form.GuardianName = Convert.ToString(dataReader["GuardianName"]);
            form.ReviewType = Convert.ToString(dataReader["ReviewType"]);
          }
        }
        string sql2 = "select * from Questions where Questions.FormURL = '" + id + "' And Questions.Mail = '" + form.ReviewerName + "'";
        string sql3 = "select * from Questions where Questions.FormURL = '" + id + "' And Questions.Mail = '" + form.GuardianName + "'";
        command = new SqlCommand(sql2, connection);
        using (SqlDataReader dataReader = command.ExecuteReader())
        {
          while (dataReader.Read())
          {
            questions.Question1 = Convert.ToString(dataReader["Question1"]);
            questions.Question2 = Convert.ToString(dataReader["Question2"]);
            questions.Question3 = Convert.ToString(dataReader["Question3"]);
            questions.Question4 = Convert.ToString(dataReader["Question4"]);
            questions.Question5 = Convert.ToString(dataReader["Question5"]);
            questions.Question6 = Convert.ToString(dataReader["Question6"]);
            questions.Question7 = Convert.ToString(dataReader["Question7"]);
            questions.Question8 = Convert.ToString(dataReader["Question8"]);
            questions.Question9 = Convert.ToString(dataReader["Question9"]);
            questions.Question0 = Convert.ToString(dataReader["Question0"]);
            questions.LongReview = Convert.ToString(dataReader["LongReview"]);
            questions.Grade = Convert.ToString(dataReader["Grade"]);
            form.Questions = questions;
          }
        }
        command = new SqlCommand(sql3, connection);
        using (SqlDataReader dataReader = command.ExecuteReader())
        {
          while (dataReader.Read())
          {
            questions2.Question1 = Convert.ToString(dataReader["Question1"]);
            questions2.Question2 = Convert.ToString(dataReader["Question2"]);
            questions2.Question3 = Convert.ToString(dataReader["Question3"]);
            questions2.Question4 = Convert.ToString(dataReader["Question4"]);
            questions2.Question5 = Convert.ToString(dataReader["Question5"]);
            questions2.Question6 = Convert.ToString(dataReader["Question6"]);
            questions2.Question7 = Convert.ToString(dataReader["Question7"]);
            questions2.Question8 = Convert.ToString(dataReader["Question8"]);
            questions2.Question9 = Convert.ToString(dataReader["Question9"]);
            questions2.Question0 = Convert.ToString(dataReader["Question0"]);
            questions2.LongReview = Convert.ToString(dataReader["LongReview"]);
            questions2.Grade = Convert.ToString(dataReader["Grade"]);
            form.QuestionsGuardian = questions2;
          }
        }
        connection.Close();
      }
      return form;
    }


  }
}
