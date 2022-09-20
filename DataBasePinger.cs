using System;
using System.Collections.Generic;
using System.Text;

namespace FlexberryTestCase
{
    // To Do
    public class DataBasePinger : Logger
    {
        public DataBasePinger() { }

        public DataBasePinger(Action<string, string, string> action) :
            base(action) { }

        /// <summary>
        /// Checks if PostgreSQL is accessible
        /// </summary>
        /// <param name="connectionString">DataBase connection string</param>
        /// <returns>0 on successful connection</returns>
        public int IsAccesible(string connectionString)
        {

            return 0;
        }
    }
}
