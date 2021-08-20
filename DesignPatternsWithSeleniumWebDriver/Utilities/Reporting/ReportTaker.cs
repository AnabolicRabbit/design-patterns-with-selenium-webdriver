using System;
using System.IO;

namespace DesignPatternsWithSeleniumWebDriver.Utilities.Reporting
{
    public static class ReportTaker
    {
        public static string reportFolderName = string.Format("{0}", DateTime.Now.ToString("dd.MM.yyyy_HH.mm.ss"));

        public static string TakeReport(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string reportSaveFullPath = Path.Combine(directory, reportFolderName);

            return reportSaveFullPath;
        }
    }
}