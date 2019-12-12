using System;
using System.Linq;
using System.Text;
using AdventOfCode.Core;

namespace AdventOfCode.Day11
{
    public class Puzzle2 : Puzzle
    {
        public override string GetResult()
            => CalculateResult().ToString();

        private string CalculateResult()
        {
            StartDrawing(isFirstPointWhite: true);

            return GetDrawing();
        }

        private string GetDrawing()
        {
            var maxX = VisitedPoints.Keys.Max(p => p.X);
            var minX = VisitedPoints.Keys.Min(p => p.X);
            var maxY = VisitedPoints.Keys.Max(p => p.Y);
            var minY = VisitedPoints.Keys.Min(p => p.Y);
            var sizeX = maxX - minX + 1;
            var sizeY = maxY - minY + 1;

            var board = new bool[sizeX, sizeY];

            foreach (var pair in VisitedPoints)
            {
                var point = pair.Key;
                var x = point.X - minX;
                var y = point.Y - minY;
                board[x, y] = pair.Value;
            }

            var sb = new StringBuilder("\n");
            for (var column = board.GetLength(1)-1; column >= 0; column--)
            {
                for (var row = 0; row < board.GetLength(0); row++)
                {
                    sb.Append(board[row, column] ? "#" : " ");
                }
                sb.Append('\n');
            }

            return sb.ToString();
        }
    }
}