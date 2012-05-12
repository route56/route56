using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillionaireStrategy
{
	delegate void TheStrategy(Decimal currentMoney, int theNumber, out bool high, out decimal bid);

	class Program
	{
		static void BasicStrategy(Decimal currentMoney, int theNumber, out bool high, out decimal bid)
		{
			if (theNumber > 50)
			{
				high = false;
				bid = (100 - theNumber) * currentMoney / 49;
			}
			else if(theNumber < 50)
			{
				high = true;
				bid = (50 - theNumber) * currentMoney / 49;
			}
			else
			{
				high = false;
				bid = 0;
			}
		}

		static void Main(string[] args)
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

				int nextNum = rand.Next(100);

				PrintBid(currentMoney, num, high, bid, nextNum);

				if ((nextNum > num && high) || (nextNum < num && !high))
				{
					PrintMoney("Won! ", bid, ConsoleColor.Green);
					currentMoney += bid;
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
