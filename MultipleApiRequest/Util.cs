using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleApiRequest
{
    public static class Util
    {
        /// <summary>
        /// Reads the setting from the app.config file
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return string.Empty;
            }
        }

    }
}
