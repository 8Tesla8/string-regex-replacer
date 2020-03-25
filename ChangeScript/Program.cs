using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ChangeScript
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string path = @"D:\Work\SQL - scripts\UserMessageTemplates.sql";
           
            var pattern = @"\d+\s\bAS\b\s\[(.*?)\]\,";

            var list1 = new List<StringReplaceModel>()
            {
               //new StringReplaceModel("UserMessageLayoutId", "", pattern),
               new StringReplaceModel("PlatformId", "", pattern),

               new StringReplaceModel("[UserMessageLayoutId]", "[UserMessageLayoutTypeId]", null),

              // new StringReplaceModel("[UserMessageLayoutId] = s.[UserMessageLayoutId],", "", null),
               new StringReplaceModel("[PlatformId] = s.[PlatformId],", "", null),

               new StringReplaceModel("s.[PlatformId],", "", null),
               new StringReplaceModel("[PlatformId],", "", null),


               //new StringReplaceModel("s.[UserMessageLayoutId],", "", null),
               //new StringReplaceModel("[UserMessageLayoutId],", "", null),
            };
            StrinReplacer s1 = new StrinReplacer();
            s1.Replace1(path, list1);

            //@"\d+\s";

            // \d+ -numbers
            // \s - space
            // ^ - start
            // \b - Start at a word boundary.
            // \[(.*?)\] -text in brackets
            // \, - ,
            // \& - &

            //\d+\s\bas\b\s\[(.*?)\] = 
            //24 AS [test],
            //24 AS [test3232],
        }
    }

    public class StrinReplacer 
    {
        public void Replace1(string pathToFile, List<StringReplaceModel> replaceList)
        {
            var lines = File.ReadAllLines(pathToFile); ;

            for (int i = 0; i < lines.Length; i++)
            { 
                foreach (var item in replaceList)
                {
                    if (!string.IsNullOrEmpty(item.RagexPattern) && !string.IsNullOrEmpty(item.StringToLook))
                    {
                        if (Regex.IsMatch(lines[i], item.RagexPattern) && lines[i].Contains(item.StringToLook))
                        {
                            lines[i] = Regex.Replace(lines[i], item.RagexPattern, item.NewString);
                        }
                    }
                    else if (!string.IsNullOrEmpty(item.RagexPattern) && Regex.IsMatch(lines[i], item.RagexPattern))
                    {
                        lines[i] = Regex.Replace(lines[i], item.RagexPattern, item.NewString);
                    }
                    else //if (lines[i].Contains(item.StringToLook))
                    {
                        lines[i] = lines[i].Replace(item.StringToLook, item.NewString);
                    }
                }
            }

            var filename = Path.GetFileName(pathToFile);
            var newName = "new" + filename;

            var newPath = pathToFile.Replace(filename, newName);
            if (File.Exists(newPath))
            {
                File.Delete(newPath);
            }

            File.AppendAllLines(newPath, lines);
        }
    }

    public class StringReplaceModel 
    {
        public string StringToLook { get; private set; }
        public string NewString { get; private set; }
        public string RagexPattern { get; private set; }

        public StringReplaceModel(string stringToLook, string newString, string ragexPattern)
        {
            StringToLook = stringToLook;
            NewString = newString;
            RagexPattern = ragexPattern;
        }
    }

}
