using System.Configuration;

namespace DesignPatternsWithSeleniumWebDriver.WebDriver
{
    class Configuration
    {
        public static string GetEnviromentVar(string var, string defaultValue)
        {
            return ConfigurationManager.AppSettings[var] ?? defaultValue;
        }

        public static string ElementTimeout => GetEnviromentVar("ElementTimeout", "");

        public static string Browser => GetEnviromentVar("Browser", "");

        public static string StartUrl => GetEnviromentVar("StartUrl", "");
    }
}