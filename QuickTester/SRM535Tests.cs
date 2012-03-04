using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexProblems.DynamicProgrammingProblems;
using RegexProblems.SRMPractice;
using RegexProblems.SRM535;

namespace QuickTester
{
	[TestClass]
	public class SRM535Tests
	{
		[TestMethod]
		public void P250Tests()
		{
			CollectionAssert.AreEqual(
				new FoxAndIntegers().get(1,
-2,
3,
4
),
				new int[] { 2, 1, 3 });

			CollectionAssert.AreEqual(
				new FoxAndIntegers().get(
0,
0,
5,
5
),
				new int[] { }
);

			CollectionAssert.AreEqual(
	new FoxAndIntegers().get(
10,
-23,
-10,
3
),
	new int[] { 0, -10, 13 }
);

			CollectionAssert.AreEqual(
	new FoxAndIntegers().get(
-27,
14,
13,
22
),
	new int[] { }
);

			CollectionAssert.AreEqual(
new FoxAndIntegers().get(
30,
30,
30,
-30
),
new int[] { 30, 0, -30 }
);
			/*0)

    
			1
			-2
			3
			4
			Returns: {2, 1, 3 }
			A - B = 2 - 1 = 1 B - C = 1 - 3 = -2 A + B = 2 + 1 = 3 B + C = 1 + 3 = 4
			1)

    
			0
			0
			5
			5
			Returns: { }
			A = B = C = 2.5 satisfy all four equalities, but A, B, and C must all be integers.
			2)

    
			10
			-23
			-10
			3
			Returns: {0, -10, 13 }
			A, B, and C may be negative or zero.
			3)

    
			-27
			14
			13
			22
			Returns: { }

			4)

    
			30
			30
			30
			-30
			Returns: {30, 0, -30 }*/
		}

		[TestMethod]
		public void P500Tests()
		{
			Assert.AreEqual(new FoxAndGCDLCM().get(2,20),14);
			Assert.AreEqual(new FoxAndGCDLCM().get(5,8),-1);
			Assert.AreEqual(new FoxAndGCDLCM().get(1000,100),-1);
			Assert.AreEqual(new FoxAndGCDLCM().get(100,1000),700);
			Assert.AreEqual(new FoxAndGCDLCM().get(10,950863963000),6298430);
		}

		[TestMethod]
		public void P500TestsV2()
		{
			Assert.AreEqual(new FoxAndGCDLCM().getV2(2, 20), 14);
			Assert.AreEqual(new FoxAndGCDLCM().getV2(5, 8), -1);
			Assert.AreEqual(new FoxAndGCDLCM().getV2(1000, 100), -1);
			Assert.AreEqual(new FoxAndGCDLCM().getV2(100, 1000), 700);
			Assert.AreEqual(new FoxAndGCDLCM().getV2(10, 950863963000), 6298430);
		}
	}
}
/*0)

    
2
20
Returns: 14
The possible pairs of A and B are {2, 20} and {4, 10}. We need to pick {4, 10} since 4+10 is the smallest sum of A and B.
1)

    
5
8
Returns: -1
There are no pairs of A and B such that their greatest common divisor is 5 and their least common multiple is 8.
2)

    
1000
100
Returns: -1

3)

    
100
1000
Returns: 700

4)

    
10
950863963000
Returns: 6298430

This problem statement is the exclusive and proprietary property of TopCoder, Inc. Any unauthorized use or reproduction of this information without the prior written consent of TopCoder, Inc. is strictly prohibited. (c)2003, TopCoder, Inc. All rights reserved.*/