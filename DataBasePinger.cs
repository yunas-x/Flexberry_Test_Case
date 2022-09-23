using Npgsql;
using System;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace FlexberryTestCase
{
    public class DataBasePinger : Logger
    {

        public DataBasePinger(Action<string, string, string> action) :
            base(action) { }

        /// <summary>
        /// Checks if PostgreSQL is accessible
        /// </summary>
        /// <param name="connectionString">DataBase connection string</param>
        /// <returns>0 on successful connection</returns>
        private bool IsAccesible(string connectionString, string baseName = "DataBase")
        {
            bool status = false;

            try
            {
                var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                status = connection.State == ConnectionState.Open;
                connection.Close();
            }
            catch
            {
                status = false;
            }
            finally
            {
                RaiseOnLog(baseName, StatusToString(status), ICategories.DBase);
            }

            return status;



        }

        /// <summary>
        /// Converts status to string
        /// </summary>
        /// <param name="status">Boolean status</param>
        /// <returns>Status converted to string (Success or Failure)</returns>
        private static string StatusToString(bool status)
        {
            if (status)
            {
                return "Success";
            }
            else
            {
                return "Failure";
            }
        }

        /// <summary>
        /// Checks all connection strings
        /// </summary>
        /// <param name="section">A name of the section</param>
        /// <returns>A result in Xml</returns>
        public XElement CheckAllConnectionStrings(string section)
        {
            XElement results = new XElement(section);
            for (int i = 1; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                var conString = ConfigurationManager.ConnectionStrings[i].ConnectionString;
                var currentResult = new XElement(ConfigurationManager.ConnectionStrings[i].Name, 
                                                     new XAttribute("Status", 
                                                        StatusToString(IsAccesible(conString, ConfigurationManager.ConnectionStrings[i].Name))),
                                                     new XAttribute("Date", DateTime.UtcNow));
                results.Add(currentResult);
            }

            return results;
        }
    }
}
