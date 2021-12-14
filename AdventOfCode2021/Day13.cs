using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2021
{
	public class Day13
	{

		[TestCase(SAMPLE, 17)]
		[TestCase(INPUT, 759)]
		public void Puzzle1(string input, int expected)
		{
			var points = new HashSet<(int, int)>();
			var folds = new List<(string, int)>();

			var lines = input.Split(Environment.NewLine);
			var isFoldInstruction = false;
			for (var i = 0; i < lines.Length; i++)
			{
				if (lines[i].Length == 0)
				{
					isFoldInstruction = true;
				} else if (!isFoldInstruction)
				{
					var parts = lines[i].Split(",");
					points.Add((int.Parse(parts[0]), int.Parse(parts[1])));
				}
				else
				{
					var parts = lines[i].Replace("fold along ", "").Split("=");
					folds.Add((parts[0], int.Parse(parts[1])));
				}

			}


			for (var i = 0; i < 1; i++)
			{
				var fold = folds[i];
				var nextPoints = new HashSet<(int, int)>();
				switch (fold.Item1)
				{
					case "y":
						foreach (var pt in points)
						{
							var relY = Math.Abs(pt.Item2 - fold.Item2);
							nextPoints.Add((pt.Item1, relY));

						}
						break;
					case "x":
						foreach (var pt in points)
						{
							var relX = Math.Abs(pt.Item1 - fold.Item2);
							nextPoints.Add((relX, pt.Item2));
						}

						break;
				}

				points = nextPoints;
			}

			Assert.AreEqual(expected, points.Count);

			// display the points
			var minX = points.Min(p => p.Item1);
			var minY = points.Min(p => p.Item2);
			var maxX = points.Max(p => p.Item1);
			var maxY = points.Max(p => p.Item2);

			for (var y = minY; y <= maxY; y++)
			{
				for (var x = maxX; x >= minX; x--)
				{
					var isDot = points.Contains((x, y));
					Console.Write(isDot ? "*" : ".");

				}
				Console.Write("\n");
			}
		}

		[TestCase(INPUT)]
		[TestCase(SAMPLE)]
		public void Puzzle2(string input)
		{
			var points = new HashSet<(int, int)>();
			var folds = new List<(string, int)>();

			var lines = input.Split(Environment.NewLine);
			var isFoldInstruction = false;
			for (var i = 0; i < lines.Length; i++)
			{
				if (lines[i].Length == 0)
				{
					isFoldInstruction = true;
				} else if (!isFoldInstruction)
				{
					var parts = lines[i].Split(",");
					points.Add((int.Parse(parts[0]), int.Parse(parts[1])));
				}
				else
				{
					var parts = lines[i].Replace("fold along ", "").Split("=");
					folds.Add((parts[0], int.Parse(parts[1])));
				}

			}


			for (var i = 0; i < folds.Count; i++)
			{
				var fold = folds[i];
				var nextPoints = new HashSet<(int, int)>();
				switch (fold.Item1)
				{
					case "y":
						foreach (var pt in points)
						{
							var relY = fold.Item2 - Math.Abs(pt.Item2 - fold.Item2);
							nextPoints.Add((pt.Item1, relY));

						}
						break;
					case "x":
						foreach (var pt in points)
						{
							/*
							 * x=1, and x=7, fold at 4.
							 * .#..|..# -> .#..
							 * 4 - 1 = 3
							 * 4 - 7 = 3
							 *
							 * ...#
							 *
							 * 4 - 3 = 1
							 */
							var relX = fold.Item2 - Math.Abs(pt.Item1 - fold.Item2);
							nextPoints.Add((relX, pt.Item2));
						}

						break;
				}

				points = nextPoints;
			}

			// display the points
			var minX = points.Min(p => p.Item1);
			var minY = points.Min(p => p.Item2);
			var maxX = points.Max(p => p.Item1);
			var maxY = points.Max(p => p.Item2);

			for (var y = minY; y <= maxY; y++)
			{
				for (var x = minX; x <= maxX; x++)
				{
					var isDot = points.Contains((x, y));
					Console.Write(isDot ? "*" : ".");

				}
				Console.Write("\n");
			}

		}

		#region input

		private const string SAMPLE = @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";

		private const string INPUT = @"688,126
1237,406
1228,327
827,189
132,457
641,120
1255,579
1310,187
371,166
576,332
1207,835
746,595
468,5
428,282
540,446
1253,393
788,701
1034,387
509,526
209,360
676,245
781,107
515,301
1235,194
83,304
177,568
159,570
141,583
599,763
519,263
261,260
18,281
870,688
1190,598
1287,169
616,351
89,488
23,725
338,204
641,792
693,577
1278,768
739,304
749,565
1096,742
504,621
900,103
627,11
1272,543
281,491
1185,129
1293,526
917,701
781,395
1005,264
843,67
930,812
967,746
1160,845
1057,313
164,92
1089,687
1267,504
113,675
676,469
1042,82
1053,777
877,334
641,270
731,855
1165,526
638,250
306,544
1034,294
1297,624
509,78
549,127
1001,647
624,499
478,509
261,232
801,526
818,810
213,339
1064,364
972,690
213,555
117,388
1146,354
837,42
667,672
223,59
50,507
1027,504
22,397
3,613
554,383
649,31
806,621
176,201
619,88
847,183
768,542
1143,140
971,110
574,747
639,120
1294,196
542,94
1011,39
89,504
875,803
271,37
729,805
253,357
492,806
622,126
748,333
693,465
780,127
1057,840
1086,826
1200,826
813,158
473,42
277,600
1022,95
1064,530
37,36
823,1
1057,651
962,714
60,816
710,794
82,810
1277,267
843,301
872,866
482,322
991,362
428,164
127,696
693,410
1139,868
832,873
32,126
872,516
21,182
875,91
1310,709
1290,201
1283,683
661,863
73,306
194,457
1094,187
144,431
1262,339
305,525
507,735
467,281
525,627
1250,78
1049,773
900,819
582,707
627,211
124,637
62,383
473,78
975,691
16,644
1245,466
1181,757
654,485
594,368
256,113
529,787
637,78
1141,626
701,670
765,347
397,329
731,263
668,721
291,179
1217,280
192,586
552,154
785,179
709,211
893,628
1273,484
89,292
571,304
437,883
1197,614
667,224
348,628
1005,600
410,551
401,406
567,417
1006,206
850,247
1004,96
674,95
393,701
1007,831
820,418
888,588
624,652
393,774
257,117
469,189
296,567
373,222
276,507
301,332
440,294
435,53
1092,772
1139,637
595,350
1042,530
435,841
75,855
492,536
93,728
927,73
497,46
673,526
445,808
557,652
401,805
818,806
412,114
1073,280
323,793
1293,647
487,1
843,145
112,94
365,765
355,487
1026,369
1193,782
694,95
463,183
820,707
57,725
1123,575
306,798
1203,288
7,868
15,465
793,480
925,277
811,196
646,546
641,326
88,42
349,294
994,268
841,705
596,252
638,644
214,742
435,526
1238,707
561,166
577,93
517,190
639,317
92,221
226,506
541,488
517,414
1019,715
909,243
37,410
601,211
232,588
617,186
37,484
1248,511
139,165
736,747
216,203
417,628
961,712
659,812
743,477
339,784
316,626
800,7
172,322
893,182
636,281
164,585
547,674
542,876
1197,70
525,267
15,36
1240,177
1005,425
1006,21
1245,121
933,600
1054,893
600,100
949,86
373,403
1057,56
1054,1
694,799
550,82
1057,243
89,824
435,501
1021,577
1185,107
483,332
955,683
1091,837
1042,364
1071,642
1273,260
167,140
619,536
113,728
857,877
437,235
129,137
1087,96
952,253
659,109
910,595
176,693
952,499
721,502
164,354
529,339
417,182
781,787
170,826
857,241
775,372
453,843
962,404
316,268
744,404
582,35
1160,84
765,498
589,196
299,519
72,259
164,866
761,127
125,765
1134,693
795,593
1015,488
216,187
299,855
52,812
1002,431
721,193
914,252
304,273
910,707
585,728
510,711
917,466
438,68
43,484
1041,603
397,614
542,352
400,747
537,11
1261,518
313,624
495,371
1116,247
21,369
803,159
566,852
303,383
748,781
656,37
254,299
176,497
1303,691
0,485
1241,504
1163,735
1064,147
182,439
490,707
214,94
1129,42
293,715
107,9
542,542
284,369
1235,39
991,469
1200,467
632,397
1014,701
1130,306
1015,406
75,197
927,427
1096,800
1241,838
1066,810
765,435
700,686
401,651
1300,647
815,526
838,767
23,189
708,203
1034,632
150,761
460,247
1029,491
1193,800
633,712
1134,201
870,94
467,491
401,54
1056,819
470,798
1134,497
837,852
602,203
1190,296
346,57
1203,53
1143,754
110,68
890,389
13,270
781,339
991,82
1017,585
813,44
269,277
393,466
383,427
975,469
1007,383
955,375
467,301
1148,806
490,418
1091,78
505,120
1287,705
331,225
848,196
346,407
641,774
356,798
27,211
785,627
343,792
599,660
1109,577
664,546
125,129
102,238
519,631
504,49
344,420
164,802
676,649
372,191
525,403
549,739
1203,368
1257,659
589,253
813,119
271,256
842,5
229,204
579,476
264,831
987,549
547,501
22,497
785,851
1001,32
574,299
1053,329
760,82
885,569
728,299
171,189
892,329
939,614
566,42
385,277
1039,256
355,519
1091,501
7,245
879,103
932,632
242,114
639,568
562,673
763,448
1019,267
537,500
1033,658
141,311
1273,36
8,546
1163,36
1193,836
124,767
381,683
431,103
1186,637
823,337
647,355
1303,245
167,1
291,582
1227,304
636,799
1201,396
490,810
828,322
254,819
1037,737
633,861
467,831
129,267
721,701
1057,89
565,390
89,614
738,567
858,575
68,866
1125,637
1143,893
1151,324
1197,675
729,502
418,565
617,484
75,194
1129,852
900,119
1014,567
436,122
1171,749
1156,147
1019,806
55,277
299,459
1223,252
714,28
415,869
820,540
1302,348
868,331
514,649
843,491
579,253
6,33
843,281
80,694
487,754
1062,108
964,57
1197,824
261,120
1026,525
763,9
223,551
420,389
1049,54
987,793
396,642
704,555
1125,201
843,831
1014,193
433,207
1056,187
244,810
873,11
1054,45
651,109
1169,481
1109,17
1208,154
393,120
219,816
315,560
1283,211
67,351
875,78
1183,885
952,74
581,805
726,290
410,819
460,701
1059,855
641,102
959,189
765,95
1146,707
365,129
1093,771
107,672
1066,103
909,54
214,163
1171,613
223,835
634,245
714,476
693,484
912,45
328,411
13,624
982,483
310,868
875,841
618,163
646,348
70,221
311,466
818,536
187,319
1201,347
711,763
453,877
45,758
1154,68
763,226
1096,549
507,484
94,140
403,476
1160,581
574,694
365,186
289,129
770,448
914,642
1166,431
1198,18
338,367
714,866
813,718
748,221
676,21
256,45
601,235
425,164
840,798
1134,385
1303,26
472,127
639,774
716,526
930,82
795,145
219,91
1198,94
726,738
800,711
711,234
32,768
540,448
214,184
253,854
311,428
495,1
1084,388
674,799
237,189
43,410
641,494
1243,543
201,115
1181,627
611,619
256,449
1096,688
976,481
331,101
729,306
545,498
694,351
811,114
561,565
65,232
686,690
445,861
967,550
164,84
89,602
182,767
1076,617
415,421
113,614
691,358
1059,476
639,858
372,703
619,806
269,603
902,791
676,425
979,793
309,144
562,561
721,698
1266,367
1203,526
305,294
164,757
1295,36
1178,457
1170,649
472,15
939,36
959,511
358,820
718,159
1039,579
22,49
541,518
172,217
239,252
1034,507
971,784
1056,299
836,588
21,871
872,28
1033,600
17,526
530,189
60,78
273,737
1307,505
1297,102
1019,582
1310,185
766,427
1006,621
577,281
1267,410
979,225
572,567
826,826
542,793
403,418
253,812
268,812
1223,865
909,406
945,129
875,526
714,812
140,245
271,315
572,119
677,871
987,221
656,857
482,665
387,656
1275,88
109,347
700,238
125,555
157,176
875,53
387,238
483,562
1297,400
117,800
361,86
181,368
73,406
632,206
393,568
180,451
485,268
437,323
57,110
1265,136
530,637
311,596
811,780
221,560
554,282
909,530
1171,281
438,28
261,840
677,182
150,810
909,651
1038,772
16,219
284,525
1007,63
634,649
319,362
565,292
440,184
1005,525
1064,747
335,691
828,229
659,785
398,45
952,372
917,120
1139,705
440,94
82,327
144,183
584,290
1230,136
589,641
739,590
246,147
828,665
1294,250
43,843
276,387
1267,51
1273,410
781,555
677,861
870,294
253,840
693,17
331,345
768,262
70,177
67,543
97,690
16,250
525,851
252,180
1002,463
13,326
277,294
103,768
589,876
1288,245
827,637
253,581
525,715
725,504
467,145
1256,346
164,309
917,326
244,119
900,551
647,474
537,782
1094,203
22,245
937,403
219,803
1146,309
841,201
144,887
1006,721
1123,319
11,224
425,690
962,628
170,696

fold along x=655
fold along y=447
fold along x=327
fold along y=223
fold along x=163
fold along y=111
fold along x=81
fold along y=55
fold along x=40
fold along y=27
fold along y=13
fold along y=6";

		#endregion
	}
}