using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.Write("Enter the path to the local .txt file: ");
        string filePath = Console.ReadLine();

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        if (File.Exists(filePath))
        {
            string bookText;
            using (StreamReader reader = new StreamReader(filePath, Encoding.Default)) // probvah da go nakaram da raboti s kirilica, ne stana
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
            Console.WriteLine("The specified file does not exist.");
        }

        stopwatch.Stop();
        Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
    }

    static string[] GetWords(string text)
    {
        List<string> wordsList = new List<string>();
        string[] words = Regex.Split(text, @"[^\p{L}]+"); 
        foreach (var word in words)
        {
            if (word.Length > 3)
            {
                wordsList.Add(word);
            }
        }
        return wordsList.ToArray();
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
                wordCount[word]++;
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
        topWords.Sort((a, b) => wordCount[b].CompareTo(wordCount[a]));

        for (int i = 0; i < count && i < topWords.Count; i++)
        {
            string word = topWords[i];
            Console.WriteLine($"{word}: {wordCount[word]} times");
        }
    }
}