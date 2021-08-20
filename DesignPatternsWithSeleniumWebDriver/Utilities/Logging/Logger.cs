using System;
using log4net;

namespace DesignPatternsWithSeleniumWebDriver.Logging
{
    public class Logger
    {
        private readonly ILog Log;

        internal Logger(Type type)
        {
            this.Log = LogManager.GetLogger(type);
        }

        public void Info(string message)
        {
            this.Log.Info(message);
        }

        public void Debug(string message)
        {
            this.Log.Debug(message);
        }

        public void Error(string message)
        {
            this.Log.Error(message);
        }

        public void Warn(string message)
        {
            this.Log.Warn(message);
        }
    }
}