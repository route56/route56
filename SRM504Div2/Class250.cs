using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace SRM504Div2
{
	public class ComparerInator
	{
		

		public int makeProgram(int[] A, int[] B, int[] wanted)
		
		{
			int count = 0;
			for (int i = 0; i < A.Length; i++)
			{
				if (A[i] == wanted[i] && B[i] != wanted[i])
				{
					count++;
				}
			}

			if (count == A.Length)
			{
				return 1;
			}

			count = 0;
			for (int i = 0; i < B.Length; i++)
			{
				if (B[i] == wanted[i] && A[i] != wanted[i])
				{
					count++;
				}
			}

			if (count == B.Length)
			{
				return 1;
			}

			count = 0;
			for (int i = 0; i < B.Length; i++)
			{
				if (A[i] == wanted[i] && B[i] == wanted[i])
				{
					count++;
				}
			}

			if (count == A.Length)
			{
				return 1;
			}

			count = 0;
			for (int i = 0; i < A.Length; i++)
			{
				if (wanted[i] == ((A[i] < B[i])?A[i]:B[i]))
				{
					count++;
				}
			}

			if (count == A.Length)
			{
				return 7;
			}

			count = 0;
			for (int i = 0; i < A.Length; i++)
			{
				if (wanted[i] == ((A[i] < B[i]) ? B[i] : A[i]))
				{
					count++;
				}
			}

			if (count == A.Length)
			{
				return 7;
			}

			return -1;
		}
	}
}
