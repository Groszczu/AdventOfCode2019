using System;
using System.Text;

namespace AdventOfCode.Day8
{
    public class Puzzle2 : Puzzle
    {
        public override string GetResult()
            => CalculateResult();

        private string CalculateResult()
        {
            var sb = new StringBuilder();
            for (var pixel = 0; pixel < ImageWidth*ImageHeight; pixel++)
            {
                var isBlack = false;
                foreach (var layer in Layers)
                {
                    var currentPixel = layer[pixel];
                    if (currentPixel == 2)
                    {
                        continue;
                    }

                    if (currentPixel == 1)
                    {
                        break;
                    }
                    if (currentPixel == 0)
                    {
                        isBlack = true;
                        break;
                    }
                }
                if (pixel % ImageWidth == 0)
                {
                    sb.Append('\n');
                }
                sb.Append(isBlack ? ' ' : '#');
            }
            sb.Append('\n');

            return sb.ToString();
        }
    }
}