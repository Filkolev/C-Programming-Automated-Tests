namespace C_Plus_Plus_Usage_Check
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    public class Program
    {
        public static void Main(string[] args)
        {
            string directoryPath =
                @"C:\Users\Filip-Laptop\AppData\Roaming\Skype\My Skype Received Files\Contest last submissions for C Programming Exam 14 November 2015";

            string regexPattern = @"(?<!\w)std(?!\w)";
            var regex = new Regex(regexPattern);

            var subDirectories = Directory.GetDirectories(directoryPath);
            foreach (var subDirectory in subDirectories)
            {
                var files = Directory.GetFiles(subDirectory);
                foreach (var file in files)
                {
                    var text = File.ReadAllText(file);

                    if (regex.IsMatch(text))
                    {
                        Console.WriteLine("std use detected in: {0}", file);
                    }
                }
            }
        }
    }
}
