using System;

namespace Calculator.Core
{
	public class CalculatorButton
	{
		internal CalculatorButton (CalculatorKey key, string text)
		{
			Key = key;
			Text = text;
		}

		public string Text { get; private set; }
		public CalculatorKey Key { get; private set; }
	}
}

