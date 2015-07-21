using System;
using Calculator.Core;

namespace Calculator.Droid
{
	public class CalculatorButtonEventArgs : EventArgs
	{
		public CalculatorKey Key { get; set; }
	}
}

