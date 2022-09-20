using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace FlexberryTestCase
{
    public class EmailChecker: Logger
    {
        public EmailChecker() { }

        public EmailChecker(Action<string, string, string> action) :
            base(action)
        { }

        /// <summary>
        /// Checks if email is valid
        /// </summary>
        /// <param name="email">An email to check</param>
        /// <returns>True if valid, False if invalid</returns>
        private bool IsValidEmail(string email)
        {
            // A regex pattern to check email validity
            var pattern = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                    + "@"
                                    + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");

            // Check if email is valid
            if (!pattern.IsMatch(email))
            {
                return false;
            }

            // Try using email adress
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Logs invalid email adresses
        /// </summary>
        public List<string> GetValidEmails()
        {
            // Store email adresses here
            List<string> validMails = new List<string>();

            // Checking mail adress validity 
            foreach (var item in ConfigDeserializer.DesirializeNameValueCollectionSection("emails"))
            {
                // Store validity status there
                string validity;

                // Checks if valid, defines the status
                if (IsValidEmail(item))
                {
                    validity = "Valid";
                    validMails.Add(item);
                }
                else
                {
                    validity = "Invalid";
                }

                // Replace @ with " at "
                var newAdress = Regex.Replace(item, "@", "~at~");

                // Log there
                RaiseOnLog(newAdress, validity, ICategories.Mails);
            }

            return validMails;
        }

    }
}
