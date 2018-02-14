//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Design;
//using System.Linq;
//using System.Runtime.Remoting.Services;
//using System.Text;
//using System.Threading.Tasks;

//namespace BullsAndCows
//{
//	public class Program
//	{
//		public static string MsgInvalidNumber = "Invalid number!";
//		public static string MsgRepeatedNumber = "You have already entered this number!";
//		public static string MsgOpponentWins = "Congratulations! You won!";
//		public static string MsgResult = "You have {0} bulls and {1} cows.";
//		public static string MsgComputerWins = "I am sorry, you lost the game.";
//		public static string MsgError = "An error occured. Please start a new game.";
//		public static string MsgInvalidNumberCowsBullsRestraint = "Invalid number! The result must be a number between 0 and 4.";
//		public static string MsgExit = "Press any key to exit..";
//		public static string MsgAskForBulls = "How many bulls do I have?";
//		public static string MsgAskForCows = "How many cows do I have?";

//		public static Random Rnd = new Random();

//		public static void Main(string[] args)
//		{
////LoadANewGame()
////
//			var optionsForKeyNumberOfTheOpponent = CreateStartListWithPossibleNumbers();
//			var myKeyNumber = ChooseRandomNumber(optionsForKeyNumberOfTheOpponent);
//			var myCurrMove = ChooseRandomNumber(optionsForKeyNumberOfTheOpponent);

//			optionsForKeyNumberOfTheOpponent.Remove(myCurrMove);

//			var opponentKeyNumber = "";
//			var opponentPassedMoves = new List<string>();

//			Console.WriteLine("Please, choose your key number.\n" +
//							  "Constraints:\n" +
//							  "1. It must be a four digit number.\n" +
//							  "2. The first digit must not be zero.\n" +
//							  "3. None of the digits in your number shall repeat.");
//			Console.WriteLine("Lets now start the game!");
//			Console.WriteLine("Please, enter a number!");

//			var opponentCurrMove = "";
////
//			while ((opponentCurrMove = Console.ReadLine()) != myKeyNumber && opponentCurrMove != "Exit" && optionsForKeyNumberOfTheOpponent.Count != 0)
//			{
//				if (!IsValidMove(opponentCurrMove, optionsForKeyNumberOfTheOpponent, opponentPassedMoves))
//				{
//					//opponentCurrMove = Console.ReadLine();
//					continue;
//				}

//				var result = new Dictionary<string, int>();

//				CheckForCowsOrBulls(opponentCurrMove, myKeyNumber, result);

//				if (result["bulls"] == 4)
//				{
//					Exit(MsgOpponentWins);
//				}

//				Console.WriteLine(MsgResult, result["bulls"], result["cows"]);
//				opponentPassedMoves.Add(opponentCurrMove);

//				myCurrMove = ChooseRandomNumber(optionsForKeyNumberOfTheOpponent);

//				optionsForKeyNumberOfTheOpponent.Remove(myCurrMove);

//				Console.WriteLine(myCurrMove);

//				int bulls;
//				int cows;
//				bool tryParseBulls;
//				bool tryParseCows;

//				Console.WriteLine(MsgAskForBulls);

//				while ((tryParseBulls = int.TryParse(Console.ReadLine(), out bulls)) == false || bulls < 0 || bulls > 4)
//					Console.WriteLine(MsgInvalidNumberCowsBullsRestraint);

//				Console.WriteLine(MsgAskForCows);

//				while ((tryParseCows = int.TryParse(Console.ReadLine(), out cows)) == false || cows < 0 || cows > 4)
//					Console.WriteLine(MsgInvalidNumberCowsBullsRestraint);

//				opponentKeyNumber = ProcessResultAndReducePossibleOptionsForKeyNumberOfOpponent(myCurrMove, bulls, cows, optionsForKeyNumberOfTheOpponent);

//				if (myCurrMove == opponentKeyNumber)
//					Exit(MsgComputerWins);

//				Console.WriteLine("Please, enter a number");
//			}

