using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using Calculator.Core;

namespace Calculator.Droid
{
	public class ButtonAdapter : BaseAdapter
	{
		Context context;
		Action<CalculatorKey> onKeyPressed;

		public ButtonAdapter (Context context, Action<CalculatorKey> onKeyPressed)
		{
			this.context = context;
			this.onKeyPressed = onKeyPressed;
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}

		public override long GetItemId (int position)
		{
			return 0;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			Button button = null;
			CalculatorButton calculatorButton = CalculatorButtons.All [position];

			if (convertView != null) {
				button = (Button)convertView;
			} else {
				button = new Button (context);
			}

			if (calculatorButton.Text != null) {
				button.Text = calculatorButton.Text;
				button.Click += (sender, e) => onKeyPressed (calculatorButton.Key);
			} else {
				button.Visibility = ViewStates.Invisible;
			}
			return button;
		}

		public override int Count {
			get { return CalculatorButtons.All.Length; }
		}
	}
}

