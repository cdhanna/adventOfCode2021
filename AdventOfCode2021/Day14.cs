using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace AdventOfCode2021
{
	public class Day14
	{
		public class InsertionRule
		{
			public string Find;
			public char Replace;
		}

		[TestCase("same10", SAMPLE, 1588, 10)]
		[TestCase("sample40", SAMPLE, 2188189693529, 40)]
		[TestCase("input10", INPUT, 2797, 10)]
		[TestCase("input40", INPUT, 2797, 40)]
		public void Puzzle1(string name, string input, long expected, int rounds)
		{
			var lines = input.Split(Environment.NewLine);
			var polymer = lines[0];
			var rules = new List<InsertionRule>(lines.Length - 2);
			for (var i = 2; i < lines.Length; i++)
			{
				var parts = lines[i].Split(" -> ");
				rules.Add(new InsertionRule
				{
					Find = parts[0],
					Replace = parts[1][0]
				});
			}

			for (var i = 0; i < rounds; i++)
			{
				Console.WriteLine("Starting round " + i);
				// find all matches, and record their index values, then insert the data back to front to save index values
				var inserts = new Dictionary<int, string>();
				foreach (var rule in rules)
				{
					for (var j = 1; j < polymer.Length; j++)
					{
						if (polymer[j - 1] == rule.Find[0] && polymer[j] == rule.Find[1])
						{
							inserts.Add(j, rule.Replace.ToString());
						}
					}
				}
				Console.WriteLine("inserting for round " + i);

				// do the inserts backwards through the list
				for (var j = polymer.Length - 1; j >= 0; j--)
				{
					if (inserts.TryGetValue(j, out var replace))
					{
						polymer = polymer.Insert(j, replace);
					}
				}


			}


			// count occurances
			long maxCount = polymer.GroupBy(c => c).Max(g => g.Count());
			long minCount = polymer.GroupBy(c => c).Min(g => g.Count());

			var value = maxCount - minCount;
			Assert.AreEqual(expected, value);
		}


		class Node
		{
			public Node prev, next;
			public char value;
		}

		[TestCase(SAMPLE, 1588, 10)]
		[TestCase(SAMPLE, 2188189693529, 40)]
		[TestCase(INPUT, 2797, 10)]
		[TestCase(INPUT, 2797, 40)]
		public void Puzzle2(string input, long expected, int rounds)
		{
			var lines = input.Split(Environment.NewLine);
			var polymer = lines[0];
			var rules = new List<InsertionRule>(lines.Length - 2);
			for (var i = 2; i < lines.Length; i++)
			{
				var parts = lines[i].Split(" -> ");
				rules.Add(new InsertionRule
				{
					Find = parts[0],
					Replace = parts[1][0]
				});
			}

			var pairCounts = new Dictionary<string, long>();
			var runningSymbolCount = new Dictionary<char, long>();

			void Inc<T>(T element, Dictionary<T, long> dict, long count=1)
			{
				if (dict.TryGetValue(element, out var existing))
				{
					dict[element] = existing + count;
				}
				else
				{
					dict[element] = count;
				}
			}

			for (var i = 0; i < polymer.Length; i++)
			{
				Inc(polymer[i], runningSymbolCount);
			}
			for (var i = 1; i < polymer.Length; i++)
			{
				var pair = $"{polymer[i - 1]}{polymer[i]}";
				Inc(pair, pairCounts);
			}

			for (var i = 0; i < rounds; i++)
			{
				//var next = pairCounts.ToDictionary(x => x.Key, x => x.Value);
				var next = new Dictionary<string, long>();
				foreach (var rule in rules)
				{
					if (pairCounts.TryGetValue(rule.Find, out var matches))
					{
						// remove all of those matches, and add
						// next.Remove(rule.Find);
						var left = $"{rule.Find[0]}{rule.Replace}";
						var right = $"{rule.Replace}{rule.Find[1]}";
						Inc(left, next, matches);
						Inc(right, next, matches);

						// NC -> X
						// NNCB   : NN, NC, CB
						// NNXCB  : NN, NX, XC, CB
						/*
						 * AB - 1
						 * BC - 1
						 *
						 * AB -> B
						 * BC ->
						 */
						Inc(rule.Replace, runningSymbolCount, matches);

					}
				}
				pairCounts = next.ToDictionary(x => x.Key, x => x.Value);
			}
			//
			// var groups = pairCounts.SelectMany(kvp => new List<(char, long)>
			// {
			// 	(kvp.Key[0], kvp.Value),
			// 	// (kvp.Key[1], kvp.Value),
			// }).GroupBy(t => t.Item1).ToList();
			// var output = groups.Select(g => (g.Key, g.Sum(n => n.Item2))).ToList();
			//
			var result = runningSymbolCount.Max(k => k.Value) - runningSymbolCount.Min(k => k.Value);
			Assert.AreEqual(expected, result);
		}

		#region input

		private const string SAMPLE = @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

		private const string INPUT = @"BCHCKFFHSKPBSNVVKVSK

OV -> V
CO -> V
CS -> O
NP -> H
HH -> P
KO -> F
VO -> B
SP -> O
CB -> N
SB -> F
CF -> S
KS -> P
OH -> H
NN -> O
SF -> K
FH -> F
VV -> B
VH -> O
BV -> V
KF -> K
CC -> F
NF -> H
VS -> O
SK -> K
HV -> O
CK -> K
VP -> F
HP -> S
CN -> K
OB -> H
NS -> F
PS -> S
KB -> S
VF -> S
FP -> H
BB -> N
HF -> V
CH -> N
BH -> F
KK -> B
OO -> N
NO -> K
BP -> K
KH -> P
KN -> P
OF -> B
VC -> F
NK -> F
ON -> O
OC -> P
VK -> O
SH -> C
NH -> C
FB -> B
FC -> K
OP -> O
PV -> V
BN -> V
PC -> K
PK -> S
FF -> C
SV -> S
HK -> H
NB -> C
OK -> C
PH -> B
SO -> O
PP -> F
KV -> V
FO -> B
FN -> H
HN -> C
VB -> K
CV -> O
BC -> C
CP -> S
FS -> S
KP -> V
BS -> V
BK -> B
PN -> C
PF -> S
HO -> V
NC -> N
SS -> N
BO -> P
BF -> N
NV -> P
PB -> K
HB -> H
VN -> H
FV -> B
FK -> K
PO -> S
SC -> S
HS -> S
KC -> F
HC -> S
OS -> K
SN -> N";

		#endregion
	}
}