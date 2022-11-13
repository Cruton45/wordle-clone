using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleClone;

public class GameBuilder
{
    private String WordFile = "words.txt";
    private int WordFileLength;
    public String Word;
    public IDictionary<string, List<char>> guessesTable = new Dictionary<string, List<char>>();
    private Random rand = new Random();
    public GameBuilder()
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