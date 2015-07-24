using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Calculator.Core
{
	public class CalculatorEngine
	{
		List<CalculatorKey> keysPressed = new List<CalculatorKey> ();

		public CalculatorEngine ()
		{
			CalculationText = "";
			ResultText = "";
		}

		public string CalculationText { get; private set; }
		public string ResultText { get; private set; }

		public event EventHandler<PropertyChangedEventArgs> PropertyChanged;

		protected virtual void OnPropertyChanged (string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null)
				handler (this, new PropertyChangedEventArgs (propertyName));
		}

		public void ProcessKeyPress (CalculatorKey key)
		{
			bool processed = true;
			switch (key) {
			case CalculatorKey.Clear:
				Clear ();
				processed = false;
				break;
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
				processed = true;
				break;
			case CalculatorKey.Plus:
			case CalculatorKey.Minus:
			case CalculatorKey.Multiply:
			case CalculatorKey.Divide:
				processed = OnOperationKeyPressed (key);
				break;
			case CalculatorKey.Point:
				processed = OnPointKeyPressed ();
				break;
			case CalculatorKey.Equal:
				processed = OnEqualKeyPressed ();
				break;
			case CalculatorKey.Backspace:
				processed = OnBackspaceKeyPressed ();
				break;
			case CalculatorKey.PlusMinus:
				processed = OnPlusMinusPressed ();
				break;
			}

			if (processed) {
				keysPressed.Add (key);
				GenerateCalculationText ();
			}
		}

		public void Clear ()
		{
			keysPressed.Clear ();
			CalculationText = "";
			ResultText = "";
			OnCalculationTextChanged ();
			OnResultTextChanged ();
		}

		void OnCalculationTextChanged ()
		{
			OnPropertyChanged ("CalculationText");
		}

		void OnResultTextChanged ()
		{
			OnPropertyChanged ("ResultText");
		}

		bool OnOperationKeyPressed (CalculatorKey key)
		{
			if (keysPressed.Count == 0)
				return false;

			if (LastKeyPressedIsPlusMinus ())
				return false;

			if (LastKeyPressedIsOperation ()) {
				RemoveLastKeyPressed ();
			}
			return true;
		}

		bool LastKeyPressedIsOperation ()
		{
			if (keysPressed.Count > 0) {
				return keysPressed [keysPressed.Count - 1].IsOperationKey ();
			}
			return false;
		}

		bool LastKeyPressedIsPlusMinus ()
		{
			if (keysPressed.Count > 0) {
				return keysPressed [keysPressed.Count - 1] == CalculatorKey.PlusMinus;
			}
			return false;
		}

		void RemoveLastKeyPressed ()
		{
			keysPressed.RemoveAt (keysPressed.Count - 1);
		}

		bool OnEqualKeyPressed ()
		{
			if (keysPressed.Count == 0)
				return false;

			if (LastKeyPressedIsOperation () || LastKeyPressedIsPlusMinus ())
				return false;

			CalculationText = "";
			ResultText = Calculation.GetResult (keysPressed);

			OnCalculationTextChanged ();
			OnResultTextChanged ();

			keysPressed.Clear ();

			return false;
		}

		bool OnBackspaceKeyPressed ()
		{
			if (keysPressed.Count > 0) {
				RemoveLastKeyPressed ();
				GenerateCalculationText ();
			}
			return false;
		}

		bool OnPointKeyPressed ()
		{
			if (keysPressed.Count == 0 || LastKeyPressedIsOperation ()) {
				keysPressed.Add (CalculatorKey.Zero);
				return true;
			}

			if (PointAlreadyPressedForCurrentNumber ()) {
				return false;
			}

			return true;
		}

		bool PointAlreadyPressedForCurrentNumber ()
		{
			for (int i = keysPressed.Count - 1; i >= 0; --i) {
				CalculatorKey key = keysPressed [i];
				if (key == CalculatorKey.Point) {
					return true;
				} else if (key.IsOperationKey ()) {
					return false;
				}
			}
			return false;
		}

		void GenerateCalculationText ()
		{
			var builder = new StringBuilder ();
			foreach (CalculatorKey key in keysPressed) {
				if (key.IsOperationKey ()) {
					builder.AppendFormat (" {0} ", key.GetText ());
				} else {
					builder.Append (key.GetText ());
				}
			}
			CalculationText = builder.ToString ().TrimEnd ();
			OnCalculationTextChanged ();
		}

		bool OnPlusMinusPressed ()
		{
			if (keysPressed.Count == 0)
				return true;

			CalculatorKey lastKeyPressed = keysPressed [keysPressed.Count - 1];
			if (lastKeyPressed == CalculatorKey.PlusMinus) {
				RemoveLastKeyPressed ();
				GenerateCalculationText ();
				return false;
			}

			if (lastKeyPressed.IsNumber ()) {
				int index = FindStartOfCurrentNumber ();
				if (keysPressed [index] == CalculatorKey.PlusMinus) {
					keysPressed.RemoveAt (index);
				} else {
					keysPressed.Insert (index, CalculatorKey.PlusMinus);
				}
				GenerateCalculationText ();
				return false;
			}
			return true;
		}

		int FindStartOfCurrentNumber ()
		{
			for (int i = keysPressed.Count - 1; i >= 0; --i) {
				CalculatorKey key = keysPressed [i];
				if (key.IsOperationKey ()) {
					return i + 1;
				}
			}
			return 0;
		}
	}
}

