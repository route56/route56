﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexProblems
{
	public class CheatCodes
	{
		public int[] Matches(string keyPresses, string[] codes)
		{
			bool []map = new bool[codes.Length] ; 
			int count = 0 ;
			for (int i = 0; i < codes.Length; ++i)
			{
				StringBuilder regex =
					//GetRegExForCodeWithJustPlus(codes[i].ToCharArray());
					//GetRegExForCode(codes[i].ToCharArray());
					//GetRegExForCodeWithoutStartEndFillers(codes[i].ToCharArray());
					GetRegExForCodeWithJustPlusQuestion(codes[i].ToCharArray());


				if (Regex.Matches(keyPresses, regex.ToString()).Count > 0)
				{
					map[i] = true;
					count++;
				}
			}

			int []res = new int[count] ;
			int jj=0;
			for (int i= 0 ; i < codes.Length; ++i)
			if(map[i] == true)
				res[jj++]=i ; 
			return res ; 
		}

		/// <summary>
		/// +? Lazy quantifer and not greedy
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		private StringBuilder GetRegExForCodeWithJustPlusQuestion(char[] code)
		{
			StringBuilder regex = new StringBuilder();

			foreach (var item in code)
			{
				regex.Append(item);
				regex.Append("+?");
			}

			return regex;
		}

		private StringBuilder GetRegExForCode(char[] code)
		{
			StringBuilder regex = new StringBuilder(".*"); // WHy is this needed?

			for (int j = 0; j < code.Length; )
			{
				int k = 1;
				while ((j + k) < code.Length && code[j + k] == code[j])
					k++;
				regex.AppendFormat("{0}{1}{2},{3}", code[j], "{", k, "}"); // WHy is this needed? Why not A+A+A+ instead of A{3, } or AAA+ ?
				j += k;
			}

			regex.Append(".*"); // WHy is this needed?
			return regex;
		}

		/// <summary>
		/// This is equally fine.
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		private StringBuilder GetRegExForCodeWithoutStartEndFillers(char[] code)
		{
			StringBuilder regex = new StringBuilder();

			for (int j = 0; j < code.Length; )
			{
				int k = 1;
				while ((j + k) < code.Length && code[j + k] == code[j])
					k++;
				regex.AppendFormat("{0}{1}{2},{3}", code[j], "{", k, "}"); // WHy is this needed? Why not A+A+A+ instead of A{3, } or AAA+ ?
				j += k;
			}

			return regex;
		}

		/// <summary>
		/// This turns out to hang for AAAAAAAAAAAAAA match for same.
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		private StringBuilder GetRegExForCodeWithJustPlus(char[] code)
		{
			StringBuilder regex = new StringBuilder();

			foreach (var item in code)
			{
				regex.Append(item);
				regex.Append("+");
			}

			return regex;
		}
	}
}

