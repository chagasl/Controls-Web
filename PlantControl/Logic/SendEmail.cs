using System.Data;
using System.Net;
using System.Net.Mail;

namespace PlantControl
{
    public class SendEmail
    {
        public void SendMail(string mailSubject, string mailBody, DataTable mailCC , DataTable mailTO)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("XXXX.XXXX@XXX.com");
           
            if (mailCC.Rows.Count > 0)
            {            
                for (int i = 0; i < mailCC.Rows.Count; i++)
                {
                    mail.CC.Add(mailCC.Rows[i].ItemArray[0].ToString()); //para                   
                }           
            }
            if (mailTO.Rows.Count > 0)
            {
                for (int i = 0; i < mailTO.Rows.Count; i++)
                {
                    mail.To.Add(mailTO.Rows[i].ItemArray[0].ToString()); //para
                }
            }
                    
            mail.Subject = mailSubject; //assunto
            mail.Body = mailBody; //mensagem
            mail.IsBodyHtml = true;

            // em caso de anexos
            //mail.Attachments.Add(new Attachment(@"C:\teste.txt"));

            using (var smtp = new SmtpClient("XXX.XXX.net"))
            {
                smtp.EnableSsl = false; // GMail requer SSL
                smtp.Port = 25;       // porta para SSL
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network; //modo de envio
                smtp.UseDefaultCredentials = true; //vamos utilizar credencias especificas

                // seu usuário e senha para autenticação
                smtp.Credentials = new NetworkCredential("", "");

                // envia o e-mail
                smtp.Send(mail);
            }  
        }
    }
}