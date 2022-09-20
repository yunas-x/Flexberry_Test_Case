using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace FlexberryTestCase
{
    class Program
    {
        // Where to store last results
        private const string results = "lastResults.xml";

        // Logfile adress
        private const string log = "log.xml";

        /// <summary>
        /// Entry point
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                // Turn logging on
                LoggingFunctions.MakeLog(log);
                var pingInfoSender = new PingInfoSender(new LoggingFunctions(log).Log);
                var emailChecker = new EmailChecker(new LoggingFunctions(log).Log);

                emailChecker.GetValidEmails(); // gets from app.config


            }
            else
            {
                Console.WriteLine(XDocument.Load(results));
            }
                
        }
    }
}
