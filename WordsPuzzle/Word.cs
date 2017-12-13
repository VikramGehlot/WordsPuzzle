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

    }
}
