using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Projec.COMMON.MyTools
{
    public static class MailSender
    {
      
        public static void Send(string receiver, string password = "Frkn511247+", string body = "Deneme", string subject = "Test", string sender = "furkanpython@gmail.com")
        {

            MailAddress senderEmail = new MailAddress(sender);

            MailAddress receiverEmail = new MailAddress(receiver);//burada item.Email kullanılır


            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };




            using (var mesaj = new MailMessage(senderEmail, receiverEmail)
            {
                //Object initializer
                Subject = subject,
                Body =body
            })
            {
                //using scope'u
                smtp.Send(mesaj); //Mail'i gönderdik.
            }

            

       
        }


       
    }
}