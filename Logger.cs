using System;

namespace FlexberryTestCase
{
    public class Logger
    {
        protected event Action<string, string, string> OnLog;

        /// <summary>
        /// Raises OnLogEvent
        /// </summary>
        protected void RaiseOnLog(string entity, string status, string node)
        {
            OnLog?.Invoke(entity, status, node);
        }

        /// <summary>
        /// Adds loging function to OnLog
        /// </summary>
        /// <param name="action">Action to perform on logging</param>
        public void AddLoging(Action<string, string, string> action)
        {
            OnLog += action;
        }

        /// <summary>
        /// Cancels loging function to OnLog
        /// </summary>
        /// <param name="action">Action to cancel on logging</param>
        public void CancelLoging(Action<string, string, string> action)
        {
            OnLog -= action;
        }

        /// <summary>
        /// Logging instance (base for another classes)
        /// </summary>
        /// <param name="action">Log function</param>
        public Logger(Action<string, string, string> action)
        {
            AddLoging(action);
        }
    }
}
