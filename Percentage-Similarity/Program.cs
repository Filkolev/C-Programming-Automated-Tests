namespace Percentage_Similarity
{
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Program
    {
        static void Main()
        {
            // Enter directory path of exam solutions
            string directoryPath =
                @"C:\Users\Filip-Laptop\AppData\Roaming\Skype\My Skype Received Files\Exam";

            // Enter problem names
            string[] problemNames =
            {
                "01. Darts.cpp",
                "02. Bitwise-Minesweeper.cpp",
                "03. Hidden-Secrets.cpp",
                "04. String-Builder.cpp"
            };
           
            Regex whitespaceRegex = new Regex(@"\s+");

            Parallel.ForEach(problemNames, problemName =>
            {
                string problemNumber = problemName.Split('.')[0];

                using (var output = File.AppendText($"../../output-{problemNumber}.csv"))
                {
                    output.WriteLine("similarity,contestant_1,contestant_2");

                    var subDirectories = Directory.GetDirectories(directoryPath);
                    for (int i = 0; i < subDirectories.Length; i++)
                    {
                        for (int j = i + 1; j < subDirectories.Length; j++)
                        {
                            var file1 = subDirectories[i] + "\\" + problemName;
                            var file2 = subDirectories[j] + "\\" + problemName;

                            try
                            {
                                var file1Content = whitespaceRegex.Replace(File.ReadAllText(file1), " ");
                                var file2Content = whitespaceRegex.Replace(File.ReadAllText(file2), " ");
                                var similarity = file1Content.CalculateSimilarity(file2Content);

                                output.WriteLineAsync($"{similarity:F3},{file1.Split('\\')[8]},{file2.Split('\\')[8]}");
                            }
                            catch (IOException)
                            {
                                // if no submission for problem - continue
                            }
                        }
                    }
                }
            });
        }
    }
}