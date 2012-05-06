using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqPG
{
	class CachedEnumerable<T> : IEnumerable<T>
	{
		private IList<T> _cache = new List<T>();
		IEnumerator<T> _cacheEnumerator = null;
		IEnumerable<T> _source = null;

		public CachedEnumerable(IEnumerable<T> source)
		{
			_source = source;
			_cacheEnumerator = _source.GetEnumerator();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new CachedEnumerator<T>(_cache, _cacheEnumerator);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	class CachedEnumerator<T> : IEnumerator<T>
	{
		private IList<T> _cache;
		private IEnumerator<T> _source;
		private T _current;
		private int _index;
		private bool _moveNextStatus;

		public CachedEnumerator(IList<T> cache, IEnumerator<T> source)
		{
			_cache = cache;
			_source = source;
			Reset();
		}

		public T Current
		{
			get
			{
				if (_moveNextStatus)
				{
					return _current;
				}
				else
				{
					throw new InvalidOperationException();
				}
			}
		}

		public void Dispose()
		{
			_source.Dispose();
		}

		object System.Collections.IEnumerator.Current
		{
			get { return Current; }
		}

		public bool MoveNext()
		{
			_moveNextStatus = _MoveNext();
			return _moveNextStatus;
		}

		private bool _MoveNext()
		{
			_index++;

			if (_index < _cache.Count)
			{
				_current = _cache[_index];
				return true;
			}
			else
			{
				if (_source.MoveNext())
				{
					_current = _source.Current;
					_cache.Add(_current);
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public void Reset()
		{
			_index = -1;
			_current = default(T);
			_moveNextStatus = false;
		}
	}

	class StubEnum : IEnumerable<int>
	{
		public StubEnumerator LastEnumerator { get; private set; }

		public IEnumerator<int> GetEnumerator()
		{
			LastEnumerator = new StubEnumerator();
			return LastEnumerator;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	class StubEnumerator : IEnumerator<int>
	{
		public int CallCount { get; set; }

		private int _current;
		private int _max = 1000;

		public StubEnumerator()
		{
			CallCount = 0;
			Reset();
		}

		public int Current
		{
			get { return _current; }
		}

		public void Dispose()
		{
		}

		object System.Collections.IEnumerator.Current
		{
			get { return Current; }
		}

		public bool MoveNext()
		{
			CallCount++;
			_current++;

			return _current < _max;
		}

		public void Reset()
		{
			_current = -1;
		}
	}


	[TestClass]
	public class CacheEnumerableTest
	{
		[TestMethod]
		public void BasicTest()
		{
			var test = new StubEnum();
			IEnumerable<int> cachedEnumeration = new CachedEnumerable<int>(test);

			var ten = cachedEnumeration.Take(10).ToArray();
			Assert.AreEqual(10, test.LastEnumerator.CallCount);
			CollectionAssert.AreEqual(Enumerable.Range(0,10).ToArray(), ten);

			test.LastEnumerator.CallCount = 0;
			var five = cachedEnumeration.Take(5).ToArray();
			Assert.AreEqual(0, test.LastEnumerator.CallCount, "should use cache now");
			CollectionAssert.AreEqual(Enumerable.Range(0, 5).ToArray(), five);

			test.LastEnumerator.CallCount = 0;
			var hundred = cachedEnumeration.Take(100).ToArray();
			Assert.AreEqual(90, test.LastEnumerator.CallCount, "should use cache and enumerate 90");
			CollectionAssert.AreEqual(Enumerable.Range(0, 100).ToArray(), hundred);
		}
	}
}
