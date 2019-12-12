using System;
using AdventOfCode.Core;

namespace AdventOfCode
{
    class Program
    {
        const int NumberOfPuzzlesPerDay = 2;
        static void Main()
        {
            System.Console.WriteLine("Enter AdventOfCode day number:");
            var day = Console.ReadLine();
            if (!IsCorrectDayInput(day))
            {
                System.Console.WriteLine("AdventOfCode has 25 days. Please enter number between 1 and 25");
                return;
            }

            System.Console.WriteLine($"Day {day}:");

            for (var puzzleNumber = 1; puzzleNumber <= NumberOfPuzzlesPerDay; puzzleNumber++)
            {
                Type puzzleType = Type.GetType($"AdventOfCode.Day{day}.Puzzle{puzzleNumber}");
                if (puzzleType == null)
                {
                    System.Console.WriteLine($"Day {day} is not implemented yet");
                    break;
                }

                IPuzzle puzzle = (IPuzzle)Activator.CreateInstance(puzzleType);
                puzzle.LoadInput($"Day{day}\\InputFiles\\puzzleInput.txt");
                System.Console.WriteLine($"Puzzle {puzzleNumber} result: {puzzle.GetResult()}");
            }
        }

        static bool IsCorrectDayInput(string input)
        {
            var valueOfDay = 0;
            try
            {
                valueOfDay = int.Parse(input);
            }
            catch (FormatException)
            {
                return false;
            }

            if (valueOfDay < 1 || valueOfDay > 25)
            {
                return false;
            }

            return true;
        }
    }
}