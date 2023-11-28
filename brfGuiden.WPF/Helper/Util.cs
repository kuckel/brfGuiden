#nullable disable 
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace brfGuiden.WPF.Helper
{
    class Util
    {

        public Util()
        {


        }


        public System.Windows.Media.Brush ConvertColour(string htmlColor)
        {
            return (Brush)new System.Windows.Media.BrushConverter().ConvertFromString(htmlColor);
        }

        public int GetSecondsSince1970()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return System.Convert.ToInt32(t.TotalSeconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FullPathAndName"></param>
        /// <param name="TextToSave">Text or content to save</param>
        /// <returns></returns>
        public bool SaveToFile(string FullPathAndName, string TextToSave)
        {
            try
            {
                StreamWriter objStreamWriter;
                objStreamWriter = new StreamWriter(FullPathAndName, true, System.Text.Encoding.Unicode);
                objStreamWriter.Write(TextToSave);
                objStreamWriter.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Newtonsoft.Json.Formatting.Indented,
            Culture = new CultureInfo("sv-SE"),
            DateFormatString = "yyyy-MM-dd"

        };

        public string PathToSpecialFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\"; 
        }

        public string SerializeToJson<T>(T data)
        {
            return JsonConvert.SerializeObject(data, jsonSerializerSettings);
        }

        public string GetComputerLoggedOnUser()
        {
            return Environment.UserName;
        }
        public string GetComputerName()
        {
            return System.Environment.MachineName;
        }

        public T DeserializeFromJson<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public bool SaveJsonAsFile(string json, string fullPathTosaveTo)
        {
            File.WriteAllText(fullPathTosaveTo, json);
            return File.Exists(fullPathTosaveTo);
        }

        


    }
}
