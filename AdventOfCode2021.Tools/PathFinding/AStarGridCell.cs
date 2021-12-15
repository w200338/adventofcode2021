namespace AdventOfCode2021.Tools.PathFinding
{
	public class AStarGridCell : GridCell
	{
		public double GScore { get; set; } = double.PositiveInfinity;
		public double FScore { get; set; } = double.PositiveInfinity;
	}
}