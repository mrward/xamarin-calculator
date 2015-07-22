using System;
using Calculator.Core;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Calculator.iOS
{
	public class CalculatorButtonCell : UICollectionViewCell
	{
		public static readonly NSString Id = new NSString ("CalculatorButtonCell");

		UIButton uiButton;
		CalculatorButton button;

		[Export ("initWithFrame:")]
		public CalculatorButtonCell (CGRect frame)
			: base (frame)
		{
			uiButton = new UIButton (UIButtonType.RoundedRect);
			uiButton.Frame = ContentView.Frame;
			uiButton.TouchUpInside += ButtonTouchUpInside;
			ContentView.AddSubview (uiButton);
		}

		void ButtonTouchUpInside (object sender, EventArgs e)
		{
			if (button != null && OnKeyPressed != null) {
				OnKeyPressed (button.Key);
			}
		}

		public CalculatorButton Button {
			get { return button; }
			set {
				button = value;
				if (button != null) {
					uiButton.SetTitle (button.Text, UIControlState.Normal);
				} else {
					uiButton.Enabled = false;
				}
			}
		}

		public Action<CalculatorKey> OnKeyPressed { get; set; }
	}
}

