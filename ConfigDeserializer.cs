using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Collections.Specialized;

namespace FlexberryTestCase
{
    public class ConfigDeserializer
    {
        /// <summary>
        /// Returns valid emails from .config files
        /// Logs invalid emails
        /// </summary>
        /// <returns>A list of emails</returns>
        public static List<string> GetValidEmails()
        {
            // Gets valid emails using LINQ queries
            var validEmails = (from item in ConfigurationManager.AppSettings.AllKeys
                                   where Pinger.IsValidEmail(ConfigurationManager.AppSettings[item])
                                   select ConfigurationManager.AppSettings[item])
                                  .ToList();
            
            return validEmails;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAdresses()
        {
            // Deserializes section sites from .config
            var keyValueSitesPairs = ConfigurationManager.GetSection("sites") as NameValueCollection;

            // Store adresses there
            var adresses = new List<string>();

            // If section sites exists 
            if (keyValueSitesPairs != null)
            {
                // Get all sites (values in pairs)
                adresses = (from key in keyValueSitesPairs.AllKeys
                            select keyValueSitesPairs[key]).
                            ToList();
            }

            return adresses;
        }


    }
}
