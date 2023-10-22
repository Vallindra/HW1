using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.Write("Enter the path to the local .txt file(english text): ");
        string filePath = Console.ReadLine();
        int threadCount = Environment.ProcessorCount*2;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        if (File.Exists(filePath))
        {
            string bookText;
            using (StreamReader reader = new StreamReader(filePath, Encoding.Default))
            {
                bookText = reader.ReadToEnd();
            }
            string[] words = GetWords(bookText);
            Console.WriteLine($"1. Number of words: {words.Length}");
            Console.WriteLine($"2. Shortest word: {FindShortestWord(words)}");
            Console.WriteLine($"3. Longest word: {FindLongestWord(words)}");
            Console.WriteLine($"4. Average word length: {CalculateAverageWordLength(words):F2}");
            Console.WriteLine("5. Five most common words:");
            DisplayTopWords(FindMostCommonWords(words), 5);
            Console.WriteLine("6. Five least common words:");
            DisplayTopWords(FindLeastCommonWords(words), 5);
        }
        else
        {
            Console.WriteLine("Wrong file path...");
        }

        stopwatch.Stop();
        Console.WriteLine($"Non-Threaded Execution Time: {stopwatch.ElapsedMilliseconds} ms");
        //-------------------------------------------------------------------------------------------------
        stopwatch.Restart();
        if (File.Exists(filePath))
        {
            string bookText;
            using (StreamReader reader = new StreamReader(filePath, Encoding.Default))
            {
                bookText = reader.ReadToEnd();
            }

            string[] words = GetWordsThreaded(bookText, threadCount);

            Console.WriteLine($"1. Number of words: {words.Length}");
            Console.WriteLine($"2. Shortest word: {FindShortestWord(words)}");
            Console.WriteLine($"3. Longest word: {FindLongestWord(words)}");
            Console.WriteLine($"4. Average word length: {CalculateAverageWordLength(words):F2}");
            Console.WriteLine("5. Five most common words:");
            DisplayTopWords(FindMostCommonWords(words), 5);
            Console.WriteLine("6. Five least common words:");
            DisplayTopWords(FindLeastCommonWords(words), 5);
        }
        else
        {
            Console.WriteLine("Wrong file path...");
        }
        stopwatch.Stop();
        Console.WriteLine($"Threaded Execution Time: {stopwatch.ElapsedMilliseconds} ms");
    }
    // metodi ---------------------------------------------------------------------------------------------------------
    static string[] GetWords(string text)
    {
        string[] words = Regex.Split(text, @"[^\p{L}]+");
        List<string> filteredWords = new List<string>();
        foreach (var word in words)
        {
            if (word.Length > 3)  // didn't know if it should be => or > but it's boring if => cuz "the" is always the most common
            {
                filteredWords.Add(word);
            }
        }

        return filteredWords.ToArray();
    }

    static string[] GetWordsThreaded(string text, int threadCount)
    {
        string[] words = Regex.Split(text, @"[^\p{L}]+");
        List<string> filteredWords = new List<string>();
        object lockObj = new object();
        Thread[] threads = new Thread[threadCount];
        int wordsPerThread = words.Length / threadCount;
        for (int i = 0; i < threadCount; i++)
        {
            int startIndex = i * wordsPerThread;
            int endIndex = (i == threadCount - 1) ? words.Length : (i + 1) * wordsPerThread;
            threads[i] = new Thread(() =>
            {
                for (int j = startIndex; j < endIndex; j++)
                {
                    if (words[j].Length > 3)
                    {
                        lock (lockObj)
                        {
                            filteredWords.Add(words[j]);
                        }
                    }
                }
            });
            threads[i].Start();
        }

        for (int i = 0; i < threadCount; i++)
        {
            threads[i].Join();
        }

        return filteredWords.ToArray();


    }

    static string FindShortestWord(string[] words)
    {
        string shortestWord = words[0];

        foreach (var word in words)
        {
            if (word.Length < shortestWord.Length)
            {
                shortestWord = word;
            }
        }

        return shortestWord;

    }

    static string FindLongestWord(string[] words)
    {
        string longestWord = words[0];

        foreach (var word in words)
        {
            if (word.Length > longestWord.Length)
            {
                longestWord = word;
            }
        }


        return longestWord;
    }

    static double CalculateAverageWordLength(string[] words)
    {
        int totalLength = 0;
        foreach (var word in words)
        {

            totalLength += word.Length;
        }

        return (double)totalLength / words.Length;
    }

    static Dictionary<string, int> FindMostCommonWords(string[] words)
    {
        var wordCount = new Dictionary<string, int>();

        foreach (var word in words)
        {
            if (wordCount.ContainsKey(word))
                wordCount[word]++;
            else
                wordCount[word] = 1;
        }

        return wordCount;

    }

    static Dictionary<string, int> FindLeastCommonWords(string[] words)
    {
        var wordCount = new Dictionary<string, int>();

        foreach (var word in words)
        {
            if (wordCount.ContainsKey(word))
                wordCount[word]--;
            else
                wordCount[word] = 1;
        }

        return wordCount;

    }

    static void DisplayTopWords(Dictionary<string, int> wordCount, int count)
    {
        List<string> topWords = new List<string>();
        foreach (var pair in wordCount)
        {
            topWords.Add(pair.Key);
        }
        topWords.Sort((a, b) => wordCount[b].CompareTo(wordCount[a]));  // holy shit it's the comparator from java wawawawawawa
        for (int i = 0; i < count && i < topWords.Count; i++)
        {
            string word = topWords[i];
            Console.WriteLine($"{word}: {wordCount[word]} times");

        }
    }
}