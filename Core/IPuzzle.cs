namespace AdventOfCode.Core
{
    public interface IPuzzle
    {
        void LoadInput(string inputPath);
        string GetResult();
    }
}