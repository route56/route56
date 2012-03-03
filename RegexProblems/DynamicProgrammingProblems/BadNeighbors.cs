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
			int[] stateBar = new int[donations.Length];
			int[] state = new int[donations.Length];

			if (donations.Length <= 3)
			{
				switch(donations.Length)
				{
					case 1:
						return donations[0];
					case 2:
						return Math.Max(donations[0], donations[1]);
					case 3:
						return Math.Max(Math.Max(donations[0], donations[1]), donations[2]);
				}
			}

			stateBar[0] = 0;
			state[0] = donations[0];

			stateBar[1] = donations[1];
			state[1] = 0;

			stateBar[2] = donations[2];
			state[2] = donations[0] + donations[2];

			for (int i = 3; i < donations.Length; i++)
			{
				stateBar[i] = donations[i] + Math.Max(stateBar[i - 2], stateBar[i - 3]);
				state[i] = donations[i] + Math.Max(state[i - 2], state[i - 3]);
			}

			return Math.Max(state[state.Length - 2], stateBar[stateBar.Length - 1]);
		}

		//{
		//    int[] states = new int[donations.Length];

		//    if (donations.Length <= 3)
		//    {
		//        throw new NotImplementedException();
		//    }

		//    states[0] = donations[0];
		//    states[1] = donations[1];
		//    states[2] = donations[2];

		//    for (int i = 3; i < donations.Length; i++)
		//    {
		//        states[i] = donations[i];
		//        states[i - 1] += states[0];

		//        for (int j = 1; j < i - 1; j++)
		//        {
		//            if (states[j] + donations[i] > states[i])
		//            {
		//                states[i] = states[j] + donations[i];
		//            }
		//        }
		//    }

		//    int maxDon = 0;

		//    for (int i = 0; i < states.Length; i++)
		//    {
		//        if (maxDon < states[i])
		//        {
		//            maxDon = states[i];
		//        }
		//    }

		//    return maxDon;
		//}
	}
}
