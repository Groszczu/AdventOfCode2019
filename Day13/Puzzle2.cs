using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day13
{
    public class Puzzle2 : Puzzle
    {
        private const int FirstInstruction = 2;
        private Point _ballPosition;
        private Point _paddlePosition;
        private long _score = 0;

        public override string GetResult()
            => CalculateResult().ToString();

        private long CalculateResult()
        {
            IntcodeComputer.OverrideFirstInstruction(FirstInstruction);

            while (!IntcodeComputer.FinishedProgram)
            {
                IntcodeComputer.RunProgram(breakOnOutput: true);
                var column = IntcodeComputer.OutputValue;

                IntcodeComputer.RunProgram(breakOnOutput: true);
                var row = IntcodeComputer.OutputValue;

                IntcodeComputer.RunProgram(breakOnOutput: true);
                var tileId = IntcodeComputer.OutputValue;

                var pos = new Point(column, row);
                if (pos.X == -1 && pos.Y == 0)
                {
                    _score = tileId;
                    continue;
                }
                
                var tileType = (TileType)tileId;
                if (tileType == TileType.Ball)
                {
                    _ballPosition = pos;
                    var comparison = _ballPosition?.X.CompareTo(_paddlePosition?.X);
                    if (comparison.HasValue)
                    {
                        IntcodeComputer.AddInputValue(comparison.Value);
                    }
                }
                if(tileType == TileType.HorizontalPaddle)
                {
                    _paddlePosition = pos;
                }
            }

            return _score;
        }
    }
}