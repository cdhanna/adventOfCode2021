using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2021
{
	public class Day11
	{
		IEnumerable<(int, int)> GetNeighborPositions((int, int) spot)
		{
			yield return (spot.Item1 - 1, spot.Item2);
			yield return (spot.Item1 + 1, spot.Item2);
			yield return (spot.Item1, spot.Item2 - 1);
			yield return (spot.Item1, spot.Item2 + 1);
			yield return (spot.Item1 - 1, spot.Item2 - 1);
			yield return (spot.Item1 - 1, spot.Item2 + 1);
			yield return (spot.Item1 + 1, spot.Item2 - 1);
			yield return (spot.Item1 + 1, spot.Item2 + 1);
		}


		[TestCase(SAMPLE, 1656)]
		[TestCase(INPUT, 1656)]
		public void Puzzle1(string input, int expected)
		{
			var map = new Dictionary<(int, int), int>();
			var lines = input.Split(Environment.NewLine);
			for (var y = 0; y < lines.Length; y++)
			{
				for (var x = 0; x < lines[y].Length; x++)
				{
					map[(x, y)] = int.Parse(lines[y][x].ToString());
				}
			}

			var flashes = 0;
			var totalSteps = 100;
			for (var i = 0; i < totalSteps; i++)
			{
				// identify which spots need to flash. Add them to a set of flashed.
				var totalFlashed = 1;
				var flashedInPhase = new HashSet<(int, int)>();

				var explorationSet = new HashSet<(int, int)>(map.Keys);

				// add 1 to everything
				foreach (var spot in explorationSet)
				{
					map[spot]++;
				}

				while (totalFlashed > 0)
				{
					var stepFlashes = new HashSet<(int, int)>();
					foreach (var spot in explorationSet)
					{
						if (!flashedInPhase.Contains(spot) && map[spot] > 9)
						{
							flashedInPhase.Add(spot);
							stepFlashes.Add(spot);
							flashes++;
						}
					}
					explorationSet.Clear();
					totalFlashed = stepFlashes.Count;

					// all of the places next to a flash spot increase by 1.
					foreach (var flasher in stepFlashes)
					{
						var neighbors = GetNeighborPositions(flasher).Where(map.ContainsKey);
						foreach (var neighbor in neighbors)
						{
							map[neighbor] += 1;
							explorationSet.Add(neighbor);
						}
					}
				}

				// reset all flashed back to 0
				foreach (var spot in flashedInPhase)
				{
					map[spot] = 0;
				}

			}

			Assert.AreEqual(expected, flashes);
		}


		[TestCase(SAMPLE, 195)]
		[TestCase(INPUT, 510)]
		public void Puzzle2(string input, int expected)
		{
			var map = new Dictionary<(int, int), int>();
			var lines = input.Split(Environment.NewLine);
			for (var y = 0; y < lines.Length; y++)
			{
				for (var x = 0; x < lines[y].Length; x++)
				{
					map[(x, y)] = int.Parse(lines[y][x].ToString());
				}
			}

			var totalSteps = 100;
			var i = 0;
			while (i < 99999)
			// for (var i = 0; i < totalSteps; i++)
			{
				// identify which spots need to flash. Add them to a set of flashed.
				var totalFlashed = 1;
				var flashedInPhase = new HashSet<(int, int)>();

				var explorationSet = new HashSet<(int, int)>(map.Keys);

				// add 1 to everything
				foreach (var spot in explorationSet)
				{
					map[spot]++;
				}

				var flashes = 0;

				while (totalFlashed > 0)
				{
					var stepFlashes = new HashSet<(int, int)>();
					foreach (var spot in explorationSet)
					{
						if (!flashedInPhase.Contains(spot) && map[spot] > 9)
						{
							flashedInPhase.Add(spot);
							stepFlashes.Add(spot);
							flashes++;
						}
					}
					explorationSet.Clear();
					totalFlashed = stepFlashes.Count;

					// all of the places next to a flash spot increase by 1.
					foreach (var flasher in stepFlashes)
					{
						var neighbors = GetNeighborPositions(flasher).Where(map.ContainsKey);
						foreach (var neighbor in neighbors)
						{
							map[neighbor] += 1;
							explorationSet.Add(neighbor);
						}
					}
				}

				// reset all flashed back to 0
				foreach (var spot in flashedInPhase)
				{
					map[spot] = 0;
				}

				i++;
				if (flashes == map.Count)
				{
					break;
				}
			}

			Assert.AreEqual(expected, i);
		}

		#region input

		private const string SAMPLE = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";

		private const string INPUT = @"3322874652
5636588857
7755117548
5854121833
2856682477
3124873812
1541372254
8634383236
2424323348
2265635842";

		#endregion
	}
}