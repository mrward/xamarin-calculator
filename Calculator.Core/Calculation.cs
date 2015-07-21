using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Core
{
	class Calculation
	{
		List<CalculatorKey> keys;
		StringBuilder number = new StringBuilder ();
		double result = 0;
		bool firstNumber = true;
		CalculatorKey? operation;

		Calculation (IEnumerable<CalculatorKey> keys)
		{
			this.keys = keys.ToList ();
		}

		public static string GetResult (IEnumerable<CalculatorKey> keys)
		{
			var calculation = new Calculation (keys);
			return calculation.CalculateResult ();
		}

		string CalculateResult ()
		{
			foreach (CalculatorKey key in keys) {
				if (key.IsOperationKey ()) {
					ProcessOperationKey (key);
				} else {
					number.Append (key.GetText ());
				}
			}
			if (operation.HasValue) {
				result = ApplyOperation (result, ConvertCurrentNumberTextToDouble ());
				return result.ToString ();
			}
			return number.ToString ();
		}

		void ProcessOperationKey (CalculatorKey key)
		{
			if (firstNumber) {
				operation = key;
				firstNumber = false;
				result = ConvertCurrentNumberTextToDouble ();
			} else {
				result = ApplyOperation (result, ConvertCurrentNumberTextToDouble ());
			}
			number.Clear ();
		}

		double ConvertCurrentNumberTextToDouble ()
		{
			return double.Parse (number.ToString ());
		}

		double ApplyOperation (double left, double right)
		{
			switch (operation) {
			case CalculatorKey.Divide:
				return left / right;
			case CalculatorKey.Minus:
				return left - right;
			case CalculatorKey.Plus:
				return left + right;
			case CalculatorKey.Multiply:
				return left * right;
			default:
				throw new InvalidOperationException ();
			}
		}
	}
}

