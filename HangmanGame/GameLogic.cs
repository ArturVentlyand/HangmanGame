using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame
{
    public class GameLogic
    {
        private readonly int allowedMisses;
        private bool[] openIndexes;
        private int triesCounter = 0;
        private string triedLetters;

        public GameStatus GameStatus { get; private set; } = GameStatus.NotStarted;

        public string Word { get; private set; }    

        public string TriedLetters
        {
            get
            {
                var chars = triedLetters.ToCharArray();
                Array.Sort(chars);
                return new string(chars);
            }
        }

        public int RemainingTries 
        {
            get { return allowedMisses - triesCounter; }
        }

        public GameLogic(int allowedMisses = 6)
        {
            if (allowedMisses < 5 || allowedMisses>8)
            {
                throw new ArgumentException("Number of misses should be between 5 and 8");
            }

            this.allowedMisses = allowedMisses;
        }

        public string GenerateWord()
        {
            string[] words = File.ReadAllLines("WordsStockRus.txt");
            Random random = new Random(DateTime.Now.Millisecond);
            int randomIndex = random.Next(words.Length - 1);

            Word = words[randomIndex];

            openIndexes = new bool[Word.Length];

            GameStatus = GameStatus.InProgress;

            return Word;
        }

        public string GuessLetter(char letter)
        {
            if (triesCounter == allowedMisses)
                throw new InvalidOperationException($"Exceeded the max misses number:{allowedMisses}");
            if (GameStatus != GameStatus.InProgress)
                throw new InvalidOperationException($"Inaproppriate status of the game:{GameStatus}");

            bool openAny = false;
            string result = string.Empty;
            for (int i = 0; i < Word.Length; i++)
            {
                if (Word[i] == letter)
                {
                    openIndexes[i] = true;
                    openAny = true;
                }
                if (openIndexes[i])
                    result += Word[i];
                else
                    result += "_";
            }

            if (!openAny)
                triesCounter++;
            triedLetters += letter;

            if (IsWin())
                GameStatus = GameStatus.Won;
            else if(triesCounter == allowedMisses)
                GameStatus = GameStatus.Lost;

            return result;
        }

        private bool IsWin()
        {
            foreach (var cur in openIndexes)
            {
                if (cur == false)
                    return false;
            }
            return true;
        }
    }
}
