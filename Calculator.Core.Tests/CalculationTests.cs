﻿using System;
using Calculator.Core;
using NUnit.Framework;

namespace Calculator.Core.Tests
{
	[TestFixture]
	public class CalculationTests
	{
		CalculatorEngine calculatorEngine;

		[SetUp]
		public void Init ()
		{
			calculatorEngine = new CalculatorEngine ();
		}

		void PressKeys (params CalculatorKey[] keys)
		{
			foreach (CalculatorKey key in keys) {
				calculatorEngine.ProcessKeyPress (key);
			}
		}

		[Test]
		public void TwoPressed ()
		{
			PressKeys (CalculatorKey.Two);

			Assert.AreEqual ("2", calculatorEngine.CalculationText);
		}

		[Test]
		public void TwoThenBackspace ()
		{
			PressKeys (CalculatorKey.Two, CalculatorKey.Backspace);

			Assert.AreEqual ("", calculatorEngine.CalculationText);
		}

		[Test]
		public void TwoThenClear ()
		{
			PressKeys (CalculatorKey.Two, CalculatorKey.Clear);

			Assert.AreEqual ("", calculatorEngine.CalculationText);
		}

		[Test]
		public void TwoNumbersThenBackspace ()
		{
			PressKeys (CalculatorKey.One, CalculatorKey.Two, CalculatorKey.Backspace);

			Assert.AreEqual ("1", calculatorEngine.CalculationText);
		}

		[Test]
		public void PointPressed ()
		{
			PressKeys (CalculatorKey.Point);

			Assert.AreEqual ("0.", calculatorEngine.CalculationText);
		}

		[Test]
		public void PlusPressed ()
		{
			PressKeys (CalculatorKey.Plus);

			Assert.AreEqual ("", calculatorEngine.CalculationText);
		}

		[Test]
		public void OnePlusTwo ()
		{
			PressKeys (CalculatorKey.One, CalculatorKey.Plus, CalculatorKey.Two);

			Assert.AreEqual ("1 + 2", calculatorEngine.CalculationText);
		}

		[Test]
		public void OnePlusTwoEquals ()
		{
			PressKeys (CalculatorKey.One, CalculatorKey.Plus, CalculatorKey.Two, CalculatorKey.Equal);

			Assert.AreEqual ("", calculatorEngine.CalculationText);
			Assert.AreEqual ("3", calculatorEngine.ResultText);
		}

		[Test]
		public void OnePointTwoTimesThreeEquals ()
		{
			PressKeys (CalculatorKey.One, CalculatorKey.Point, CalculatorKey.Two,
				CalculatorKey.Multiply,
				CalculatorKey.Three,
				CalculatorKey.Equal);

			Assert.AreEqual ("3.6", calculatorEngine.ResultText);
		}

		[Test]
		public void ThreeDividedByTwoEquals ()
		{
			PressKeys (CalculatorKey.Three, CalculatorKey.Divide, CalculatorKey.Two, CalculatorKey.Equal);

			Assert.AreEqual ("1.5", calculatorEngine.ResultText);
		}

		[Test]
		public void ThreeMinusTwoEquals ()
		{
			PressKeys (CalculatorKey.Three, CalculatorKey.Minus, CalculatorKey.Two, CalculatorKey.Equal);

			Assert.AreEqual ("1", calculatorEngine.ResultText);
		}

		[Test]
		public void ThreeMinusOneEqualsThenOneKeyPressedThenClear ()
		{
			PressKeys (CalculatorKey.Three, CalculatorKey.Minus, CalculatorKey.One, 
				CalculatorKey.Equal,
				CalculatorKey.One,
				CalculatorKey.Clear);

			Assert.AreEqual ("", calculatorEngine.CalculationText);
			Assert.AreEqual ("", calculatorEngine.ResultText);
		}

		[Test]
		public void OnePlusTwoThenBackspace ()
		{
			PressKeys (CalculatorKey.One, CalculatorKey.Plus, CalculatorKey.Two, CalculatorKey.Backspace);

			Assert.AreEqual ("1 +", calculatorEngine.CalculationText);
		}
	}
}

