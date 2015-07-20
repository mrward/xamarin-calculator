using System;
using System.ComponentModel;
using System.Collections.Generic;

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
			ResultText += number.ToString ();
			OnResultTextChanged ();
		}
	}
}

