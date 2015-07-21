using System;
using System.Linq;

namespace Calculator.Core
{
	static class CalculatorKeyExtensions
	{
		static readonly CalculatorKey[] operationKeys = new CalculatorKey[] {
			CalculatorKey.Multiply,
			CalculatorKey.Divide,
			CalculatorKey.Plus,
			CalculatorKey.Minus
		};

		public static string GetText (this CalculatorKey key)
		{
			switch (key) {
			case CalculatorKey.Nine:
			case CalculatorKey.Eight:
			case CalculatorKey.Seven:
			case CalculatorKey.Six:
			case CalculatorKey.Five:
			case CalculatorKey.Four:
			case CalculatorKey.Three:
			case CalculatorKey.Two:
			case CalculatorKey.One:
			case CalculatorKey.Zero:
				return ((int)key).ToString ();
			case CalculatorKey.Plus:
				return "+";
			case CalculatorKey.Minus:
				return "-";
			case CalculatorKey.Multiply:
				return "*";
			case CalculatorKey.Divide:
				return "/";
			case CalculatorKey.Point:
				return ".";
			default:
				return "";
			}
		}

		public static bool IsOperationKey (this CalculatorKey key)
		{
			return operationKeys.Contains (key);
		}
	}
}

