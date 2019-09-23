using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace ThesisReview.Data.Services
{
  public class EmailSender
  {

    private readonly static string MailName = Startup.MailName;
    private readonly static string MailPassword = Startup.MailPassword;
    private readonly static string MailSMTP = Startup.MailSMTP;
    private readonly static string MailPort = Startup.MailPort;

    /// <summary>
    /// Sends mail message.
    /// </summary>
    public static void Send(string receiver, string subject, string content)
    {
      var message = new MimeMessage();
      message.From.Add(new MailboxAddress("Recenzje Prac", MailName));
      message.To.Add(new MailboxAddress(receiver, receiver));
      message.Subject = subject;
      message.Body = new TextPart("plain")
      {
        Text = content
      };

      using (var client = new SmtpClient())
      {
        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
        client.Connect(MailSMTP, Int32.Parse(MailPort) , false);
        client.Authenticate(MailName, MailPassword);
        client.Send(message);
        client.Disconnect(true);
      }
    }
  }
}
