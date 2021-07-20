using System;
using System.Configuration;

namespace DesignPatternsWithSeleniumWebDriver.WebDriver
{
    class Configuration
    {
        public static BrowsersList.BrowserType currentBrowser;

        public static string GetEnviromentVar(string var, string defaultValue)
        {
            return ConfigurationManager.AppSettings[var] ?? defaultValue;
        }

        public static int ElementTimeout => Convert.ToInt32(GetEnviromentVar("ElementTimeout", ""));

        public static bool Browser => BrowsersList.BrowserType.TryParse(GetEnviromentVar("Browser", ""), out currentBrowser);

        public static string StartUrl => GetEnviromentVar("StartUrl", "");

        public static string Login => GetEnviromentVar("Login", "");

        public static string Password => GetEnviromentVar("Password", "");
    }
}