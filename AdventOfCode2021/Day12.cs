using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCode2021
{
	public class Day12
	{

		[TestCase(SAMPLE, 10)]
		[TestCase(SAMPLE2, 19)]
		[TestCase(SAMPLE3, 226)]
		[TestCase(INPUT, 226)]
		public void Puzzle1(string input, int expected)
		{
			var edges = input.Split(Environment.NewLine);

			var graph = new Dictionary<string, List<string>>();
			foreach (var edge in edges)
			{
				var parts = edge.Split('-');
				if (parts[0].ToUpper() == parts[0] && parts[1] == parts[1].ToUpper())
				{
					throw new Exception("There is a cycle!");
				}

				if (graph.TryGetValue(parts[0], out var existing))
				{
					existing.Add(parts[1]);
				}
				else
				{
					graph[parts[0]] = new List<string> {parts[1]};
				}

				if (graph.TryGetValue(parts[1], out existing))
				{
					existing.Add(parts[0]);
				}
				else
				{
					graph[parts[1]] = new List<string> {parts[0]};
				}

			}

			var total = 0;
			void Search(string node, HashSet<string> visited)
			{
				var neighbors = graph[node];
				foreach (var neighbor in neighbors)
				{
					if (visited.Contains(neighbor)) continue;

					if (neighbor == "start") continue;


					if (neighbor == "end") // base case
					{
						total++;
						continue;
					} else if (neighbor.ToLower() == neighbor) // small case
					{
						var nextV = new HashSet<string>(visited);
						nextV.Add(neighbor);
						Search(neighbor, nextV);
					} else if (neighbor.ToUpper() == neighbor) // big cave
					{
						Search(neighbor, visited);
					}
				}
			}

			Search("start", new HashSet<string>());

			Assert.AreEqual(expected, total);
		}

		[TestCase(SAMPLE, 36)]
		[TestCase(SAMPLE2, 103)]
		[TestCase(SAMPLE3, 3509)]
		[TestCase(INPUT, 96988)]
		public void Puzzle2(string input, int expected)
		{
			var edges = input.Split(Environment.NewLine);

			var graph = new Dictionary<string, List<string>>();
			var smallCaves = new HashSet<string>();
			foreach (var edge in edges)
			{
				var parts = edge.Split('-');
				if (parts[0].ToLower() == parts[0]) smallCaves.Add(parts[0]);
				if (parts[1].ToLower() == parts[1]) smallCaves.Add(parts[1]);

				if (parts[0].ToUpper() == parts[0] && parts[1] == parts[1].ToUpper())
				{
					throw new Exception("There is a cycle!");
				}

				if (graph.TryGetValue(parts[0], out var existing))
				{
					existing.Add(parts[1]);
				}
				else
				{
					graph[parts[0]] = new List<string> {parts[1]};
				}

				if (graph.TryGetValue(parts[1], out existing))
				{
					existing.Add(parts[0]);
				}
				else
				{
					graph[parts[1]] = new List<string> {parts[0]};
				}

			}

			smallCaves.Remove("start");
			smallCaves.Remove("end");

			var total = 0;

			var paths = new HashSet<string>();

			void Search(string node, HashSet<string> visited, string special, string path)
			{
				var neighbors = graph[node];
				foreach (var neighbor in neighbors)
				{
					if (visited.Contains(neighbor)) continue;

					if (neighbor == "start") continue;

					if (neighbor == "end") // base case
					{
						if (paths.Contains(path))
						{
							continue;
						}
						total++;
						paths.Add(path);
						Console.WriteLine(path);
						continue;
					} else if (neighbor.ToUpper() == neighbor) // big cave
					{
						Search(neighbor, visited, special, $"{path},{neighbor}");
					}
					else if (neighbor == special)
					{
						Search(neighbor, visited, "", $"{path},{neighbor}");
					}
					else if (neighbor.ToLower() == neighbor) // small case
					{
						var nextV = new HashSet<string>(visited);
						nextV.Add(neighbor);
						Search(neighbor, nextV, special, $"{path},{neighbor}");
					}

				}
			}

			foreach (var smallCave in smallCaves)
			{
				Search("start", new HashSet<string>(), smallCave, "start");
			}

			Assert.AreEqual(expected, total);
		}

		#region input

		private const string SAMPLE = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end";

		private const string SAMPLE2 = @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc";

		private const string SAMPLE3 = @"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW";

		private const string INPUT = @"ey-dv
AL-ms
ey-lx
zw-YT
hm-zw
start-YT
start-ms
dv-YT
hm-ms
end-ey
AL-ey
end-hm
rh-hm
dv-ms
AL-dv
ey-SP
hm-lx
dv-start
end-lx
zw-AL
hm-AL
lx-zw
ey-zw
zw-dv
YT-ms";

		#endregion
	}
}