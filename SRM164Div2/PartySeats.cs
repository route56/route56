using System;
using System.Collections.Generic;
using System.Text;

namespace SRM164Div2
{
	public class PartySeats
	{
		string[] seating(string[] attendees)
		{
			List<string> boys = new List<string>();
			List<string> girls = new List<string>();
			string[] listtoReturnOnFail = { };
			foreach (string s in attendees)
			{
				string[] strsplit = s.Split(' ');

				if(strsplit[1].CompareTo("boy") == 0)
				{
					boys.Add(strsplit[0]);
				}
				else
				{
					girls.Add(strsplit[0]);
				}
			}

			boys.Sort();
			girls.Sort();

			int totalParticipants = attendees.Length + 2;
			int hostessLoc = totalParticipants / 2;

			// if 4 or odd count
			if (totalParticipants == 4 || totalParticipants % 2 == 1)
			{
				return listtoReturnOnFail;
			}

			List<string> result = new List<string>();
			bool failed = false;

			for (int i = 0, boyCount = 0, girlCount = 0; i < totalParticipants; i++)
			{
				if (i == 0)
				{
					result.Add("HOST");
					continue;
				}

				if (i == hostessLoc)
				{
					result.Add("HOSTESS");
					continue;
				}

				// Girls Seat
				if (i % 2 == 1)
				{
					if (girlCount < girls.Count)
					{
						result.Add(girls[girlCount]);
					}
					else
					{
						failed = true;
						break;
					}
					girlCount++;
				}
				else
				{
					if (boyCount < boys.Count)
					{
						result.Add(boys[boyCount]);
					}
					else
					{
						failed = true;
						break;
					}
					boyCount++;
				}
			}

			if (failed)
			{
				return listtoReturnOnFail;
			}
			else
			{
				return result.ToArray();
			}
		}
	}
}
