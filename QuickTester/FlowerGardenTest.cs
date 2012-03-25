//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using RegexProblems.DynamicProgrammingProblems;

//namespace QuickTester
//{
//    [TestClass]
//    public class FlowerGardenTest
//    {
//        [TestMethod]
//        public void KnownTests()
//        {
//            CollectionAssert.AreEqual(
//                new FlowerGarden().getOrdering(
//                    new int[] {5,4,3,2,1},
//                    new int[] {1,1,1,1,1},
//                    new int[] {365,365,365,365,365}),
//                    new int[] { 1,  2,  3,  4,  5 },
//                    "These flowers all bloom on January 1st and wilt on December 31st. Since they all may block each other, you must order them from shortest to tallest.");

//            CollectionAssert.AreEqual(
//                new FlowerGarden().getOrdering(
//                    new int[] {5,4,3,2,1},
//                    new int[] {1,5,10,15,20},
//                    new int[] {4,9,14,19,24}),
//                    new int[] { 5,  4,  3,  2,  1 },
//                    "The same set of flowers now bloom all at separate times. Since they will never block each other, you can order them from tallest to shortest to get the tallest ones as far forward as possible.");


//            CollectionAssert.AreEqual(
//                new FlowerGarden().getOrdering(
//                    new int[] {5,4,3,2,1},
//                    new int[] {1,5,10,15,20},
//                    new int[] {5,10,15,20,25}),
//                    new int[] { 1,  2,  3,  4,  5 },
//                    "Although each flower only blocks at most one other, they all must be ordered from shortest to tallest to prevent any blocking from occurring.");


//            CollectionAssert.AreEqual(
//                new FlowerGarden().getOrdering(
//                    new int[] {5,4,3,2,1},
//                    new int[] {1,5,10,15,20},
//                    new int[] {5,10,14,20,25}),
//                    new int[] { 3,  4,  5,  1,  2 },
//                    "The difference here is that the third type of flower wilts one day earlier than the blooming of the fourth flower. Therefore, we can put the flowers of height 3 first, then the flowers of height 4, then height 5, and finally the flowers of height 1 and 2. Note that we could have also ordered them with height 1 first, but this does not result in the maximum possible height being first in the garden.");

//            CollectionAssert.AreEqual(
//                new FlowerGarden().getOrdering(
//                    new int[] {1,2,3,4,5,6},
//                    new int[] {1,3,1,3,1,3},
//                    new int[] {2,4,2,4,2,4}),
//                    new int[] { 2,  4,  6,  1,  3,  5 });

//            CollectionAssert.AreEqual(
//                new FlowerGarden().getOrdering(
//                    new int[] {3,2,5,4},
//                    new int[] {1,2,11,10},
//                    new int[] {4,3,12,13}),
//                    new int[] { 4,  5,  2,  3 });
//        }
//    }
//}
