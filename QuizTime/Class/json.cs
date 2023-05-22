using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTime.Class
{
    internal class json
    {
        public static void WriteDataToFile(List<vragenopslaan> argListSavedData)
        {
            var tempSavedDataFilePath = GetSavedDataFilePath();

            var tempJSON = JsonConvert.SerializeObject(argListSavedData, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(tempSavedDataFilePath, tempJSON);
        }   
        public static string GetSavedDataFilePath()
        {
            var startupPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            var tempSavedCompanyMarges = Path.Combine(startupPath, "NAAM.SavedData.json");
            return tempSavedCompanyMarges;
        }
        public static List<vragenopslaan> ReadSavedDataFile()
        {
            try
            {
                var tempSavedDataFilePath = GetSavedDataFilePath();

                var tempJSON = File.ReadAllText(tempSavedDataFilePath);
                var tempDeserializedSavedDataFileList = JsonConvert.DeserializeObject<List<vragenopslaan>>(tempJSON);

                return tempDeserializedSavedDataFileList;
            }
            catch (Exception ex)
            {
                return new List<vragenopslaan>();
            }
        }

    }
}
