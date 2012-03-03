using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexProblems.DynamicProgrammingProblems
{
	public class BadNeighbors
	{
		public int MaxDonations(int[] donations)
		{
			int size = 4;
			int[] stateBar = new int[size];
			int[] state = new int[size];

			switch (donations.Length)
			{
				case 1:
					return donations[0];
				case 2:
					return Math.Max(donations[0], donations[1]);
				case 3:
					return Math.Max(Math.Max(donations[0], donations[1]), donations[2]);
			}

			stateBar[0] = 0;
			state[0] = donations[0];

			stateBar[1] = donations[1];
			state[1] = 0;

			stateBar[2] = donations[2];
			state[2] = donations[0] + donations[2];

			for (int i = 3; i < donations.Length; i++)
			{
				stateBar[i % size] = donations[i] + Math.Max(stateBar[(i - 2) % size], stateBar[(i - 3) % size]);
				state[i % size] = donations[i] + Math.Max(state[(i - 2) % size], state[(i - 3) % size]);
			}

			return Math.Max(state[(donations.Length - 2) % size], stateBar[(donations.Length - 1) % size]);
		}
	}
}
