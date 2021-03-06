using System;
using System.Linq;

namespace AdventOfCode.Day4
{
    public class Puzzle2 : Puzzle
    {
        public override string GetResult()
            => CalculateResult().ToString();

        private int CalculateResult()
        {
            var result = Enumerable.Range(Min, Max - Min)
                .Count(n => 
                {
                    var digits = GetArrayOfDigits(n);

                    return IsOrdered(digits) && HasExactlyTwoAdjacentDigits(digits);
                });
            
            return result;
        }

        private bool HasExactlyTwoAdjacentDigits(int[] digits)
        {
            var digitsWhithIndexes = digits
                .Select((d, i) => (digit: d, index: i))
                .Where(p => digits.Count(x => x == p.digit) == 2);

            var digitPairs = digitsWhithIndexes
                .Select(p => (firstIndex: p.index, secondIndex: digitsWhithIndexes.First(x => x.index != p.index && x.digit == p.digit).index));
            
            return digitPairs.Any(p => Math.Abs(p.firstIndex - p.secondIndex) == 1);
        }
    }
}