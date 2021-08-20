using System;
using System.IO;
using log4net.Config;

namespace DesignPatternsWithSeleniumWebDriver.Logging
{
    public static class LoggerManager
    {
        static LoggerManager()
        {
            XmlConfigurator.Configure(new FileInfo("App.config"));
        }

        public static Logger GetLogger(Type type)
        {
            return new Logger(type);
        }
    }
}