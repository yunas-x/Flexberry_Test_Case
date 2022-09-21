using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace FlexberryTestCase
{

    // ToDo

    public class PingInfoSender: Logger
    {
        /// <summary>
        /// Path to the file to send
        /// </summary>
        public string FileName { get; set; }


        /// <param name="action">Logging function</param>
        /// <param name="filename">Filename of the file to be sent</param>
        public PingInfoSender(Action<string, string, string> action, string filename) :
            base(action)
        {
            FileName = filename;
        }


        /// <summary>
        /// Sends mail with results
        /// </summary>
        /// <param name="email">Adressee</param>
        /// <returns>True is sent successfully, else False</returns>
        private bool MailResult(string email)
        {
            try
            {
                var from = new MailAddress("testinfoforflexberry@gmail.com", "testinfo");
                var to = new MailAddress(email);

                var message = new MailMessage(from, to);

                message.Subject = "Результаты";
                message.IsBodyHtml = true;
                message.Body = "<h3>Ниже приложены результаты подключения к СУБД</h3>";
                message.Attachments.Add(new Attachment(FileName));

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                smtp.Credentials = new NetworkCredential("testinfoforflexberry@gmail.com", "csamnaotkltgeoiz");
                smtp.EnableSsl = true;
                smtp.Send(message);
                return true;
            }
            catch
            {
                return false;
            }


        }

        /// <summary>
        /// Tries to send results to each email
        /// </summary>
        /// <param name="emails">A collection of emails</param>
        public void MailResults(IEnumerable<string> emails)
        {
            foreach (var email in emails)
            {
                // Replace @ with " at "
                var newAdress = Regex.Replace(email, "@", "--at--"); ;

                if (MailResult(email))
                {
                    RaiseOnLog(newAdress, "Report send successfully", ICategories.Sends);
                }
                else
                {
                    RaiseOnLog(newAdress, "Report send failure", ICategories.Sends);
                }
            }
        }
    }
}
