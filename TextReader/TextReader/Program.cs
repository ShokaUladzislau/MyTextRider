using System;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TextReader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ReaderClass readerClass = new ReaderClass();
            readerClass.Read();
            
            readerClass.ShowSentenses();
            readerClass.ShowWords();
            readerClass.ShowMarks();
            
            readerClass.WriteWords();
            readerClass.WriteSentence();
            readerClass.WriteLetter();
            
        }
    }
}