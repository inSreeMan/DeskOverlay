using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskOverlay
{
    public static class SMPaths
    {
        private const string AppDomainName = "SymphonyApps";
        private static string getAppName(string appName = "")
        {
            return appName == "" ? System.Reflection.Assembly.GetExecutingAssembly().GetName().Name : appName; 
        }

        public static string GetAppDataPath(string appName)
        {
            appName = getAppName(appName);
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppDomainName, appName);
            System.IO.Directory.CreateDirectory(appDataPath);
            return appDataPath;
        }
        public static string GetAppDataFile(string fileName, string appName = "")
        {
            return Path.Combine(GetAppDataPath(appName), fileName);
        }
    }
}
