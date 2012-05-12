using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;

namespace TestClickOnce
{
	class Program
	{
		static string ByteArrayToString(byte[] arrInput)
		{
			int i;
			StringBuilder sOutput = new StringBuilder(arrInput.Length);
			for (i = 0; i < arrInput.Length; i++)
			{
				sOutput.Append(arrInput[i].ToString("X2"));
			}
			return sOutput.ToString();
		}

		static void Main(string[] args)
		{
			decimal defMoney = new decimal(10000);
			decimal startMoney;

			try
			{
				startMoney = Decimal.Parse(ConfigurationManager.AppSettings["Money"]);
				string hash = ConfigurationManager.AppSettings["Hash"];

				string tmpHash = GetMoneyHash(startMoney);

				if (hash != tmpHash)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error! Money value tampered by user. Resetting to default {0:C}", defMoney);
					throw new Exception();
				}
			}
			catch
			{
				startMoney = defMoney;
			}

			Console.WriteLine("Lets play Hi Low Game.");

			
			bool userWantsToPlay = true;
			Random rand = new Random();

			while (startMoney > 0 && userWantsToPlay)
			{
				int num = rand.Next(100);
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine("The current random number between 0-99 is {0}", num);

				try
				{
					bool high;
					decimal bid = 0;
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("Money left = {0:C}", startMoney);

					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("High(H) low[L]? Followed by bet amount");
					string input = Console.ReadLine();
					string[] split = input.Split();

					high = split[0].ToLower() == "high" || split[0].First() == 'h';
					bid = decimal.Parse(split[1]);
					if (bid > startMoney)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("bid is greater than money you have. Try again");
						continue;
					}

					int nextNum = rand.Next(100);
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.WriteLine("And the number is = {0}", nextNum);
					if (nextNum == num)
					{
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.WriteLine("Same number. Draw");
					}

					if ((nextNum > num && high) || (nextNum < num && !high))
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("You win!");
						startMoney = SetCurrentMoney(startMoney + bid);
						Console.WriteLine("Balance = {0:C}", startMoney);
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("You lose! Better luck next time");
						startMoney = SetCurrentMoney(startMoney - bid);
						Console.WriteLine("Balance = {0:C}", startMoney);
					}

					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("Continue playing? Yes, No? [Default = yes]");
					userWantsToPlay = Console.ReadLine().ToLower() != "no";
				}
				catch (Exception ex)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error occured {0}", ex.Message);
					Console.WriteLine("Try again");
				}
			}

			if (startMoney <= 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("You are broke!! Play later");
				Console.ReadLine();
			}

			Console.ResetColor();
		}

		private static decimal SetCurrentMoney(decimal newMoney)
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			KeyValueConfigurationCollection settings = config.AppSettings.Settings;

			// update SaveBeforeExit
			settings["Money"].Value = newMoney.ToString();
			settings["Hash"].Value = GetMoneyHash(newMoney);
			//save the file
			config.Save(ConfigurationSaveMode.Modified);

			//reload the section you modified
			ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);

			return newMoney;
		}

		private static string GetMoneyHash(decimal startMoney)
		{
			// http://support.microsoft.com/kb/307020
			string sSourceData;
			byte[] tmpSource;
			byte[] tmpHash;

			sSourceData = startMoney.ToString();
			//Create a byte array from source data.
			tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);

			//Compute hash based on source data.
			tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

			return ByteArrayToString(tmpHash);
		}
	}
}
