using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FileMergerApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string filePath1 = @"C:\\Users\\patha\\Downloads\\application.txt";
            string filePath2 = @"C:\\Users\\patha\\Downloads\\default.txt";
            string outputFilePath = @"C:\Users\patha\Downloads\merged.txt";

            try
            {

                if (!File.Exists(filePath1) || !File.Exists(filePath2))
                {
                    Console.WriteLine("One or both input files do not exist.");
                    return;
                }


                Console.WriteLine("Merging files...");

   
                Stopwatch stopwatch = Stopwatch.StartNew();


                Task<string> task1 = File.ReadAllTextAsync(filePath1);
                Task<string> task2 = File.ReadAllTextAsync(filePath2);


                string[] results = await Task.WhenAll(task1, task2);


                string mergedContent = string.Join(Environment.NewLine, results);


                await File.WriteAllTextAsync(outputFilePath, mergedContent);


                stopwatch.Stop();


                Console.WriteLine($"Files merged successfully into: {outputFilePath}");
                Console.WriteLine($"Time taken: {stopwatch.Elapsed.TotalSeconds:F2} seconds");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Access Error: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("I/O Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
            }
        }
    }
}
