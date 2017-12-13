using System;
using System.Linq;
using System.IO;
using System.Configuration;

namespace WordsPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string wordsFilePath = ConfigurationManager.AppSettings["WordsFilePath"];

                if (File.Exists(wordsFilePath) == false)
                {
                    Console.WriteLine("File '{0}' doesn't exist", wordsFilePath);
                    Console.ReadLine();
                    return;
                }

                var words = File.ReadLines(@wordsFilePath).ToList();

                Console.WriteLine("Longest word made from other is: {0}", Word.GetLongestWord(words));

                Console.WriteLine("First largest word: {0}", Word.GetNthLongestWord(words, 1));

                Console.WriteLine("Second largest word: {0}", Word.GetNthLongestWord(words, 2));

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
