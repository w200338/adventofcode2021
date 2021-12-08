namespace AdventOfCode2021.Tools.Enums
{
	using System;

	public static class EnumUtils
	{
		public static Direction TurnRight(Direction input)
		{
			switch (input)
			{
				case Direction.Right:
					return Direction.Down;
				case Direction.Up:
					return Direction.Right;
				case Direction.Left:
					return Direction.Up;
				case Direction.Down:
					return Direction.Left;
				default:
					throw new ArgumentOutOfRangeException(nameof(input), input, null);
			}
		}

		public static Direction TurnLeft(Direction input)
		{
			switch (input)
			{
				case Direction.Right:
					return Direction.Up;
				case Direction.Up:
					return Direction.Left;
				case Direction.Left:
					return Direction.Down;
				case Direction.Down:
					return Direction.Right;
				default:
					throw new ArgumentOutOfRangeException(nameof(input), input, null);
			}
		}

		public static Compass TurnRight(Compass compass)
		{
			switch (compass)
			{
				case Compass.North:
					return Compass.East;
				case Compass.East:
					return Compass.South;
				case Compass.South:
					return Compass.West;
				case Compass.West:
					return Compass.North;
				default:
					throw new ArgumentOutOfRangeException(nameof(compass), compass, null);
			}
		}

		public static Compass TurnLeft(Compass compass)
		{
			switch (compass)
			{
				case Compass.North:
					return Compass.West;
				case Compass.East:
					return Compass.North;
				case Compass.South:
					return Compass.East;
				case Compass.West:
					return Compass.South;
				default:
					throw new ArgumentOutOfRangeException(nameof(compass), compass, null);
			}
		}
	}
}