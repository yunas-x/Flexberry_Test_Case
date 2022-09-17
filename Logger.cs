using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FlexberryTestCase
{
    public class Logger
    {
        /// <summary>
        /// Logs invalid email adresses
        /// </summary>
        public static void LogInvalidEmails()
        {

            // Checking mail validity using LINQ-queries
            var invalidEmails = (from item in ConfigurationManager.AppSettings.AllKeys
                                 where !Pinger.IsValidEmail(ConfigurationManager.AppSettings[item])
                                 select ConfigurationManager.AppSettings[item])
                                .ToList();

            /// Implement logging there

        }

        /// <summary>
        /// Logs invalid email adresses
        /// Outs the list of invalid emails
        /// </summary>
        public static void LogInvalidEmails(out List<string> invalidEmails)
        {

            // Checking mail validity using LINQ-queries
            invalidEmails = (from item in ConfigurationManager.AppSettings.AllKeys
                             where !Pinger.IsValidEmail(ConfigurationManager.AppSettings[item])
                             select ConfigurationManager.AppSettings[item])
                             .ToList();

            /// Implement logging there

        }


    }
}
