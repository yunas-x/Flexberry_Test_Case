using System;
using System.Collections.Generic;
using System.Text;

namespace FlexberryTestCase
{
    interface ICategories
    {
        protected static string[] categories = new string[4] { "Sends", "Mails", "Sites", "DBase" };

        public static string[] Categories
        {
            get => categories;
        }

        public static string Sends
        {
            get => categories[0];
        }

        public static string Mails
        {
            get => categories[1];
        }

        public static string Sites
        {
            get => categories[2];
        }

        public static string DBase
        {
            get => categories[3];
        }
    }

}
