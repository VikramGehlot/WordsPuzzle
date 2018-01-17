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

                Console.WriteLine("In sample text file, longest word made from other is: {0}", Word.GetLongestWord(words));

                Console.WriteLine("In sample text file, first largest word: {0}", Word.GetNthLongestWord(words, 1));

                Console.WriteLine("In sample text file, second largest word: {0}", Word.GetNthLongestWord(words, 2));

                Console.WriteLine();
                string[] sampleWords = { "ala", "ma", "kota", "aa", "aabbb", "bbb", "cccc", "aabbbmacccc", "aabbbmaxxcccc" };
                //string[] sampleWords = { "cat", "cats", "catsdogcats", "catxdogcatsrat", "dog", "dogcatsdog", "hippopotamuses", "rat", "ratcatdogcat" };

                Console.WriteLine("For example, sample word list is: 'ala', 'ma', 'kota', 'aa', 'aabbb', 'bbb', 'cccc', 'aabbbmacccc', 'aabbbmaxxcccc'");
                Console.WriteLine("Total words made to others in above sample word list is : {0}", Word.GetWordCountMadeToOthers(sampleWords.ToList()));

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
