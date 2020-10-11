using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CaseWebApp.Models
{
    public class Tools
    {
        public string SendMail(string toMail, string body, string subject)//SendMail("wee@sd.com;sdf@dfsdf.com","wee@sd.com;sdf@dfsdf.com")
        {
            string sonuc = "";
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("testing@creaworkers.com", "YIqk08E1");
            client.Host = "mail.creaworkers.com";
            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.Timeout = 600000;
            client.EnableSsl = false;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("testing@creaworkers.com");

            mailMessage.To.Add(toMail);

            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;

            mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html));
            mailMessage.Subject = subject;

            try
            {
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                sonuc = ex.ToString();
            }
            return sonuc;
        }

    }
}
