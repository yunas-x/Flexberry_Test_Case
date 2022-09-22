using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Collections.Specialized;

namespace FlexberryTestCase
{
    public static class ConfigDeserializer
    {

        /// <summary>
        /// Returns valid emails from .config files
        /// Logs invalid emails
        /// </summary>
        /// <returns>A list of emails</returns>
        public static List<string> DeserializeAppSettingsSection()
        {
            // Returns all the values from AppSettings
            return ConfigurationManager.AppSettings.GetValues().ToList();
        }

        /// <summary>
        /// Deserializes sites section
        /// Throws exception 
        /// </summary>
        /// <returns>A list of sites to access. Throws NullReferenceException if sectionName is invalid</returns>
        public static List<string> DesirializeValuesFromNameValueCollectionSection(string sectionName)
        {
            // Deserializes section sites from .config
            var keyValueSitesPairs = ConfigurationManager.GetSection(sectionName) as NameValueCollection;

            // Tries to get values
            List<string> adresses = keyValueSitesPairs?.GetValues().ToList();

            // Checks if section exists, if not throws exception
            return adresses?? throw new NullReferenceException($"Section {sectionName} doesn't exist");
        }

        public static Dictionary<string, string> DesirializeNameValueCollectionSection(string sectionName)
        {
            // Deserializes section sites from .config
            var keyValueSitesPairs = ConfigurationManager.GetSection(sectionName) as NameValueCollection;

            Dictionary<string, string> pairs = new Dictionary<string, string>();
            // Tries to get pairs
            for (var i = 0; i < keyValueSitesPairs.Count; i++)
            {
                var name = keyValueSitesPairs.AllKeys[i];
                pairs.Add(name, keyValueSitesPairs[name]);
            }

            // Checks if section exists, if not throws exception
            return pairs ?? throw new NullReferenceException($"Section {sectionName} doesn't exist");
        }

        /// <summary>
        /// Gets all accesible values from NameValueCollection
        /// </summary>
        /// <param name="nameValueCollection">A collection to get values from</param>
        /// <returns>All values in collection. If no values returns empty collection</returns>
        public static IEnumerable<string> GetValues(this NameValueCollection nameValueCollection)
        {

            // Get all sites (values in pairs)
            var adresses = (from key in nameValueCollection.AllKeys
                            select nameValueCollection[key]).
                            ToList();

            return adresses.Count != 0? adresses : new List<string>();
        }
    }
}
