using System;
using System.ComponentModel;
using System.Windows.Input;
using Calculator.Core;
using Xamarin.Forms;

namespace Calculator.Forms
{
	public class CalculatorViewModel : INotifyPropertyChanged
	{
		CalculatorEngine calculatorEngine;
		string calculationText = " ";
		string resultText = " ";

		public CalculatorViewModel ()
		{
			calculatorEngine = new CalculatorEngine ();
			calculatorEngine.PropertyChanged += CalculatorEnginePropertyChanged;
			KeyPressCommand = new Command<string> (key => OnKeyPress (key));
		}

		void CalculatorEnginePropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			CalculationText = calculatorEngine.CalculationText;
			ResultText = calculatorEngine.ResultText;
		}

		public string CalculationText {
			get { return calculationText; }
			set {
				calculationText = value;
				if (string.IsNullOrEmpty (calculationText)) {
					calculationText = " "; // HACK to force grid view to allocate space.
				}
				OnPropertyChanged ("CalculationText");
			}
		}

		public string ResultText {
			get { return resultText; }
			set {
				resultText = value;
				if (string.IsNullOrEmpty (resultText)) {
					resultText = " "; // HACK to force grid view to allocate space.
				}
				OnPropertyChanged ("ResultText");
			}
		}

		public Command<string> KeyPressCommand { get; private set; }

		void OnKeyPress (string key)
		{
			var calculatorKey = (CalculatorKey)Enum.Parse (typeof(CalculatorKey), key, true);
			calculatorEngine.ProcessKeyPress (calculatorKey);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged (string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null)
				handler (this, new PropertyChangedEventArgs (propertyName));
		}
	}
}

