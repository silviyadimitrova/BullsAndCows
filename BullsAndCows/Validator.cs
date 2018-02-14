using System;
using System.Collections.Generic;

namespace BullsAndCows
{
	public class Validator
	{
		public static bool IsValidMove(string opponentCurrMove, List<string> optionsForKeyNumberOfTheComputer, List<string> opponentPassedMoves)
		{
			int opponentCurrMoveInt;

			var isNumber = int.TryParse(opponentCurrMove, out opponentCurrMoveInt);

			if (opponentCurrMove.Length != 4 || !IsValidNumber(opponentCurrMove) || !isNumber)
			{
				Console.WriteLine(MessageCreator.MsgInvalidNumber);
				return false;
			}

			if (IsRepeatedNumber(opponentCurrMove, opponentPassedMoves))
			{
				Console.WriteLine(MessageCreator.MsgRepeatedNumber);
				return false;
			}

			return true;
		}

		public static bool IsValidNumber(string numberStr)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = i + 1; j < 4; j++)
				{
					if (numberStr[i] == numberStr[j])
						return false;
				}
			}

			return true;
		}

		private static bool IsRepeatedNumber(string opponentCurrMove, List<string> opponentPassedMoves)
		{
			return opponentPassedMoves.Contains(opponentCurrMove);
		}
	}
}
