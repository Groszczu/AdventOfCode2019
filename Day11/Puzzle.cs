using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day11
{
    public abstract class Puzzle : IPuzzle
    {
        protected static List<long> Program { get; private set; }

        protected IntcodeComputer IntcodeComputer { get; private set; }

        // dictionary of visited points and its' colors (true => white, false => black)
        protected Dictionary<Point, bool> VisitedPoints { get; private set; } = new Dictionary<Point, bool> { { new Point(0, 0), false } };
        protected Point RobotPosition { get; set; } = new Point(0, 0);
        protected Direction RobotDirection { get; private set; } = Direction.North;

        private static readonly Dictionary<Direction, Direction> _afterTurningLeftDirections = new Dictionary<Direction, Direction>
        {
            { Direction.North, Direction.West },
            { Direction.West, Direction.South },
            { Direction.South, Direction.East },
            { Direction.East, Direction.North },
        };

        private static readonly Dictionary<Direction, Direction> _afterTurningRightDirections
            = new Dictionary<Direction, Direction>(_afterTurningLeftDirections.ToDictionary(x => x.Value, x => x.Key));

        public void LoadInput(string inputPath)
        {
            Program = File.ReadAllText(inputPath)
                .Split(',')
                .Select(long.Parse)
                .ToList();

            IntcodeComputer = new IntcodeComputer(Program);
        }

        public abstract string GetResult();

        protected void StartDrawing(bool isFirstPointWhite)
        {
            IntcodeComputer.AddInputValue(isFirstPointWhite ? 1 : 0);

            while (!IntcodeComputer.FinishedProgram)
            {
                IntcodeComputer.RunProgram(breakOnOutput: true);
                var isWhitePaint = IntcodeComputer.OutputValue == 1;
                RobotPaint(isWhitePaint);

                IntcodeComputer.RunProgram(breakOnOutput: true);
                var turnRight = IntcodeComputer.OutputValue == 1;
                if (turnRight)
                {
                    RobotTurnRight();
                }
                else
                {
                    RobotTurnLeft();
                }

                RobotMoveForeward();
                if (VisitedPoints.ContainsKey(RobotPosition))
                {
                    var isPointWhite = VisitedPoints[RobotPosition];
                    if (isPointWhite)
                    {
                        IntcodeComputer.AddInputValue(1);
                    }
                    else
                    {
                        IntcodeComputer.AddInputValue(0);
                    }
                }
                else
                {
                    VisitedPoints.Add(new Point(RobotPosition), false);
                    IntcodeComputer.AddInputValue(0);
                }
            }
        }

        protected void RobotMoveForeward()
        {
            switch (RobotDirection)
            {
                case Direction.North:
                    RobotPosition.Y++;
                    break;
                case Direction.West:
                    RobotPosition.X--;
                    break;
                case Direction.South:
                    RobotPosition.Y--;
                    break;
                case Direction.East:
                    RobotPosition.X++;
                    break;
            }
        }

        protected void RobotPaint(bool isWhitePaint)
            => VisitedPoints[RobotPosition] = isWhitePaint;

        protected void RobotTurnLeft()
            => RobotDirection = _afterTurningLeftDirections[RobotDirection];

        protected void RobotTurnRight()
            => RobotDirection = _afterTurningRightDirections[RobotDirection];
    }
}