using System;
using System.ComponentModel;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Calculator.Core;

namespace Calculator.Droid
{
	[Activity (Label = "Calculator", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		CalculatorEngine calculatorEngine;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			calculatorEngine = new CalculatorEngine ();
			calculatorEngine.PropertyChanged += CalculatorPropertyChanged;

			var gridView = FindViewById<GridView> (Resource.Id.buttonGridView);
			gridView.Adapter = new ButtonAdapter (this, OnCalculatorKeyPress);
		}

		void CalculatorPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			var calculationTextView = FindViewById<TextView> (Resource.Id.calculationText);
			calculationTextView.Text = calculatorEngine.CalculationText;

			var resultTextView = FindViewById<TextView> (Resource.Id.resultText);
			resultTextView.Text = calculatorEngine.ResultText;
		}

		void OnCalculatorKeyPress (CalculatorKey key)
		{
			calculatorEngine.ProcessKeyPress (key);
		}
	}
}


