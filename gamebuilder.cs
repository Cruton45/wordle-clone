using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleClone;

public class GameBuilder
{
    private static String WordFile = "words.txt";
    private static int WordFileLength;
    public static String Word;
    public static IDictionary<string, List<char>> guessesTable = new Dictionary<string, List<char>>();
    private static Random rand = new Random();
    static GameBuilder()
    {
        guessesTable.Add("almostCorrect", new List<char>());
        guessesTable.Add("correct", new List<char>());
        for (int i = 0; i < 5; i++)
        {
            guessesTable["almostCorrect"].Add('|');
            guessesTable["correct"].Add('|');
        }
        WordFileLength = File.ReadLines(WordFile).Count();
        Word = File.ReadLines(WordFile).Skip(rand.Next(1, WordFileLength - 1)).Take(1).First().ToLower();
    }
}