using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testground
{
	class LRUCache<TKey, TValue>
	{
		class ValueTickPair<TValue>
		{
			public TValue Value { get; set; }
			public long Ticks { get; set; }
		}

		public LRUCache(IDictionary<TKey, TValue> source)
			: this(source, 1024)
		{
		}

		public LRUCache(IDictionary<TKey, TValue> source, int cacheSize)
		{
			if (cacheSize <= 0)
			{
				throw new ArgumentException("cacheSize");
			}

			if (source == null)
			{
				throw new ArgumentException("source");
			}

			_source = source;
			_cacheSize = cacheSize;
			_cache = new Dictionary<TKey, ValueTickPair<TValue>>(_cacheSize);
		}

		public TValue GetValue(TKey key)
		{
			if (_cache.ContainsKey(key))
			{
				var val = _cache[key];
				val.Ticks = ticks++;
				return val.Value;
			}

			var result = _source[key]; // cache hit

			if (_cache.Count >= _cacheSize)
			{
				long min = long.MaxValue;
				TKey fire = default(TKey);

				foreach (var item in _cache) // TODO This should be improved.
				{
					if (min > item.Value.Ticks)
					{
						min = item.Value.Ticks;
						fire = item.Key;
					}
				}

				_cache.Remove(fire);
			}

			_cache.Add(key, new ValueTickPair<TValue>() { Value = result, Ticks = ticks++ });

			return result;
		}


		public int CachedItemCount { get { return _cache.Count;} }

		public bool IsItemInCache(TKey key)
		{
			return _cache.ContainsKey(key);
		}

		public void ClearCache()
		{
			_cache.Clear();
		}

		private IDictionary<TKey, TValue> _source;
		private int _cacheSize;
		private IDictionary<TKey, ValueTickPair<TValue>> _cache;
		private long ticks = 0;
	}

	[TestClass]
	public class LRUCacheTest
	{
		[TestMethod]
		public void ApiDesign()
		{
			// Type specific
			IDictionary<int, string> source = new Dictionary<int, string>();

			for (int i = 0; i < 100; i++)
			{
				source.Add(i, i.ToString());
			}

			var target = new LRUCache<int, string>(source, 10);

			for (int i = 0; i < 50; i++)
			{
				Assert.AreEqual(i.ToString(), target.GetValue(i));
			}

			Assert.AreEqual(10, target.CachedItemCount);

			for (int i = 40; i < 50; i++)
			{
				Assert.IsTrue(target.IsItemInCache(i));
			}

			// use items present in cache
			for (int i = 42; i < 47; i++) // 5 items
			{
				Assert.AreEqual(i.ToString(), target.GetValue(i));
			}

			for (int i = 40; i < 50; i++)
			{
				Assert.IsTrue(target.IsItemInCache(i));
			}

			// use new items.
			for (int i = 60; i < 63; i++) // 3 items
			{
				Assert.AreEqual(i.ToString(), target.GetValue(i));
			}

			// Assert items in cache
			for (int i = 40; i < 42; i++)
			{
				Assert.IsFalse(target.IsItemInCache(i));
			}

			for (int i = 42; i < 47; i++)
			{
				Assert.IsTrue(target.IsItemInCache(i));
			}

			for (int i = 47; i < 48; i++)
			{
				Assert.IsFalse(target.IsItemInCache(i));
			}

			for (int i = 48; i < 50; i++)
			{
				Assert.IsTrue(target.IsItemInCache(i));
			}

			for (int i = 60; i < 63; i++)
			{
				Assert.IsTrue(target.IsItemInCache(i));
			}

			Assert.AreEqual(10, target.CachedItemCount);

			target.ClearCache();

			Assert.AreEqual(0, target.CachedItemCount);
		}
	}
}
