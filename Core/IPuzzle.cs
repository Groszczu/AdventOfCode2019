namespace AdventOfCode.Core
{
    public interface IPuzzle
    {
        void LoadInput(string inputPath);
        int GetResult();
    }
}