﻿using Npgsql;
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
        private bool IsAccesible(string connectionString)
        {
            bool status;

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
                RaiseOnLog("DataBase", StatusToString(status), ICategories.DBase);
            }

            return status;



        }

        private static string StatusToString(bool status)
        {
            if (status)
            {
                return "Success";
            }
            else
            {
                return "Failuure";
            }
        }

        public XElement CheckAllConnectionStrings(string section)
        {
            XElement results = new XElement(section);
            for (int i = 1; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                var conString = ConfigurationManager.ConnectionStrings[i].ConnectionString;
                var currentResult = new XElement("DataBase", new XAttribute("Status", StatusToString(IsAccesible(conString))),
                                                     new XAttribute("Date", DateTime.UtcNow));
                results.Add(currentResult);
            }

            return results;
        }
    }
}
