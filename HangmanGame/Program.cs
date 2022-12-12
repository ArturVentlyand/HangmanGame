using System;

namespace HangmanGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameLogic game = new GameLogic();

            string word = game.GenerateWord();

            Console.WriteLine($"The word consists of {word.Length} letters.");
            Console.WriteLine("Try to guess the word.");

            while(game.GameStatus == GameStatus.InProgress)
            {
                Console.WriteLine("Pick a letter");
                char c = Console.ReadLine().ToCharArray()[0];

                string curState = game.GuessLetter(c);
                Console.WriteLine(curState);

                Console.WriteLine($"Remaining tries = {game.RemainingTries}");
                Console.WriteLine($"Tried letters:{game.TriedLetters}");
            }

            if (game.GameStatus == GameStatus.Lost)
            {
                Console.WriteLine($"You're hanged.\n" +
                    $"The word was: {game.Word}");
            }
            else if (game.GameStatus == GameStatus.Won)
            {
                Console.WriteLine("You Won!");
            }
        }
    }
}