//			if (opponentCurrMove == myKeyNumber)
//				Exit(MsgOpponentWins);

//			else if (opponentCurrMove == "Exit")
//				Exit(MsgExit);

//			else
//				Exit(MsgError);
//		}

//		private static List<string> CreateStartListWithPossibleNumbers()
//		{
//			var possibleNumbersList = new List<string>();

//			for (int i = 1000; i < 9999; i++)
//			{
//				if (IsValidNumber(i.ToString()))
//				{
//					possibleNumbersList.Add(i.ToString());
//				}
//			}

//			return possibleNumbersList;
//		}

//		private static string ChooseRandomNumber(List<string> numbersList)
//		{
//			var randomNumber = Rnd.Next(0, numbersList.Count - 1);

//			return numbersList[randomNumber];
//		}

//		private static void CheckForCowsOrBulls(string opponentsMove, string myKeyNumber, Dictionary<string, int> result)
//		{
//			if (opponentsMove == myKeyNumber)
//			{
//				result["bulls"] = 4;
//				return;
//			}

//			int bulls = 0;
//			int cows = 0;

//			for (int i = 0; i < 4; i++)
//			{
//				if (myKeyNumber[i] == opponentsMove[i])
//				{
//					bulls++;
//					continue;
//				}

//				if (myKeyNumber.Contains(opponentsMove[i].ToString()))
//				{
//					cows++;
//				}
//			}

//			result["bulls"] = bulls;
//			result["cows"] = cows;
//		}

//		private static string ProcessResultAndReducePossibleOptionsForKeyNumberOfOpponent(string numberStr, int bullsResult, int cowsResult, List<string> optionsForKeyNumberOfTheOpponent)
//		{
//			if (bullsResult == 4)
//			{
//				return numberStr;
//			}

//			int bulls = 0;
//			int cows = 0;

//			for (int i = 0; i < optionsForKeyNumberOfTheOpponent.Count; i++)
//			{
//				bulls = 0;
//				cows = 0;

//				for (int j = 0; j < 4; j++)
//				{

//					if (numberStr[j] == optionsForKeyNumberOfTheOpponent[i][j])
//					{
//						bulls++;
//						continue;
//					}

//					if (optionsForKeyNumberOfTheOpponent[i].Contains(numberStr[j].ToString()))
//					{
//						cows++;
//					}
//				}

//				if (bulls != bullsResult || cows != cowsResult)
//				{
//					optionsForKeyNumberOfTheOpponent.Remove(optionsForKeyNumberOfTheOpponent[i]);
//					i--;
//				}
//			}

//			return "";
//		}

//		private static bool IsValidMove(string opponentCurrMove, List<string> optionsForKeyNumberOfTheComputer, List<string> opponentPassedMoves)
//		{
//			int opponentCurrMoveInt;

//			var isNumber = int.TryParse(opponentCurrMove, out opponentCurrMoveInt);

//			if (numberStr.Length != 4 || !IsValidNumber(opponentCurrMove) || !isNumber)
//			{
//				Console.WriteLine(MsgInvalidNumber);
//				return false;
//			}

//			if (IsRepeatedNumber(opponentCurrMove, opponentPassedMoves))
//			{
//				Console.WriteLine(MsgRepeatedNumber);
//				return false;
//			}

//			return true;
//		}

//		private static bool IsValidNumber(string numberStr)
//		{
//			for (int i = 0; i < 3; i++)
//			{
//				for (int j = i + 1; j < 4; j++)
//				{
//					if (numberStr[i] == numberStr[j])
//						return false;
//				}
//			}

//			return true;
//		}

//		private static bool IsRepeatedNumber(string opponentCurrMove, List<string> opponentPassedMoves)
//		{
//			return opponentPassedMoves.Contains(opponentCurrMove);
//		}

//		private static void Exit(string message)
//		{
//			Console.WriteLine(message);
//			var exit = Console.ReadLine();
//			Environment.Exit(1);
//		}
//	}
//}
