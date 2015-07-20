
using Android.Content;
using Android.Views;
using Android.Widget;
using Calculator.Core;

namespace Calculator.Droid
{
	public class ButtonAdapter : BaseAdapter
	{
		Context context;

		public ButtonAdapter (Context context)
		{
			this.context = context;
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
			string text = CalculatorButtons.All [position].Text;

			if (convertView != null) {
				button = (Button)convertView;
			} else {
				button = new Button (context);
			}

			if (text != null) {
				button.Text = text;
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

