using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using ProblemHelper;

namespace QR2011
{
	public class BotTrust : IGCJSolution
	{
		public class SequenceElement
		{
			public int Button { get; set; }
			public int SeqNum { get; set; }
		}

		// Verify boundary cases and add UTs
		// Refactor
		public int RunAlgo(string seq)
		{
			// State based simulation run
			Queue<SequenceElement> blueSeq = null;
			Queue<SequenceElement> orgSeq = null;

			GetTwoQueuesFrom(seq, out blueSeq, out orgSeq);

			int timePassed = 0;
			int whereIsBlue = 1;
			int whereIsOrg = 1;

			SequenceElement blue = GetNext(blueSeq);
			SequenceElement org = GetNext(orgSeq);

			while (blue != null && org != null)
			{
				if (blue.SeqNum < org.SeqNum)
				{
					Process(blueSeq, ref timePassed, ref whereIsBlue, ref whereIsOrg, ref blue, org);
				}
				else
				{
					Process(orgSeq, ref timePassed, ref whereIsOrg, ref whereIsBlue, ref org, blue);
				}
			}

			while (blue != null)
			{
				Process(blueSeq, ref timePassed, ref whereIsBlue, ref whereIsOrg, ref blue, org);
			}

			while (org != null)
			{
				Process(orgSeq, ref timePassed, ref whereIsOrg, ref whereIsBlue, ref org, blue);
			}

			return timePassed;
		}

		private void Process(Queue<SequenceElement> blueSeq, ref int timePassed, ref int whereIsBlue, ref int whereIsOrg, ref SequenceElement blue, SequenceElement org)
		{
			if (whereIsBlue == blue.Button)
			{
				timePassed++; // pressed.
				blue = GetNext(blueSeq);
				whereIsOrg = MoveOrgIfNeeded(whereIsOrg, org, 1);
			}
			else
			{
				int delta = 0;

				whereIsBlue = MoveBlueToSeqNum(blue, whereIsBlue, out delta);

				timePassed += delta;
				whereIsOrg = MoveOrgIfNeeded(whereIsOrg, org, delta);
			}
		}

		private int MoveBlueToSeqNum(SequenceElement blue, int whereIsBlue, out int delta)
		{
			delta = Math.Abs(blue.Button - whereIsBlue);

			return blue.Button;
		}

		private void GetTwoQueuesFrom(string seq, out Queue<SequenceElement> blueSeq, out Queue<SequenceElement> orgSeq)
		{
			//"4 B 1 B 10 O 10 O 1"
			blueSeq = new Queue<SequenceElement>();
			orgSeq = new Queue<SequenceElement>();

			string[] split = seq.Split();

			for (int i = 0; i < Int32.Parse(split[0]); i++)
			{
				switch (split[2*i + 1])
				{
					case "B":
						blueSeq.Enqueue(new SequenceElement() { SeqNum = i, Button = Int32.Parse(split[2*i + 2]) });
						break;

					case "O":
						orgSeq.Enqueue(new SequenceElement() { SeqNum = i, Button = Int32.Parse(split[2*i + 2]) });
						break;

					default:
						Debug.Assert(false);
						break;
				}
			}
		}

		private static SequenceElement GetNext(Queue<SequenceElement> queue)
		{
			return (queue.Count > 0) ? queue.Dequeue() : null;
		}

		private static int MoveOrgIfNeeded(int whereIsOrg, SequenceElement org, int delta)
		{
			if (org == null)
			{
				return 1;
			}

			if (whereIsOrg != org.Button)
			{
				if (whereIsOrg < org.Button)
				{
					whereIsOrg += delta;
					if (whereIsOrg > org.Button)
					{
						whereIsOrg = org.Button;
					}
				}
				else
				{
					whereIsOrg -= delta;
					if (whereIsOrg < org.Button)
					{
						whereIsOrg = org.Button;
					}
				}
			}

			return whereIsOrg;
		}

		public void Solve(string InputFile, string ActualOutputFile)
		{
			using (System.IO.StreamReader rd = new System.IO.StreamReader(InputFile))
			using (System.IO.StreamWriter wr = new System.IO.StreamWriter(ActualOutputFile))
			{
				int dataItems = Int32.Parse(rd.ReadLine());

				for (int i = 0; i < dataItems; i++)
				{
					int result = this.RunAlgo(rd.ReadLine());
					wr.WriteLine("Case #{0}: {1}", i+1, result);
				}

				rd.Close();
				wr.Close();
			}
		}
	}
}
