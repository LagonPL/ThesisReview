using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisReview.Data.Services
{
  public class EmailSender
  {
    public static void Send(string receiver, string subject, string content)
    {
      var message = new MimeMessage();
      message.From.Add(new MailboxAddress("Recenzje Prac", "lagonamv@gmail.com"));
      message.To.Add(new MailboxAddress(receiver, receiver));
      message.Subject = subject;
      message.Body = new TextPart("plain")
      {
        Text = content
      };

      using (var client = new SmtpClient())
      {
        client.Connect("smtp.gmail.com", 587, false);
        client.Authenticate("lagonamv@gmail.com", "xxxx");
        client.Send(message);
        client.Disconnect(true);
      }
    }
  }
}
