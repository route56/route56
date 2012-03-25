//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace RegexProblems.DynamicProgrammingProblems
//{
//    public class FlowerGarden
//    {
//        class Data
//        {
//            public int Height { get; set; }
//            public int Start { get; set; }
//            public int End { get; set; }
//        }

//        public int[] getOrdering(int[] height, int[] bloom, int[] wilt)
//        {
//            List<Data> answer = new List<Data>();

//            for (int i = 0; i < height.Length; i++)
//            {
//                Data curr = new Data()
//                    {
//                        Height = height[i],
//                        Start = bloom[i],
//                        End = wilt[i]
//                    };

//                int position = GetInsertPostion(answer, curr);

//                answer.Insert(position, curr);
//            }

//            int[] ans = new int[answer.Count];

//            for (int i = 0; i < answer.Count; i++)
//            {
//                ans[i] = answer[i].Height;
//            }

//            return ans;
//        }

//        private int GetInsertPostion(List<Data> answer, Data data)
//        {
//            switch(answer.Count)
//            {
//                case 0:
//                    return 0;
//                case 1:
//                    if (IsOverLapping(answer[0], data))
//                    {
//                        if (answer[0].Height < data.Height)
//                        {
//                            return 1;
//                        }
//                        else
//                        {
//                            return 0;
//                        }
//                    }
//                    else
//                    {
//                        if (answer[0].Height < data.Height)
//                        {
//                            return 0;
//                        }
//                        else
//                        {
//                            return 1;
//                        }
//                    }
//            }

//            Data curr = answer[0];

//            for (int i = 1; i < answer.Count; i++)
//            {
//                if (IsOverLapping(answer[i], data))
//                {
//                    if (answer[i].Height < data.Height)
//                    {

//                    }
//                }

//                curr = answer[i];
//            }
//        }

//        private bool IsOverLapping(Data left, Data right)
//        {
//            return (left.Start <= right.Start && right.Start <= left.End)
//                || (right.Start <= left.Start && left.Start <= right.End);
//        }
//    }
//}
