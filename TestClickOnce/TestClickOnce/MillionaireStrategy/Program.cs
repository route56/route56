using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

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

		static void Main(string[] args)
		{
			Play();
		}

		static void Test()
		{
			bool high;
			decimal bid;

			BasicStrategy(100, 0, out high, out bid);
			Debug.Assert(high == true);
			Debug.Assert(bid == 100);

			BasicStrategy(100, 50, out high, out bid);
			Debug.Assert(bid == 0);

			BasicStrategy(100, 99, out high, out bid);
			Debug.Assert(high == false);
			Debug.Assert(bid == 100);
		}

		static void Play()
		{
			decimal currentMoney = new decimal(10000);
			decimal millionDollars = new decimal(1000000);

			TheStrategy strategy = BasicStrategy;

			Console.WriteLine("Running bot for bidding");

			long bidCount;
			Random rand = new Random();

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
