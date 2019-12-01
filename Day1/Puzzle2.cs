using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using AdventOfCode.Core;

namespace AdventOfCode.Day1
{
    public class Puzzle2 : IPuzzle
    {
        private IEnumerable<int> modulesMasses;
        public void LoadInput(string inputPath)
        {
            var logFile = File.ReadAllLines(inputPath);
            modulesMasses = new List<string>(logFile)
                                .Select(line => int.Parse(line));
        }

        public string GetResult()
            => CalculateFuelNeeded();

        private string CalculateFuelNeeded()
        {
            var totalFuelNeeded = 0;
            foreach (var mass in modulesMasses)
            {
                var curr = mass / 3 - 2;
                for (var fuelNeeded = mass/3 - 2; fuelNeeded > 0; fuelNeeded = fuelNeeded/3 - 2)
                {
                    totalFuelNeeded += fuelNeeded;
                }
            }

            return totalFuelNeeded.ToString();
        }
    }
}