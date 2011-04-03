using SRM164Div2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestJustifier
{
    
    
    /// <summary>
    ///This is a test class for PartySeatsTest and is intended
    ///to contain all PartySeatsTest Unit Tests
    ///</summary>
	[TestClass()]
	public class PartySeatsTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///A test for seating
		///</summary>
		[TestMethod()]
		[DeploymentItem("SRM164Div2.dll")]
		public void seatingTest1()
		{
			PartySeats_Accessor target = new PartySeats_Accessor();
			string[] attendees = {"BOB boy","SAM girl","DAVE boy","JO girl"};
			string[] expected = { "HOST", "JO", "BOB", "HOSTESS", "DAVE", "SAM" };
			string[] actual;
			actual = target.seating(attendees);
			Assert.IsTrue(UnitTestHelpers.StringHelpers.AreEqualStringsArrays(expected, actual));
		}

		/// <summary>
		///A test for seating
		///</summary>
		[TestMethod()]
		[DeploymentItem("SRM164Div2.dll")]
		public void seatingTest2()
		{
			PartySeats_Accessor target = new PartySeats_Accessor();
			string[] attendees = { "JOHN boy" };
			string[] expected = { };
			string[] actual;
			actual = target.seating(attendees);
			Assert.IsTrue(UnitTestHelpers.StringHelpers.AreEqualStringsArrays(expected, actual));
		}

		/// <summary>
		///A test for seating
		///</summary>
		[TestMethod()]
		[DeploymentItem("SRM164Div2.dll")]
		public void seatingTest3()
		{
			PartySeats_Accessor target = new PartySeats_Accessor();
			string[] attendees = { "JOHN boy", "CARLA girl" };
			string[] expected = { };
			string[] actual;
			actual = target.seating(attendees);
			Assert.IsTrue(UnitTestHelpers.StringHelpers.AreEqualStringsArrays(expected, actual));
		}

		/// <summary>
		///A test for seating
		///</summary>
		[TestMethod()]
		[DeploymentItem("SRM164Div2.dll")]
		public void seatingTest4()
		{
			PartySeats_Accessor target = new PartySeats_Accessor();
			string[] attendees = { "BOB boy", "SUZIE girl", "DAVE boy", "JO girl", "AL boy", "BOB boy", "CARLA girl", "DEBBIE girl" };
			string[] expected = { "HOST", "CARLA", "AL", "DEBBIE", "BOB", "HOSTESS", "BOB", "JO", "DAVE", "SUZIE" };

			string[] actual;
			actual = target.seating(attendees);
			Assert.IsTrue(UnitTestHelpers.StringHelpers.AreEqualStringsArrays(expected, actual));
		}
	}
}
