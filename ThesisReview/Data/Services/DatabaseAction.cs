using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisReview.Data.Models;

namespace ThesisReview.Data.Services
{
  public class DatabaseAction
  {
    private readonly static string connectionString = Startup.ConnectionString;


    public static void AddForm(Form form, string id, string zero, string password, string link)
    {
      DateTime dateTime = DateTime.Now;
      string sql;
      if (form.ReviewType.Equals("Praca Magisterska"))
      {
        sql = $"Insert Into Forms (Title, ShortDescription, StudentMail, ReviewerName, GuardianName, FormURL, ReviewType, Status, Password, Link, DateTime) Values ('{form.Title}', '{form.ShortDescription}','{form.StudentMail}','{form.ReviewerName}','{form.GuardianName}','{id}','{form.ReviewType}','{form.Status}','{password}','{link}','{dateTime.ToString("yyyy-MM-dd HH:mm:ss")}');  Insert Into Questions (FormURL, Mail, Question0, Question1, Question2, Question3, Question4, Question5, Question6, Question7, Question8, Question9, Points, Finished) Values ('{id}', '{form.ReviewerName}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{false}'); Insert Into Questions (FormURL, Mail, Question0, Question1, Question2, Question3, Question4, Question5, Question6, Question7, Question8, Question9, Points, Finished) Values ('{id}', '{form.GuardianName}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{zero}', '{false}')";
      }
      else
      {
        sql = $"Insert Into Forms (Title, ShortDescription, StudentMail, ReviewerName, GuardianName, FormURL, ReviewType, Status, Password, Link, DateTime) Values ('{form.Title}', '{form.ShortDescription}','{form.StudentMail}','{form.ReviewerName}','{form.GuardianName}','{id}','{form.ReviewType}','{form.Status}','{password}','{link}','{dateTime.ToString("yyyy-MM-dd HH:mm:ss")}'); Insert Into Questions (FormURL, Mail, Points, Finished) Values ('{id}', '{form.ReviewerName}', '{zero}', '{false}'); Insert Into Questions (FormURL, Mail, Points, Finished) Values ('{id}', '{form.GuardianName}', '{zero}', '{false}')";
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
    
      public static void UpdateForm(Questions questions, string id, string mail, bool isFinish)
      {
      string sql;
      if (isFinish)
      {
        sql = $"Update Questions SET Question1='{questions.Question1}', Question2='{questions.Question2}', Question3='{questions.Question3}', Question4='{questions.Question4}', Question5='{questions.Question5}', Question6='{questions.Question6}', Question7='{questions.Question7}', Question8='{questions.Question8}', Question9='{questions.Question9}', Question0='{questions.Question0}', LongReview='{questions.LongReview}', Grade='{questions.Grade}', Finished='{true}'  Where FormURL='{id}'  And Mail = '{mail}'";
      }
      else
      {
        sql = $"Update Questions SET Question1='{questions.Question1}', Question2='{questions.Question2}', Question3='{questions.Question3}', Question4='{questions.Question4}', Question5='{questions.Question5}', Question6='{questions.Question6}', Question7='{questions.Question7}', Question8='{questions.Question8}', Question9='{questions.Question9}', Question0='{questions.Question0}', LongReview='{questions.LongReview}', Grade='{questions.Grade}'  Where FormURL='{id}'  And Mail = '{mail}'";
      }

      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
          connection.Open();
          command.ExecuteNonQuery();
          connection.Close();
        }
      }
      if (isFinish)
        ReadStatus(id);
      else
      {
        UpdateStatus("Otwarta", id);
      }

    }

    public static void UpdateStatus(string status, string id)
    {
      DateTime dateTime = DateTime.Now;
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        string sql;
        if (status.Equals("Oceniono"))
        {
          sql = $"Update Forms SET Status='{status}', DateTimeFinish='{dateTime.ToString("yyyy-MM-dd HH:mm:ss")}'  Where FormURL='{id}'";
        }
        else
        {
          sql = $"Update Forms SET Status='{status}'  Where FormURL='{id}'";
        }
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
          connection.Open();
          command.ExecuteNonQuery();
          connection.Close();
        }
      }
    }
    public static bool ReadStatus(string id)
    {
      string reviewer = String.Empty;
      string guardian = reviewer;
      string mail = reviewer;
      string url = reviewer;
      string reviewtype = reviewer;
      bool statusReviewer = true;
      bool statusGuardian = false;
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();
        
        string sql = "select * from Forms where Forms.FormURL = '" + id + "'";
        SqlCommand command = new SqlCommand(sql, connection);
        using (SqlDataReader dataReader = command.ExecuteReader())
        {
          while (dataReader.Read())
          {
            reviewer = Convert.ToString(dataReader["ReviewerName"]);
            guardian = Convert.ToString(dataReader["GuardianName"]);
            mail = Convert.ToString(dataReader["StudentMail"]);
            url = Convert.ToString(dataReader["Link"]);
            reviewtype = Convert.ToString(dataReader["ReviewType"]);
          }
        }
        if (reviewtype.Equals("Praca Podyplomowa"))
        {
          UpdateStatus("Oceniono", id);
          EmailSender.Send(mail, "ThesisReview - Zakończono Oceniania", "Zakończono Ocenianie twojego zgłoszenia\nLink: " + url);
          return true;
        }
        sql = "select * from Questions where Questions.FormURL = '" + id + "' And Questions.Mail = '" + reviewer + "'";
        command = new SqlCommand(sql, connection);
        using (SqlDataReader dataReader = command.ExecuteReader())
        {
          while (dataReader.Read())
          {
            statusReviewer = Convert.ToBoolean(dataReader["Finished"]);
          }
        }
        sql = "select * from Questions where  Questions.FormURL = '" + id + "' And  Questions.Mail = '" + guardian + "'";
        command = new SqlCommand(sql, connection);
        using (SqlDataReader dataReader = command.ExecuteReader())
        {
          while (dataReader.Read())
          {
            statusGuardian = Convert.ToBoolean(dataReader["Finished"]);
          }
        }

        connection.Close();
      }
      if (statusGuardian == statusReviewer)
      {
        UpdateStatus("Oceniono", id);
        EmailSender.Send(mail, "ThesisReview - Zakończono Oceniania", "Zakończono Ocenianie twojego zgłoszenia\nLink: " + url);
        return true;
      }
      return false;
    }

    public static Form ReadForm(string id, string mail)
    {
      Form form = new Form();
      Questions questions = new Questions();
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
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
            form.ShortDescription = Convert.ToString(dataReader["ShortDescription"]);
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
            questions.Finished = Convert.ToBoolean(dataReader["Finished"]);
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
