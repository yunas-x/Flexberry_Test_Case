using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FlexberryTestCase
{

    public class PingInfoXmlSaver: PingInfoSaver
    { 

        public PingInfoXmlSaver() { }

        public PingInfoXmlSaver(Action<string, string, string> action) :
            base(action)
        { }

        public PingInfoXmlSaver(Action<string, string, string> action, string saveFileName) : base(action, saveFileName)
        { }

        public PingInfoXmlSaver(string saveFileName) : base(saveFileName)
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
                RaiseOnLog(FileName, "Save success", ICategories.Sends);
            }
            catch
            {
                RaiseOnLog(FileName, "Save failed", ICategories.Sends);
            }
        }
    }
}