/*
Problem Statement
    	Checking to to see if a player enters a valid cheat code in a game is not just a simple matter of checking that the keypresses exactly line up with the cheat code. It is possible that the player may hold down one key a little too long and consequently it is read as two or more key presses. Ignoring repeated key presses isn't an option either however, because a cheat code may require a key to be used repeatedly. Also, the player may press any number of keys before or after entering the cheat code. 



You will be given a String keyPresses which will contain the keys the player pressed. Each character in keyPresses will correspond to one key pressed by the player. The order of characters in keyPresses is the order in which the keys were entered. You will also be given a String[] codes. Each String in codes will be one possible valid cheat code. You are to create a class CheatCode with a method matches which will return a sorted int[] containing the indexes of all codes in codes which potentially match what the player entered. A cheat code matches keyPresses if some substring of keyPresses is the same as the cheat code, except that it may have more than one of the same character, where only one was expected. So, if keyPresses where "ABBCC", it would match the cheat codes "ABC", "BB" and "BC" (and others), but not "ABBB" or "BBCCD". 



For example, if keyPresses is "UUDDLRRLLRBASS" (quotes for clarity) and codes is
{ "UUDDLRLRBA",
  "UUDUDLRLRABABSS",
  "DDUURLRLAB",
  "UUDDLRLRBASS",
  "UDLRRLLRBASS" }
The code "UUDDLRLRBA" matches because the player may enter extra keys after the code has been entered. Also notice that although there is an extra 'R' and 'L' in the middle of keyPresses, the code still matches because the computer may have accidentally detected repeated keys while the player entering a code.

The code "UUDUDLRLRABABSS" stops matching at the 4th character, 'U'. The 2nd 'D' in keyPresses is allowed because the player may have held it down too long. However, the 'U' does not match the 'L', which is next in keyPresses.

The code "DDUURLRLAB" does not match. Remember though that the beginning of keyPresses, "UU", is ignored here because the player may have done another move before entering the code. The "DD" at the beginning of the code does match with the first "DD" in keyPresses, however next in keyPresses is an 'L' which does not match the 'U' in the code.

The code "UUDDLRLRBASS" matches.

The code "UDLRRLLRBASS" also matches.



Your program would return
{ 0,  3,  4 }
for the 0th, 3rd, and 4th codes in codes which match keyPresses.
 
Definition
    	
Class:	CheatCode
Method:	matches
Parameters:	String, String[]
Returns:	int[]
Method signature:	int[] matches(String keyPresses, String[] codes)
(be sure your method is public)
    
 
Constraints
-	keyPresses will be between 0 and 50 characters in length, inclusive.
-	keyPresses will only contain uppercase letters ('A'-'Z').
-	codes will contain between 0 and 50 elements, inclusive.
-	Each String in codes will be between 1 and 50 characters in length, inclusive.
-	Each String in codes will only contain uppercase letters ('A'-'Z').
 
Examples
0)	
    	
"UUDDLRRLLRBASS"
{"UUDDLRLRBA","UUDUDLRLRABABSS","DDUURLRLAB","UUDDLRLRBASS","UDLRRLLRBASS"}
Returns: { 0,  3,  4 }
This is the example from above.
1)	
    	
"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
{"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"}
Returns: { 0 }
Watch your time!
2)	
    	
"IDDQDDTSFHHALL"
{"FHHALL", "FHSHH", "IDBEHOLDA", "IDBEHOLDI", "IDBEHOLDL",
 "IDBEHOLDR", "IDBEHOLDS", "IDBEHOLDV", "IDCHOPPERS", "IDCLEV",
 "IDCLIP", "IDDQD", "IDDT", "IDFA", "IDKFA", "IDMYPOS", "IDMUS"}
Returns: { 0,  11 }
3)	
    	
"AABBCCDDEEFFGGHHIIJJKKLLMMNNOOPPQQRRSSTTUUVVWWXXYY"
{"ABCDE", "BCDEF", "CDEFG", "DEFGH", "EFGHI",
 "FGHIJ", "GHIJK", "HIJKL", "IJKLM", "JKLMN",
 "KLMNO", "LMNOP", "MNOPQ", "NOPQR", "OPQRS",
 "PQRST", "QRSTU", "RSTUV", "STUVW", "TUVWX",
 "UVWXY", "VWXYZ", "WXYZA", "XYZAB", "YZABC",
 "ZABCD"}
Returns: 
{ 0,  1,  2,  3,  4,  5,  6,  7,  8,  9,  10,  11,  12,  13,  14,  15,  16,  17,  18,  19,  20 }
4)	
    	
"LAKJDGSJKGLSDKHFKDFHDGHHSDKKSJDHFHJGKDKLSLSLJKAHSJ"
{"LAKJDGSJKGLSDKHFKDFHDGHHSDKKSJDHFHJGKDKLSLSLJKAHSS", "LAKJDGSJKGLSDKHFKDFHDGHSDKKSJDHFHJGKDKLSLSLJKAHSJ",
 "LAKJDGSJKGLSDKHFKDFHDHHSDKKSJDHFHJGKDKLSLSLJKASSJ",  "AKJDGSJKGLSDKHFKDFHDGHHSDKKSJDHFHJGKDKLSLSLJKAHSJ",
 "LAJDGSJKGLSDKHFKDFHDGHHSDKKSJDHFHJGKDKLSLSLJKHHSJ",  "LAKDGSJKGLSDKHFKDFHDGHHSDKKSJDHFHJGKDKLSLSLJKAHSJ",
 "LAKJDGSJKGLSDKHFKDFHDGHHSDKKSJDFHJGKDKLSLSLJKAHS",   "KJDGSJKGLSDKHFKDFHDGHHSDKKSJDHFHJGKDKLSLSLJKAHSJ",
 "LAKJDGSJKGLSDKHFKDFHDHHSDKKSJDHFHJGKDKLLSLJKAHS",    "LAKGSJKGLSDKHFKDFHDGHHSDKKSJDHFHJGKDKLSLSLJKAHSJ",
 "LAKJDGJKGLSDKHFKDFHDGHHDKKSJDHFHJGKDKLSLSLJKAHS",    "LAKJDGJKGLSDKHFKDFHDGHHSDKKSJDHFHJGKDKLSLSLJKAHSJ",
 "LAKJDGSJKGLSDKHFKDFHGHHSDKKSJDHFHJGKDKLSLSLJKAHS",   "LAKJDGSJKGLSDKHFKDFHDGHHSDKKSJDHFHJGKDKLSLSL",
 "LAKJDGSJKGLSDKHFDFHDGHHSDKKSJDHFHJGKDKLSLSLJKAHS",   "LAKJDGSJKGLSDKHFKDFHDGHHSDKKSJDHFHJGKDKLSLJKAHSJ",
 "LAKJDGSJKGLSDHFKDFHDGHHSDKKSJDHFHJGKDKLSLSJKAHS",    "KGLSDKHFKDFHDGHHSDKKSJDHFHJGKDKLSLSL",
 "LAKJDGSJKGLSDKHFKDFHDGHHSDKKSJHFHJGKDKLSLSLJKAHS",   "LAKJDGSJKGLSDKHFKDFHDGHHSDKKSJDHFHJGKDSLSLJKAHSJ",
 "LAKJDGSJKGLSDKHFKDFHDGHHSDKSJHFHJGKDKLSLSLJKAHS",    "LAKJDGSJKGLSDKHFKDFHDGHHSDKKSJDHFHJGKKLSLSLJKAHSJ",
 "LAKJDGSJKGLSDKHFKFHDGHHSDKKSJDHFHJGKDKLSLSLJKAHS",   "LAKJDGSJKGLSDKHFKDFHDGHHSDKKSJDHFHJGDKLSLSLJKAHSJ",
 "LAKJDGSJKGLSDKHFKDFHDGHHSDKSJHFHJGKDKLSLSLJKAHS",    "LAKJDGSJKGLSDKHFKDFHDGHHSDKKSJDHFHJKDKLSLSLJKAHSJ",
 "LKJDGSJKGLSDKHFKDFHDGHHSDKKJDHFHJGKDKLSLSLJKAHS",    "AKJDGSJKGLSDKHFKDFHDGHHSDKKSJDHFHJGKDKLSLSLJKAHS",
 "LAJDGSJKGLSDKHFKDFHDGHHSDKKSJDFHJGKDKLSLSLKAHS",     "LAKJDGSJKGLSDKHFKDFHDGHHSDKKSJDHFGKDKLSLSLJKAHSJ",
 "LKJDGSJKLSDKHFKDFHDGHHSDKSJDHFHJGKDKLSLSLJKAHS",     "LAKJDGSJKGLSDKHFKDFHDGHHSDKKSJDHHJGKDKLSLSLJKAHSJ",
 "AKJDGSJKGLSDKFKDFHDGHHSSJDHFJGKDKLSLSLJKAHS",        "LAKJDGSJKGLSDKHFKDFHDGHHSDKKSJDFHJGKDKLSLSLJKAHSJ",
 "LAKJDGSJKGLSDKHFKDFHDGHHSDKKSJDHFJGKDKLSLSLJKAHS",   "LAKJDGSJKGLSDKHFKDFHDGHHSDKKSJHFHJGKDKLSLSLJKAHSJ",
 "LAKJDSJKGLSDKHFKDFHDGHHSDKKSJDHFHJGKDLSLSLJKAHS",    "LAKJDGSJKGLSDKHFKDFHDGHHSDKKSDHFHJGKDKLSLSLJKAHSJ",
 "LAKJDSJKGLSDKHFKDFHDGHHSDKKSJDHFHJGKDLSLSLJKAHS",    "LAKJDGSJKGLSDKHFKDFHDGHHSDKKJDHFHJGKDKLSLSLJKAHSJ",
 "LAKJDGSJKGLSDKHFKDFHDHHSDKKSJDHFHJGKDKLSLSLJKAHS",   "LAKDGSJKGLSDKHFKDFHDGHHSDKSJDHFHJGKDKLSLSLJKAHSJ",
 "LAKJDGSJKGLSDKHFKDFHDGHHSDKSJDHFHJGKDKLSLSLJKAS",    "KJDGSJKGLSDKHFKDFHDGHSDKKSJDHFHJGKDKLSLSLJKAH",
 "LAKJDGSJKGLSDKHFKDFHDGHHDKKSJDHFHJGKDKLSLSLJKAHS",   "LAKDGSJKGLSDKFHDGHHSDKSJDHFHJGKDKLSLSLJKAHSJ",
 "LAKJDGSJKGLSKHFKDFHDHHSDKKSJDHFHJGKDKLSLSLJKAHS",    "LAKJDGSJKGLSDKHFKDFHDGHHSKKSJDHFHJGKDKLSLSLJKAHSJ",
 "LAKJDGSJGLSDKHFKDFHDGHHSDKKSJDFHJGKDKLSLSLJKAHS",    "LAKJDGSJKGLSDKHFKDFHDGHHDKKSJDHFHJGKDKLSLSLJKAHSJ"}
Returns: { 1,  3,  7,  13,  17,  27,  43 }
*/