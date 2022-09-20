using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FlexberryTestCase
{
    public class PingInfoSaver: Logger
    {

        protected string FileName { get; set; }

        public PingInfoSaver() { }

        public PingInfoSaver(Action<string, string, string> action) :
            base(action)
        { }

        public PingInfoSaver(Action<string, string, string> action, string saveFileName) : base(action)
        {
            FileName = saveFileName;
        }

        public PingInfoSaver(string saveFileName)
        {
            FileName = saveFileName;
        }

    }
}
