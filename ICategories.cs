
namespace FlexberryTestCase
{
    interface ICategories
    {
        /// <summary>
        /// Log categories
        /// </summary>
        protected static string[] categories = new string[5] { "Sends", "Mails", "Sites", "DBase", "Saves" };

        public static string[] Categories
        {
            get => categories;
        }

        public static string Sends
        {
            get => categories[0];
        }

        public static string Saves
        {
            get => categories[4];
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
