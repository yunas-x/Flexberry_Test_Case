using System;
using System.Collections.Generic;
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
                #region LogOn
                LoggingFunctions.MakeLog(log);

                var loggingFunc = new Action<string, string, string>(new LoggingFunctions(log).Log);

                var pingInfoSender = new PingInfoXmlSaver(loggingFunc, results);
                var emailChecker = new EmailChecker(loggingFunc);
                var dbPinger = new DataBasePinger(loggingFunc);
                var adressSender = new AdressPinger(loggingFunc);
                var mailSender = new PingInfoSender(loggingFunc, results);

                #endregion


                #region Checking
                // Gets results of checks
                var xResults = new List<XElement>();

                xResults.Add(adressSender.PackPingResultsToXml(ICategories.Sites.ToLower())); // Checking sites



                xResults.Add(dbPinger.CheckAllConnectionStrings(ICategories.DBase));


                #endregion

                #region PackAndSend
                pingInfoSender.SaveToXml(xResults); // Create a file to send

                // Sending results
                var emails = emailChecker.GetValidEmails(); // gets from app.config
                mailSender.MailResults(emails);
                #endregion
            }
            else
            {
                Console.WriteLine(XDocument.Load(results));
            }

        }
    }
}
