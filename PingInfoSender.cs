using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace FlexberryTestCase
{

    // ToDo

    public class PingInfoSender: Logger
    {
        private const string node = "Sends";
        protected string FileName { get; set; }

        public PingInfoSender() { }

        public PingInfoSender(Action<string, string, string> action) :
            base(action)
        { }


        /// <summary>
        /// Sends mail with results
        /// </summary>
        /// <param name="email">Adressee</param>
        /// <returns>True is sent successfully, else False</returns>
        public bool MailPingInfo(string email)
        {
            //RaiseOnLog(email, "Success", node);
            return false;
        }
    }
}
