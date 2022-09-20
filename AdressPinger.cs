using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace FlexberryTestCase
{
    /// <summary>
    /// A class to check adress' accesiblity
    /// </summary>
    public class AdressPinger : Logger
    {
        public AdressPinger() { }

        public AdressPinger(Action<string, string, string> action) :
            base(action)
        { }

        /// <summary>
        /// The method checks if host adress is accesible
        /// </summary>
        /// <param name="nameOrAdress">A host adress to check</param>
        /// <returns>An exit code (0 for success)</returns>
        private string PingAdress(string nameOrAdress)
        {

            using (var ping = new Ping())
            {
                string status;
                try
                {
                    var reply = ping.Send(nameOrAdress);
                    status = reply.Status == IPStatus.Success ? "Success" : "Fail";
                }
                catch (PingException) // Adress does not exist
                {
                    status = "Doesn't Exist";
                }
                catch // Other failures
                {
                    status = "Something went wrong";
                }
                RaiseOnLog(nameOrAdress, status, ICategories.Sites);
                return status;
            }
        }
        
        /// <summary>
        /// Checks if sites in section are availible and packs the results to XML
        /// </summary>
        /// <param name="section">Name of section where sites are</param>
        /// <returns>XML document with sites accessibility</returns>
        public XElement PackPingResultsToXml(string section = "sites")
        {
            // Create Document
            var XmlPingResults = new XElement(section,
                                                from site in ConfigDeserializer.DesirializeNameValueCollectionSection(section)
                                                select new XElement(site,
                                                                    new XAttribute("Status", PingAdress(site)),
                                                                    new XAttribute("Date", DateTime.UtcNow)));

            return XmlPingResults;
        }


    }
}
