using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace MillionaireStrategy
{
	delegate void TheStrategy(Decimal currentMoney, int theNumber, out bool high, out decimal bid);

	class Program
	{
		static void BasicStrategy(Decimal currentMoney, int theNumber, out bool high, out decimal bid)
		{
			if (theNumber == 50)
			{
				high = false;
				bid = 0;
			}
			else if (theNumber < 50)
			{
				high = true;
				bid = (50 - theNumber) * currentMoney / 50;
			}
			else
			{
				high = false;
				bid = (theNumber - 49) * currentMoney / 50;
			}
		}

		static void ExponentialStrategy(Decimal currentMoney, int theNumber, out bool high, out decimal bid)
		{
			if (theNumber == 50)
			{
				high = false;
				bid = 0;
			}
			else if (theNumber < 50)
			{
				high = true;
				bid = (new Decimal(Math.Pow(2, (50.0 - theNumber)/50.0) - 1)) * currentMoney;
			}
			else
			{
				high = false;
				bid = (new Decimal(Math.Pow(2, (theNumber - 49.0) / 50.0) - 1)) * currentMoney;
			}
		}

		static void SqrtStrategy(Decimal currentMoney, int theNumber, out bool high, out decimal bid)
		{
			if (theNumber == 50)
			{
				high = false;
				bid = 0;
			}
			else if (theNumber < 50)
			{
				high = true;
				bid = (new Decimal(Math.Sqrt((50.0 - theNumber) / 50.0))) * currentMoney;
			}
			else
			{
				high = false;
				bid = (new Decimal(Math.Sqrt((theNumber - 49.9) / 50.0))) * currentMoney;
			}
		}

		static decimal? basicStragey2LastMoney;

		static void BasicStrategy2(Decimal currentMoney, int theNumber, out bool high, out decimal bid)
		{
			BasicStrategy(currentMoney, theNumber, out high, out bid);

			if (basicStragey2LastMoney.HasValue && !(theNumber == 0 || theNumber == 99))
			{
				if (currentMoney > basicStragey2LastMoney) // we won last time. Take more risk
				{
					bid = bid + (currentMoney - bid) / 2;
				}
				else
				{
					bid = bid - (currentMoney - bid) / 2;
				}
			}

			basicStragey2LastMoney = currentMoney;
		}

		const int size = 100;
		static Queue<int> queue = new Queue<int>(size);


		static void PastHistory(Decimal currentMoney, int theNumber, out bool high, out decimal bid)
		{
			if (queue.Count <= size)
			{
				queue.Enqueue(theNumber);
			}
			else
			{
				queue.Dequeue();
				queue.Enqueue(theNumber);
			}

			if (theNumber == 0 || theNumber == 99)
			{
				bid = currentMoney;
				high = theNumber < 50;
			}
			else if(theNumber == 50)
			{
				bid = 0;
				high = false;
			}
			else
			{
				double average = queue.Average();
				high = average < 50;
				bid = new decimal(Math.Abs(average - 50) / 50) * currentMoney;
			}
		}

		static void Main(string[] args)
		{
			//Play(BasicStrategy);
			//Play(ExponentialStrategy);
			//Test(ExponentialStrategy);
			//CompareStrategy(BasicStrategy, SqrtStrategy);

			//CompareStrategy(ExponentialStrategy, SqrtStrategy);
			// CompareStrategy(BasicStrategy, BasicStrategy2);
			CompareStrategy(BasicStrategy, PastHistory);
		}

		static void CompareStrategy(TheStrategy one, TheStrategy two)
		{
			List<long> oneList = new List<long>();
			List<long> twoList = new List<long>();
			List<long> diff = new List<long>();

			Random rand = new Random();

			while (true)
			{
				int seed = rand.Next();

				Random randOne = new Random(seed);
				Random randTwo = new Random(seed);

				Parallel.Invoke(
					() => { oneList.Add(RunStrategy(randOne, one)); },
					() => { twoList.Add(RunStrategy(randTwo, two)); });

				diff.Add(oneList.Last() - twoList.Last());
				Console.WriteLine("One = {0}, Two = {1}, Diff = {2}", oneList.Last(), twoList.Last(), diff.Last());
				Console.WriteLine("Average One = {0}, Two = {1}, Diff = {2}", oneList.Average(), twoList.Average(), diff.Average());
				Console.WriteLine("Total Call = {0}, One won = {1}, Two won = {2}, Draw = {3}",
					diff.Count,
					diff.Where(s => s < 0).Count(),
					diff.Where(s => s > 0).Count(),
					diff.Where(s => s == 0).Count()
					);

				Thread.Sleep(100);
			}
		}

		static long RunStrategy(Random rand, TheStrategy strategy)
		{
			decimal currentMoney = new decimal(10000);
			decimal millionDollars = new decimal(1000000);

			long bidCount;

			for (bidCount = 0; currentMoney > 0 && currentMoney < millionDollars; bidCount++)
			{
				int num = rand.Next(100);
				bool high;
				decimal bid;

				strategy(currentMoney, num, out high, out bid);

				if (bid > currentMoney)
				{
					continue;
				}

				int nextNum = rand.Next(100);

				if ((nextNum > num && high) || (nextNum < num && !high))
				{
					currentMoney += bid;
				}
				else if (nextNum == num)
				{
				}
				else
				{
					currentMoney -= bid;
				}
			}

			if (currentMoney > millionDollars)
			{
				return bidCount;
			}
			else
			{
				throw new Exception("Sorry you are Broke!!");
			}
		}

		static void Test(TheStrategy strategy)
		{
			bool high;
			decimal bid;

			strategy(100, 0, out high, out bid);
			Debug.Assert(high == true);
			Debug.Assert(bid == 100);

			strategy(100, 50, out high, out bid);
			Debug.Assert(bid == 0);

			strategy(100, 99, out high, out bid);
			Debug.Assert(high == false);
			Debug.Assert(bid == 100);
		}

		static void Play(Random rand, TheStrategy strategy)
		{
			decimal currentMoney = new decimal(10000);
			decimal millionDollars = new decimal(1000000);

			Console.WriteLine("Running bot for bidding");

			long bidCount;

			for (bidCount = 0; currentMoney > 0 && currentMoney < millionDollars; bidCount++)
			{
				int num = rand.Next(100);
				bool high;
				decimal bid;

				strategy(currentMoney, num, out high, out bid);

				if (bid > currentMoney)
				{
					Console.ForegroundColor = ConsoleColor.DarkRed;
					Console.WriteLine("Strategy returned Bid {0:C} > Current Money {1:C}", bid, currentMoney);
					continue;
				}

				if (bid == currentMoney)
				{
					Console.ForegroundColor = ConsoleColor.DarkRed;
					Console.WriteLine("All IN");
				}

				int nextNum = rand.Next(100);

				PrintBid(currentMoney, num, high, bid, nextNum);

				if ((nextNum > num && high) || (nextNum < num && !high))
				{
					PrintMoney("Won! ", bid, ConsoleColor.Green);
					currentMoney += bid;
				}
				else if (nextNum == num)
				{
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.WriteLine("Draw");
				}
				else
				{
					PrintMoney("Lost!", bid, ConsoleColor.Red);
					currentMoney -= bid;
				}

				System.Threading.Thread.Sleep(100);
			}

			if (currentMoney > millionDollars)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Congrats you are a millionaire!!");
				Console.WriteLine("Money = {0:C}. No of bids made = {1}", currentMoney, bidCount);
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Sorry you are Broke!!");
				Console.WriteLine("No of bids made = {0}", bidCount);
			}

			Console.ReadLine();
			Console.ResetColor();
		}

		private static void PrintBid(decimal currentMoney, int num, bool high, decimal bid, int nextNum)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			string sym = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;
			Console.WriteLine("CurrMoney {0:C} Num {1} High? {2} bid {3:C} Result {4}", currentMoney, num, high, bid, nextNum);
		}

		private static void PrintMoney(string msg, decimal bid, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			string sym = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;
			Console.WriteLine("{2} {0}{1,10:#,##0.00}", sym, bid, msg);
		}
	}
}
