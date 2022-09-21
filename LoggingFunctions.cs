using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FlexberryTestCase
{
    public class LoggingFunctions
    {

        /// <summary>
        /// FileName of log with extension
        /// </summary>
        protected string FileName { get; set; }

        public LoggingFunctions(string logTo)
        {
            FileName = logTo;
        }

        /// <summary>
        /// Logs all actions performed to a logFile passed to Constructor
        /// </summary>
        /// <param name="entity">What is checked</param>
        /// <param name="status">It's status</param>
        /// <param name="node">Category of information (node to attach)</param>
        public void Log(string entity, string status, string node)
        {
            // Load log
            var xdoc = XDocument.Load(FileName);

            xdoc.Root.Element(node)?
                .Add(new XElement(entity, new XAttribute("Status", status),
                                                              new XAttribute("Date", DateTime.UtcNow)));

            xdoc.Save(FileName);
        }

        /// <summary>
        /// Creates log if it doesn't exist
        /// If new categories added in list it will update
        /// </summary>
        public void MakeLog()
        {
            if (!this.FileExistsInAppDirectory())
            {
                XDocument xdoc = new XDocument();
                xdoc.Add(new XElement("root", from category in ICategories.Categories
                                              select new XElement(category)));
                xdoc.Save(FileName);
            }
            foreach (var category in ICategories.Categories)
            {
                // Load the log
                var xdoc = XDocument.Load(FileName);

                // If node doesn't exist
                if (!xdoc.Root.Elements(category).Any())
                { 
                    // Create it
                    xdoc.Add(new XElement(category)); 
                }
            }
        }

        /// <summary>
        /// Creates log if it doesn't exist
        /// If new categories added in list it will update
        /// </summary>
        /// <param name="logPath">Logfile name</param>
        public static void MakeLog(string logPath)
        {
            if (!FileExistsInAppDirectory(logPath))
            {
                XDocument xdoc = new XDocument();
                xdoc.Add(new XElement("root", from category in ICategories.Categories
                                              select new XElement(category)));
                xdoc.Save(logPath);
            }
            foreach (var category in ICategories.Categories)
            {
                // Load the log
                var xdoc = XDocument.Load(logPath);

                // If node doesn't exist
                if (!xdoc.Root.Elements(category).Any())
                {
                    // Create it
                    xdoc.Root.Add(new XElement(category));
                }
            }
        }

        /// <summary>
        /// Checks if file with given name exists in app folder
        /// </summary>
        /// <returns>True if exists, else false</returns>
        protected bool FileExistsInAppDirectory()
        {
            return File.Exists(Path.Combine(Directory.GetCurrentDirectory(), this.FileName));
        }

        /// <summary>
        /// Checks if file with given name exists in app folder
        /// </summary>
        /// <param name="fileName">path to the file</param>
        /// <returns>True if exists, else false</returns>
        protected static bool FileExistsInAppDirectory(string filename)
        {
            return File.Exists(Path.Combine(Directory.GetCurrentDirectory(), filename));
        }
    }
}
