using System;
using System.ComponentModel;

using Calculator.Core;
using Foundation;
using UIKit;

namespace Calculator.iOS
{
	public partial class ViewController : UIViewController
	{
		CalculatorEngine calculatorEngine;

		public ViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			calculatorEngine = new CalculatorEngine ();
			calculatorEngine.PropertyChanged += CalculatorPropertyChanged;

			buttonsCollectionView.RegisterClassForCell (typeof(CalculatorButtonCell), CalculatorButtonCell.Id);
			buttonsCollectionView.DataSource = new CalculatorButtonsDataSource (OnCalculatorKeyPress);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		void CalculatorPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			calculationTextField.Text = calculatorEngine.CalculationText;

			resultTextField.Text = calculatorEngine.ResultText;
		}

		void OnCalculatorKeyPress (CalculatorKey key)
		{
			calculatorEngine.ProcessKeyPress (key);
		}
	}
}

