using System;

namespace Calculator.Core
{
	public static class CalculatorButtons
	{
		public static CalculatorButton Backspace = new CalculatorButton (CalculatorKey.Backspace, "←");
		public static CalculatorButton Clear = new CalculatorButton (CalculatorKey.Clear, "C");
		public static CalculatorButton PlusMinus = new CalculatorButton (CalculatorKey.PlusMinus, "±");
		public static CalculatorButton Divide = new CalculatorButton (CalculatorKey.Divide, "/");
		public static CalculatorButton Multiply = new CalculatorButton (CalculatorKey.Multiply, "*");
		public static CalculatorButton Minus = new CalculatorButton (CalculatorKey.Minus, "-");
		public static CalculatorButton Plus = new CalculatorButton (CalculatorKey.Plus, "+");
		public static CalculatorButton Equal = new CalculatorButton (CalculatorKey.Equal, "=");
		public static CalculatorButton Nine = new CalculatorButton (CalculatorKey.Nine, "9");
		public static CalculatorButton Eight = new CalculatorButton (CalculatorKey.Eight, "8");
		public static CalculatorButton Seven = new CalculatorButton (CalculatorKey.Seven, "7");
		public static CalculatorButton Six = new CalculatorButton (CalculatorKey.Six, "6");
		public static CalculatorButton Five = new CalculatorButton (CalculatorKey.Five, "5");
		public static CalculatorButton Four = new CalculatorButton (CalculatorKey.Four, "4");
		public static CalculatorButton Three = new CalculatorButton (CalculatorKey.Three, "3");
		public static CalculatorButton Two = new CalculatorButton (CalculatorKey.Two, "2");
		public static CalculatorButton One = new CalculatorButton (CalculatorKey.One, "1");
		public static CalculatorButton Zero = new CalculatorButton (CalculatorKey.Zero, "0");
		public static CalculatorButton Point = new CalculatorButton (CalculatorKey.Point, ".");

		public static CalculatorButton[] All = new CalculatorButton[] {
			Backspace, Clear, PlusMinus, Divide,
			Seven, Eight, Nine, Multiply,
			Four, Five, Six, Minus,
			One, Two, Three, Plus,
			Zero, Point, Equal
		};
	}
}

