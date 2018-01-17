using System;
using System.Collections.Generic;
using System.Linq;

namespace WordsPuzzle
{
    public class Word
    {
        /// <summary>
        /// Get longest words made from other words
        /// </summary>
        /// <param name="words">list of existing words</param>
        /// <returns></returns>
        public static string GetLongestWord(List<string> words)
        {
            if (words == null || words.Count == 0)
            {
                return string.Empty;
            }

            List<string> sortedWords = words.OrderByDescending(word => word.Length).ToList();

            HashSet<String> setOfWords = new HashSet<String>(sortedWords);

            foreach (var word in sortedWords)
            {
                if (IsMadeFromOtherWords(word, setOfWords))
                {
                    return word;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Check if made from other existing words.
        /// </summary>
        /// <param name="word">any existing word</param>
        /// <param name="setOfWords">all existing words</param>
        /// <returns></returns>
        private static bool IsMadeFromOtherWords(string word, HashSet<string> setOfWords)
        {
            if (String.IsNullOrEmpty(word))
            {
                return false;
            }

            if (word.Length == 1)
            {
                return setOfWords.Contains(word);
            }

            foreach (var pair in GetPairs(word).Where(pair => setOfWords.Contains(pair.Item1)))
            {
                return setOfWords.Contains(pair.Item2) || IsMadeFromOtherWords(pair.Item2, setOfWords);
            }

            return false;
        }

        /// <summary>
        /// Get all pairs from word
        /// </summary>
        /// <param name="word">any existing word</param>
        /// <returns></returns>
        private static List<Tuple<string, string>> GetPairs(string word)
        {
            var wordPairs = new List<Tuple<string, string>>();

            for (int i = 1; i < word.Length; i++)
            {
                wordPairs.Add(Tuple.Create(word.Substring(0, i), word.Substring(i)));
            }
            return wordPairs;
        }

        /// <summary>
        /// Get Nth Longest Word
        /// </summary>
        /// <param name="words">all existing words</param>
        /// <param name="nPosition">Nth Position like first longest, second longest</param>
        /// <returns></returns>
        public static string GetNthLongestWord(List<string> words, int nPosition)
        {
            if (words == null || nPosition < 1 || words.Count < nPosition)
            {
                return string.Empty;
            }

            string longestWord = words.OrderByDescending(word => word.Length).Skip(nPosition - 1).First();

            return longestWord;
        }

        /// <summary>
        /// Get count of words made from other words
        /// </summary>
        /// <param name="words">list of existing words</param>
        /// <returns></returns>
        public static int GetWordCountMadeFromOthers(List<string> words)
        {
            if (words == null || words.Count == 0)
            {
                return 0;
            }

            int countWords = 0;

            HashSet<String> setOfWords = new HashSet<String>(words);

            foreach (var word in setOfWords)
            {
                if (IsMadeFromOtherWords(word, setOfWords) == true)
                {
                    countWords++;
                }
            }

            return countWords;
        }

        /// <summary>
        /// Get count of words made to other words
        /// </summary>
        /// <param name="words">list of existing words</param>
        /// <returns></returns>
        public static int GetWordCountMadeToOthers(List<string> words)
        {
            if (words == null || words.Count == 0)
            {
                return 0;
            }

            int countWords = 0;

            HashSet<String> setOfWords = new HashSet<String>(words);

            foreach (var word in setOfWords)
            {
                if (GetCountWordMadeToOtherWords(word, setOfWords) > 1)
                {
                    countWords++;
                }
            }

            return countWords;


            /*
            // other approach, but takes more time.
            var abc = from w1 in words
                      join w2 in words on w1.Contains(w2) = true
            select w1;

            var abc = from w1 in words
                      from w2 in words
                      where w1.Contains(w2)
                      select w2;

            int a = abc.Count();
            */
        }

        /// <summary>
        /// Check if made to other existing words.
        /// </summary>
        /// <param name="word">any existing word</param>
        /// <param name="setOfWords">all existing words</param>
        /// <returns></returns>
        private static int GetCountWordMadeToOtherWords(string word, HashSet<string> setOfWords)
        {
            if (String.IsNullOrEmpty(word))
            {
                return 0;
            }

            int count = 0;

            foreach (var w in setOfWords)
            {
                count += w.Contains(word) ? 1 : 0;

                if (count > 1)
                    break;
            }

            return count;

            /*
            // other approach, but takes more time.
            if (String.IsNullOrEmpty(word))
            {
                return 0;
            }

            int count = setOfWords.Where(w => w.Contains(word)).Count();

            return count;
            */
        }

    }
}
