using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FlexberryTestCase
{

    public class PingInfoXmlSaver: PingInfoSaver
    { 

        public PingInfoXmlSaver(Action<string, string, string> action, string saveFileName) : base(action, saveFileName)
        { }

        /// <summary>
        /// Saves results to a file, overrides it if exists
        /// </summary>
        public void SaveToXml(List<XElement> elements)
        {
            try
            {
                XDocument xdoc = new XDocument();
                xdoc.Add(new XElement("root", elements));
                xdoc.Save(FileName);
                RaiseOnLog(FileName, "Save success", ICategories.Saves);
            }
            catch
            {
                RaiseOnLog(FileName, "Save failed", ICategories.Saves);
            }
        }
    }
}
