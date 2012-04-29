using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqPG
{
	class AbsIntComparer : IEqualityComparer<int>
	{
		public bool Equals(int x, int y)
		{
			return Math.Abs(x).Equals(Math.Abs(y));
		}

		public int GetHashCode(int obj)
		{
			return Math.Abs(obj).GetHashCode();
		}
	}

	[TestClass]
	public class ArrayLinqTest
	{
		static readonly int[] dataSource = new int[] { 2, 3, 5 };

		

		[TestMethod]
		public void Aggregate_Tests()
		{
			int[] arr = new int[] { 2, 3, 5 };

			Assert.AreEqual(2 * 3 * 5, arr.Aggregate((a, b) => a * b));
			Assert.AreEqual(2, arr.Aggregate((a, b) => a));
			Assert.AreEqual(5, arr.Aggregate((a, b) => b));

			Assert.AreEqual(10 * 2 * 3 * 5, arr.Aggregate(10, (a, b) => a * b));
			Assert.AreEqual(10, arr.Aggregate(10, (a, b) => a));
			Assert.AreEqual(5, arr.Aggregate(10, (a, b) => b));

			Assert.AreEqual(10 * 2 * 3 * 5 * 7, arr.Aggregate(10, (a, b) => a * b, c => 7 * c));
		}

		[TestMethod]
		public void All_Test()
		{
			int[] arr = new int[] { 2, 3, 5 };

			int prod = 2*3*5;

			Assert.IsTrue(arr.All(a => prod % a == 0));

			prod = 2 * 3;
			Assert.IsFalse(arr.All(a => prod % a == 0));

			prod = 2;
			Assert.IsFalse(arr.All(a => prod % a == 0));

			prod = 7;
			Assert.IsFalse(arr.All(a => prod % a == 0));
		}

		[TestMethod]
		public void Any_Test()
		{
			int[] arr = new int[] { 2, 3, 5 };
			int[] empty = new int[] { }; 

			int prod = 2 * 3 * 5;

			Assert.IsTrue(arr.Any());
			Assert.IsFalse(empty.Any());

			Assert.IsTrue(arr.Any(a => prod % a == 0));
			prod = 2;
			Assert.IsTrue(arr.Any(a => prod % a == 0));
			prod = 7;
			Assert.IsFalse(arr.Any(a => prod % a == 0));
		}

		// Custom class.
		class Clump<T> : List<T>
		{
			public bool ClumpWhereCalled = false;

			// Custom implementation of Where().
			public IEnumerable<T> Where(Func<T, bool> predicate)
			{
				ClumpWhereCalled = true;
				return Enumerable.Where(this, predicate);
			}
		}

		[TestMethod]
		public void AsEnumerable()
		{
			// Create a new Clump<T> object.
			Clump<string> fruitClump = new Clump<string> 
				{ "apple", "passionfruit", "banana", "mango", "orange", 
					"blueberry", "grape", "strawberry" };

			// First call to Where():
			// Call Clump's Where() method with a predicate.
			IEnumerable<string> query1 =
				fruitClump.Where(fruit => fruit.Contains("o"));

			Assert.IsTrue(fruitClump.ClumpWhereCalled);

			fruitClump.ClumpWhereCalled = false;
			// Second call to Where():
			// First call AsEnumerable() to hide Clump's Where() method and thereby
			// force System.Linq.Enumerable's Where() method to be called.
			IEnumerable<string> query2 =
				fruitClump.AsEnumerable().Where(fruit => fruit.Contains("o"));

			Assert.IsFalse(fruitClump.ClumpWhereCalled);
		}

		[TestMethod]
		public void Average()
		{
			int[] arr = new int[] { 2, 4, 6 };

			Assert.AreEqual(4, arr.Average());

			arr = new int[] { 2, 3 };
			Assert.AreEqual(2.5, arr.Average());

			Assert.AreEqual(5, arr.Average(a => 2 * a));
		}

		[TestMethod]
		public void Cast()
		{
			int[] arr = new int[] { 2, 3, 5 };
			long[] expected = new long[] { 2, 3, 5 };

			IEnumerable<long> cast = arr.Cast<long>(); // TODO find out why this fails?

			CollectionAssert.AreEqual(expected, cast.ToArray());
		}

		[TestMethod]
		public void Concat()
		{
			int[] arr = new int[] { 2, 3, 5 };

			var ans = arr.Concat(new int[] { 7, 11 });

			CollectionAssert.AreEqual(new int[] { 2, 3, 5, 7, 11 }, ans.ToArray());
		}

		[TestMethod]
		public void Contains()
		{
			int[] arr = new int[] { 2, 3, 5 };

			Assert.IsTrue(arr.Contains(2));
			Assert.IsFalse(arr.Contains(7));

			// TODO find any func<> overrride
		}

		[TestMethod]
		public void Count()
		{
			int[] arr = new int[] { 2, 3, 5 };

			Assert.AreEqual(3, arr.Count());

			arr = new int[] { 2, 3, 2 };
			Assert.AreEqual(2, arr.Count(a => a == 2));
			Assert.AreEqual(0, arr.Count(a => a == 5));
			Assert.AreEqual(1, arr.Count(a => a == 3));
		}

		[TestMethod]
		public void DefaultIfEmpty()
		{
			int[] arr = new int[] { 2, 3, 5 };

			CollectionAssert.AreEqual(arr, arr.DefaultIfEmpty().ToArray());

			int[] empty = new int[] { };

			CollectionAssert.AreEqual(new int[] { 0 }, empty.DefaultIfEmpty().ToArray());
			CollectionAssert.AreEqual(new int[] { 1 }, empty.DefaultIfEmpty<int>(1).ToArray());
		}

		[TestMethod]
		public void Distinct()
		{
			int[] arr = new int[] { -2, 2, 2, -3, 3,  5, -5, 5 };

			CollectionAssert.AreEqual(new int[] { -2, 2, -3, 3, 5, -5 }, arr.Distinct().ToArray());

			CollectionAssert.AreEqual(new int[] { -2, -3, 5 }, arr.Distinct(new AbsIntComparer()).ToArray());
		}
	}
}
