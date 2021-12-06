using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2021
{
	public class Day6
	{
		[Test]
		[TestCase(80, 390011)]
		[TestCase(256, 1746710169834)]
		public void Puzzle1(int daysToSimulate, long expectedCount)
		{
			var fishes = INPUT.Split(",").Select(int.Parse).ToList();

			/*
			 * insight: there are only 8 valid lifetime values, so we should count how many fish are in each stage,
			 *		instead of simulating each fish itself
			 */

			// initialize the stage list
			var stages = new long[9];

			// populate the stage list
			for (var i = 0; i < fishes.Count; i++)
			{
				var fishStage = fishes[i];
				stages[fishStage]++;
			}

			// simulate the days
			for (var d = 0; d < daysToSimulate; d++)
			{
				var spawnCount = stages[0];

				// down-shift the array elements
				for (var i = 1; i < stages.Length; i++)
				{
					stages[i - 1] = stages[i];
				}

				stages[6] += spawnCount; // the spawners enter a new cycle.
				stages[8] = spawnCount; // the spawned have entered the building
			}

			var total = stages.Sum();
			Console.WriteLine($"There are {total} ");
			Assert.AreEqual(expectedCount, total);
			// TOO HIGH 9446473788
		}


		#region input

		private const string INPUT =
			@"1,3,4,1,1,1,1,1,1,1,1,2,2,1,4,2,4,1,1,1,1,1,5,4,1,1,2,1,1,1,1,4,1,1,1,4,4,1,1,1,1,1,1,1,2,4,1,3,1,1,2,1,2,1,1,4,1,1,1,4,3,1,3,1,5,1,1,3,4,1,1,1,3,1,1,1,1,1,1,1,1,1,1,1,1,1,5,2,5,5,3,2,1,5,1,1,1,1,1,1,1,1,1,1,1,1,2,1,1,1,1,5,1,1,1,1,5,1,1,1,1,1,4,1,1,1,1,1,3,1,1,1,1,1,1,1,1,1,1,1,3,1,2,4,1,5,5,1,1,5,3,4,4,4,1,1,1,2,1,1,1,1,1,1,2,1,1,1,1,1,1,5,3,1,4,1,1,2,2,1,2,2,5,1,1,1,2,1,1,1,1,3,4,5,1,2,1,1,1,1,1,5,2,1,1,1,1,1,1,5,1,1,1,1,1,1,1,5,1,4,1,5,1,1,1,1,1,1,1,1,1,1,1,1,1,2,1,1,1,1,5,4,5,1,1,1,1,1,1,1,5,1,1,3,1,1,1,3,1,4,2,1,5,1,3,5,5,2,1,3,1,1,1,1,1,3,1,3,1,1,2,4,3,1,4,2,2,1,1,1,1,1,1,1,5,2,1,1,1,2";

		#endregion
	}
}