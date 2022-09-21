using System;

namespace FlexberryTestCase
{
    /// <summary>
    /// Base class for saving results
    /// </summary>
    public class PingInfoSaver: Logger
    {
        /// <summary>
        /// File name to save a file
        /// </summary>
        protected string FileName { get; set; }

        /// <summary>
        /// A basic class to inherit from
        /// </summary>
        /// <param name="action">Log functiob</param>
        /// <param name="saveFileName">A file to save to</param>
        public PingInfoSaver(Action<string, string, string> action, string saveFileName) : base(action)
        {
            FileName = saveFileName;
        }
    }
}
