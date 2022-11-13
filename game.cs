using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleClone;

class Game
{
    public static GameBuilder? gameBuilder;
    public static String? Word;
    public static int guessNumber = 6;
    public static string? Guess = ".....";
    public static IDictionary<string, List<char>> guessesTable = new Dictionary<string, List<char>>();
    static void Main(string[] args)
    {
        gameBuilder = new GameBuilder();
        new apihelper();
        Word = gameBuilder.Word;
        guessesTable = gameBuilder.guessesTable;
        Console.WriteLine(Word);
        GameLoop();
    }

    static void GameLoop()
    {
        Console.WriteLine("Welcome to Wordle Clone!\nA random five letter word has been selected. Guess words to find the correct answer.");
        while (!GameIsOver())
        {
            CheckGuess();
            Console.Write("Letters in correct order: ");
            foreach (char c in guessesTable["correct"])
            {
                Console.Write(c);
            }
            Console.WriteLine();
            Console.Write("Letters in the word but not in correct order: ");
            foreach (char c in guessesTable["almostCorrect"])
            {
                Console.Write(c);
            }
            Console.WriteLine();

            Console.WriteLine("Guesses left: " + guessNumber.ToString());
            Console.Write("Enter your guess: ");

            Guess = Console.ReadLine().ToLower();
            if (!WordIsValid(Guess))
            {
                Guess = ".....";
                Console.WriteLine("Your guess was invalid! Please try again.");
                continue;
            }
            guessNumber--;
        }
    }
    static bool WordIsValid(string? guess)
    {
        if (guess == null) return false;
        if (guess.Length < 5 || guess.Length > 5) return false;
        if (!apihelper.IsValidWord(guess)) return false;
        return true;
    }
    static bool GameIsOver()
    {
        if (guessNumber <= 0)
        {
            GameEnd(false);
            return true;
        }
        else if (Word == Guess)
        {
            GameEnd(true);
            return true;
        }
        return false;
    }
    static void CheckGuess()
    {
        int NumOfFoundLetters = 0;
        for (int i = 0; i < Guess.Length; i++)
        {
            if (Word.Contains(Guess[i]))
            {
                if (Word.IndexOf(Guess[i]) != i)
                {
                    if (NumOfFoundLetters > Word.Length) continue;
                    if (guessesTable["almostCorrect"].Contains(Guess[i])) continue;
                    guessesTable["almostCorrect"].Remove('|');
                    guessesTable["almostCorrect"].Add(Guess[i]);
                    NumOfFoundLetters++;
                }
                else if (Word.IndexOf(Guess[i]) == i)
                {
                    guessesTable["correct"][i] = Guess[i];
                    Guess.Remove(i);
                    if (guessesTable["almostCorrect"].Contains(Guess[i]))
                    {
                        guessesTable["almostCorrect"].Remove(Guess[i]);
                        guessesTable["almostCorrect"].Add('|');
                    }
                }
            }
        }
    }

    static void GameEnd(bool won)
    {
        if(won)
        {
            Console.Write("Congratulations you get the word correct!");
        }
        else
        {
            Console.Write("Sorry you ran out of guesses!");
        }
        Console.Write(" The word was " + Word + ".\n");
    }
}