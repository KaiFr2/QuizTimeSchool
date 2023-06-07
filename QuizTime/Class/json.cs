using Microsoft.Extensions.FileSystemGlobbing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuizTime.Class
{
    internal class json
    {
        public static void WriteDataToFile(quiz quiz_)
        {
            var tempSavedDataFilePath = GetSavedDataFilePath(quiz_.iD);

            var tempJSON = JsonConvert.SerializeObject(quiz_, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(tempSavedDataFilePath, tempJSON);
        }   
        public static string GetSavedDataFilePath(int quiz_id)
        {
            var startupPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            var UsageFilePath = Path.Combine(startupPath, quiz_id + ".SavedData.json");
            
            return UsageFilePath;
        }
        public static List<quiz> readAllQuizes()
        {
            var appDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            Matcher matcher = new Matcher();
            matcher.AddIncludePatterns(new[] { "*.SavedData.json" });


            IEnumerable<string> matchingFiles = matcher.GetResultsInFullPath(appDir);

            var list = new List<quiz>();

            foreach (var matchingFile in matchingFiles)
            {
                string content = File.ReadAllText(matchingFile);
                list.Add(JsonConvert.DeserializeObject<quiz>(content));
            }
            return list;

            /*try
            {
                var tempSavedDataFilePath = GetSavedDataFilePath(12);

                var tempJSON = File.ReadAllText(tempSavedDataFilePath);
                var tempDeserializedSavedDataFileList = JsonConvert.DeserializeObject<List<vraag>>(tempJSON);

                if (tempDeserializedSavedDataFileList == null)
                    return new List<vraag>();

                return tempDeserializedSavedDataFileList;
            }
            catch (Exception ex)
            {
                return new List<vraag>();
            }*/
        }

    }
}
