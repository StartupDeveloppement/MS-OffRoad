using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace OffRoad.Methodes
{
    public class MailMethode
    {
        public void SendMail(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress("offroad@vilaine.fr");
            mail.Subject = subject;
            string Body = body;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.vilaine.com";
            smtp.Port = 25;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential ("offroad@vilaine.fr", "offroad2016");
            // Enter seders User name and password 
            smtp.EnableSsl = false;
            smtp.Send(mail); 
        }
    }
}