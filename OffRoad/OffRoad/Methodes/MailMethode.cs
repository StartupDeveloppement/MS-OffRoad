using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace OffRoad.Methodes
{
    public class MailMethode
    {
        public void SendMail(string to, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient();

            NetworkCredential basicCredential = new NetworkCredential("offroad@vilaine.fr", "offroad2016");

            MailMessage message = new MailMessage();

            MailAddress fromAddress = new MailAddress("offroad@vilaine.fr");

            smtpClient.Host ="smtp.vilaine.com";

            smtpClient.Port = 25;

            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = basicCredential;

            message.From = fromAddress;

            message.Subject = subject;

            message.IsBodyHtml = true;

            message.Body = body;

            message.To.Add(to);

            smtpClient.EnableSsl = false;

            smtpClient.Send(message);
                
        }
    }
}