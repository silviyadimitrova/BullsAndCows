using System;
using System.Collections.Generic;

namespace BullsAndCows
{
	public class Engine
	{
		private List<string> optionsForKeyNumberOfTheOpponent;
		private string myKeyNumber;
		private string myCurrMove;
		private string opponentKeyNumber;
		private List<string> opponentPassedMoves;
		private string opponentCurrMove;
		private static Random Rnd = new Random();

		public void Run()
		{
			LoadNewGame();

			Play();
		}

		private void LoadNewGame()
		{
			optionsForKeyNumberOfTheOpponent = CreateStartListWithPossibleNumbers();
			myKeyNumber = ChooseRandomNumber(optionsForKeyNumberOfTheOpponent);
			myCurrMove = ChooseRandomNumber(optionsForKeyNumberOfTheOpponent);

			optionsForKeyNumberOfTheOpponent.Remove(myCurrMove);

			opponentKeyNumber = "";
			opponentPassedMoves = new List<string>();

			Console.WriteLine("Please, choose your key number.\n" +
			                  "Constraints:\n" +
			                  "1. It must be a four digit number.\n" +
			                  "2. The first digit must not be zero.\n" +
			                  "3. None of the digits in your number shall repeat.\n" +
							  "Lets now start the game!\n" +
			                  "Please, enter a number!");

			opponentCurrMove = "";
		}

		private List<string> CreateStartListWithPossibleNumbers()
		{
			var possibleNumbersList = new List<string>();

			for (int i = 1000; i < 9999; i++)
			{
				if (Validator.IsValidNumber(i.ToString()))
					possibleNumbersList.Add(i.ToString());
			}

			return possibleNumbersList;
		}

		private string ChooseRandomNumber(List<string> numbersList)
		{
			var randomNumber = Rnd.Next(0, numbersList.Count - 1);

			return numbersList[randomNumber];
		}

		private void Play()
		{
			while ((opponentCurrMove = Console.ReadLine()) != myKeyNumber && opponentCurrMove != "Exit" && optionsForKeyNumberOfTheOpponent.Count != 0)
			{
				if (!Validator.IsValidMove(opponentCurrMove, optionsForKeyNumberOfTheOpponent, opponentPassedMoves))
					continue;

				var result = new Dictionary<string, int>();

				CheckForCowsOrBulls(opponentCurrMove, myKeyNumber, result);

				if (result["bulls"] == 4)
					Exit(MessageCreator.MsgOpponentWins);

				Console.WriteLine(MessageCreator.MsgResult, result["bulls"], result["cows"]);
				opponentPassedMoves.Add(opponentCurrMove);

				//
				myCurrMove = ChooseRandomNumber(optionsForKeyNumberOfTheOpponent);

				optionsForKeyNumberOfTheOpponent.Remove(myCurrMove);

				Console.WriteLine(myCurrMove);

				int bulls;
				int cows;
				bool tryParseBulls;
				bool tryParseCows;

				Console.WriteLine(MessageCreator.MsgAskForBulls);

				while ((tryParseBulls = int.TryParse(Console.ReadLine(), out bulls)) == false || bulls < 0 || bulls > 4)
					Console.WriteLine(MessageCreator.MsgInvalidNumberCowsBullsRestraint);

				if (bulls < 4)
				{
					Console.WriteLine(MessageCreator.MsgAskForCows);

					while ((tryParseCows = int.TryParse(Console.ReadLine(), out cows)) == false || cows < 0 || cows > 4 || bulls + cows > 4)
						Console.WriteLine(MessageCreator.MsgInvalidNumberCowsBullsRestraint);

					ReducePossibleOptionsForKeyNumberOfOpponent(myCurrMove, bulls, cows, optionsForKeyNumberOfTheOpponent);
				}
				else
				{
					Exit(MessageCreator.MsgComputerWins, MessageCreator.MsgExit);
				}

				Console.WriteLine("Please, enter a number");
			}

			if (opponentCurrMove == myKeyNumber)
				Exit(MessageCreator.MsgOpponentWins, MessageCreator.MsgExit);

			else if (opponentCurrMove == "Exit")
				Exit(MessageCreator.MsgExit);

			else
				Exit(MessageCreator.MsgError, MessageCreator.MsgExit);
		}

		private static void CheckForCowsOrBulls(string opponentsMove, string myKeyNumber, Dictionary<string, int> result)
		{
			if (opponentsMove == myKeyNumber)
			{
				result["bulls"] = 4;
				return;
			}

			int bulls = 0;
			int cows = 0;

			for (int i = 0; i < 4; i++)
			{
				if (myKeyNumber[i] == opponentsMove[i])
				{
					bulls++;
					continue;
				}

				if (myKeyNumber.Contains(opponentsMove[i].ToString()))
				{
					cows++;
				}
			}

			result["bulls"] = bulls;
			result["cows"] = cows;
		}

		private void ReducePossibleOptionsForKeyNumberOfOpponent(string numberStr, int bullsResult, int cowsResult, List<string> optionsForKeyNumberOfTheOpponentToProcess)
		{
			int bulls = 0;
			int cows = 0;

			for (int i = 0; i < optionsForKeyNumberOfTheOpponentToProcess.Count; i++)
			{
				bulls = 0;
				cows = 0;

				for (int j = 0; j < 4; j++)
				{
					if (numberStr[j] == optionsForKeyNumberOfTheOpponentToProcess[i][j])
					{
						bulls++;
						continue;
					}

					if (optionsForKeyNumberOfTheOpponentToProcess[i].Contains(numberStr[j].ToString()))
						cows++;
				}

				if (bulls != bullsResult || cows != cowsResult)
				{
					optionsForKeyNumberOfTheOpponentToProcess.Remove(optionsForKeyNumberOfTheOpponentToProcess[i]);
					i--;
				}
			}
		}

		private void Exit(params string[] message)
		{
			foreach (var m in message)
				Console.WriteLine(m);
			var exit = Console.ReadKey();
			Environment.Exit(1);
		}
	}
}
