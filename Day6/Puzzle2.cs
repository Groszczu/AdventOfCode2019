
namespace AdventOfCode.Day6
{
    public class Puzzle2 : Puzzle
    {
        public override string GetResult()
            => ObjectsTree.OrbitalTransfersBetween("YOU", "SAN").ToString();
    }
}