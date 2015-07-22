using System;
using Calculator.Core;
using Foundation;
using UIKit;

namespace Calculator.iOS
{
	public class CalculatorButtonsDataSource : UICollectionViewDataSource
	{
		Action<CalculatorKey> onKeyPressed;

		public CalculatorButtonsDataSource (Action<CalculatorKey> onKeyPressed)
		{
			this.onKeyPressed = onKeyPressed;
		}

		public override nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return 4;
		}

		public override UICollectionViewCell GetCell (UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			
			var cell = (CalculatorButtonCell)collectionView.DequeueReusableCell (CalculatorButtonCell.Id, indexPath);

			CalculatorButton button = null;
			nint index = (indexPath.Section * 4) + indexPath.Item;
			if (index < CalculatorButtons.All.Length)
				button = CalculatorButtons.All [index];

			cell.Button = button;
			cell.OnKeyPressed = onKeyPressed;

			return cell;
		}

		public override nint NumberOfSections (UICollectionView collectionView)
		{
			return 5;
		}
	}
}

