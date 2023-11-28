using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace brfGuiden.WPF.Helper
{
    public class ConfigHelper
    {

        public ConfigHelper()
        { 
        
        }


        public bool WriteToAppConfig(string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[key].Value = value;
                config.Save();
                return true; 
            }
            catch (Exception)
            {
                return false;
            }                

        }



        public string ReadFromAppConfig(string key)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                return config.AppSettings.Settings[key].Value;
            }
            catch (Exception)
            {
                return string.Empty;
            }                
                

        }



    }
}
