using System;

namespace AdventOfCode.Day8
{
    public class Puzzle2 : Puzzle
    {
        public override int GetResult()
            => CalculateResult();

        private int CalculateResult()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
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
                    Console.WriteLine();
                }
                Console.Write(isBlack ? '\u25AE' : ' ');
            }
            Console.WriteLine();

            return 0;
        }
    }
}