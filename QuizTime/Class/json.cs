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
    public static class json
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
            var parentFolder = Directory.GetParent(startupPath).FullName;
            var quizzesFolderPath = Path.Combine(parentFolder, "Debug\\quizzes");

            if (!Directory.Exists(quizzesFolderPath))
                Directory.CreateDirectory(quizzesFolderPath);

            var savedDataFilePath = Path.Combine(quizzesFolderPath, quiz_id + ".SavedData.json");

            return savedDataFilePath;
        }


        public static List<quiz> readAllQuizes()
        {
            var appDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            var quizzesFolderPath = Path.Combine(appDir, "quizzes");

            Matcher matcher = new Matcher();
            matcher.AddIncludePatterns(new[] { "*.SavedData.json" });

            IEnumerable<string> matchingFiles = matcher.GetResultsInFullPath(quizzesFolderPath);

            var list = new List<quiz>();

            foreach (var matchingFile in matchingFiles)
            {
                string content = File.ReadAllText(matchingFile);
                list.Add(JsonConvert.DeserializeObject<quiz>(content));
            }
            return list;
        }

    public static quiz FetchQuiz(int id)
    {
        string filePath = GetSavedDataFilePath(id);

        // Read the JSON file
        string jsonContent = File.ReadAllText(filePath);

        // Deserialize the JSON into Quiz object
        quiz quiz = JsonConvert.DeserializeObject<quiz>(jsonContent);

        return quiz;
    }

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

