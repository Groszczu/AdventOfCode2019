using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day4
{
    public class Puzzle1 : Puzzle
    {
        public override string GetResult()
            => CalculateResult().ToString();

        private int CalculateResult()
        {
            var result = Enumerable.Range(Min, Max - Min)
                .Count(n =>
                {
                    var digits = GetArrayOfDigits(n);
                    return IsOrdered(digits) && HasSameAdjacentDigits(digits);
                });

            return result;
        }

        private bool HasSameAdjacentDigits(int[] digits)
        {
            return digits
                .Where((d, i) => i != 0 && d == digits[i-1])
                .Any();
        }
    }
}