using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Configuration;

namespace FlexberryTestCase
{
    /// <summary>
    /// A class to check adress' accesiblity
    /// </summary>
    public class Pinger
    {
        /// <summary>
        /// The method checks if host adress is accesible
        /// </summary>
        /// <param name="nameOrAdress">A host adress to check</param>
        /// <returns>An exit code (0 for success)</returns>
        static public int PingAdress(string nameOrAdress)
        {
            /// 0 for success
            /// -1 for failure
            /// 2, 3 in case of exception
            int status;

            using (var ping = new Ping())
            {
                try
                {
                    var reply = ping.Send(nameOrAdress);
                    return reply.Status == IPStatus.Success ? status = 0 : status = -1;
                }
                catch (PingException) // Adress does not exist
                {
                    status = 2;
                }
                catch // Other failures
                {
                    status = 3;
                }
            }
            return status;
        }

        /// <summary>
        /// Checks if email is valid
        /// </summary>
        /// <param name="email">An email to check</param>
        /// <returns>True if valid, False if invalid</returns>
        static public bool IsValidEmail(string email)
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
    }
}
