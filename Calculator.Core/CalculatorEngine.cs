using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Calculator.Core
{
	public class CalculatorEngine
	{
		List<CalculatorKey> keysPressed = new List<CalculatorKey> ();

		public CalculatorEngine ()
		{
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
				OnNumberPressed ((int)key);
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
			}

			if (processed) {
				keysPressed.Add (key);
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

		void OnNumberPressed (int number)
		{
			AppendCalculationText (number.ToString ());
		}

		void AppendCalculationText (string text)
		{
			CalculationText += text;
			OnCalculationTextChanged ();
		}

		bool OnOperationKeyPressed (CalculatorKey key)
		{
			if (keysPressed.Count == 0)
				return false;
			
			if (LastKeyPressedIsOperation ()) {
				RemoveLastKeyPressed ();
				ReplaceLastCalculationTextCharacter (key.GetText ());
			} else {
				AppendCalculationText (key.GetText ());
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

		void RemoveLastKeyPressed ()
		{
			keysPressed.RemoveAt (keysPressed.Count - 1);
		}

		void ReplaceLastCalculationTextCharacter (string text)
		{
			CalculationText = CalculationText.Substring (0, CalculationText.Length - 1);
			AppendCalculationText (text);
		}

		bool OnEqualKeyPressed ()
		{
			if (keysPressed.Count == 0)
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
				ReplaceLastCalculationTextCharacter ("");
			}
			return false;
		}

		bool OnPointKeyPressed ()
		{
			if (keysPressed.Count == 0 || LastKeyPressedIsOperation ()) {
				keysPressed.Add (CalculatorKey.Zero);
				AppendCalculationText ("0.");
				return true;
			}

			if (PointAlreadyPressedForCurrentNumber ()) {
				return false;
			}

			AppendCalculationText (".");

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
	}
}

