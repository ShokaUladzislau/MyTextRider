using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextReader
{
    public class ReaderClass
    {
        string text;
        string writeWordsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Words.txt");
        string writeSentensPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Statistic.txt");
        string strofsentes = string.Empty;

        
        internal void Read()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sample.txt");
            using (StreamReader reader = new StreamReader(path))
            { 
                text =  reader.ReadToEnd();
            }
        }
       
        internal void Sentenses()
        {
            string[] sentences = text.Split("");
            foreach (var sentence in sentences)
            {
                strofsentes += sentence;
            } 
            strofsentes = Regex.Replace(strofsentes, @"[\n\r\t\s]", " ");
            strofsentes = Regex.Replace(strofsentes, @"[.?!]+\s", "|\n");        }
        
        internal void ShowSentenses()
        {
            Console.WriteLine(strofsentes);
        }

        internal void ShowWords()
        {
            string[] words = text.Split();
            foreach (var word in words)
            {
                MatchCollection wordmatches = Regex.Matches(word, @"(.+\w)|\w");
                foreach (Match wordmatch in wordmatches)
                {
                    Console.WriteLine(wordmatch.Value);
                }
            }
        }

        internal void ShowMarks()
        {
            string[] marks = text.Split();
            foreach (var mark in marks)
            {
                MatchCollection markmatches = Regex.Matches(mark, @"\W");
                foreach (Match markmatch in markmatches)
                {
                    Console.WriteLine(markmatch.Value);
                }
            }
        }
        
        internal void WriteWords()
        {
            List<string> wordslist = new List<string>();
            string[] words = text.Split();
            foreach (var word in words)
            {
                MatchCollection wordmatches = Regex.Matches(word, @"[\w-]+");
                foreach (Match wordmatch in wordmatches)
                {
                    wordslist.Add(wordmatch.Value);
                }
            }

            wordslist.Sort();

            foreach (var val in wordslist.Distinct())
            {
                try
                {
                    using (StreamWriter wordswrire = new StreamWriter(writeWordsPath, true , Encoding.Default))
                    {
                        wordswrire.WriteLine(val + " - " + wordslist.Where(x => x == val).Count() + " times");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            Console.WriteLine("Words was be saved in file - \"Words.txt\"");

        }

        internal void WriteSentence()
        {
            Sentenses();
            StringBuilder builder = new StringBuilder();
            int max = 0;
            foreach (var item in strofsentes.Split('\n'))
            {
                if (item.Length>max)
                {
                    max = item.Length;
                    builder = new StringBuilder(item);
                }
            }
            try
            {
                using (StreamWriter sentencewrite = new StreamWriter(writeSentensPath, true , Encoding.Default))
                {
                    sentencewrite.WriteLine($"Bigest sentence is a :\n{builder}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.WriteLine("Bigest sentence was be saved in file - \"Statictic.txt\"");

        }
        internal void WriteLetter()
        {
            List<char> letterslist = new List<char>();
            text = Regex.Replace(text, @"\W", "");
            letterslist.AddRange(text.ToCharArray());
            letterslist.Sort();
                
            try
            {
                using (StreamWriter letterswrite = new StreamWriter(writeSentensPath, true , Encoding.Default))
                {
                    letterswrite.WriteLine($"most repeated letter - {letterslist.Select(x => new { Symbol = x, Count = letterslist.Count(y => y == x) }).OrderByDescending(x => x.Count).First().Symbol}");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.WriteLine("Most repeated letter was be saved in file - \"Statictic.txt\"");
        }
    }
}