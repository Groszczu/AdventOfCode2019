using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day8
{
    public class Puzzle1 : Puzzle
    {
        
        public override string GetResult()
            => CalculateResult().ToString();

        private int CalculateResult()
        {
            var fewestZerosLayer = Layers.OrderBy(l => l.Count(n => n == 0))
                .First();

            var result = fewestZerosLayer.Count(n => n == 1) * fewestZerosLayer.Count(n => n == 2);
            return result;
        }
    }
}