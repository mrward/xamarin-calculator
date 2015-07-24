using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Calculator.Forms.UITests
{
	[TestFixture (Platform.Android)]
	[TestFixture (Platform.iOS)]
	public class Tests
	{
		IApp app;
		Platform platform;

		public Tests (Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest ()
		{
			app = AppInitializer.StartApp (platform);
		}

		void TapButtons (params string[] buttonName)
		{
			foreach (string name in buttonName) {
				TapButton (name);
			}
		}

		void TapButton (string name)
		{
			Func<AppQuery, AppQuery> button = c => c.Button (name);
			app.Tap (button);
		}

		void AssertCalculationTextIs (string expected)
		{
			AssertTextIs ("calculationText", expected);
		}

		void AssertTextIs (string id, string expected)
		{
			AppResult result = app.Query (c => c.Marked (id)).Single ();
			Assert.AreEqual (expected, result.Text);
		}

		void AssertResultTextIs (string expected)
		{
			AssertTextIs ("resultText", expected);
		}

		[Test]
		public void WhenOnePlusTwoButtonsTappedThenCalculationTextIsUpdated ()
		{
			TapButtons ("1", "+", "2");
			app.Screenshot ("1+2");

			AssertCalculationTextIs ("1 + 2");
		}

		[Test]
		public void WhenOnePlusTwoEqualsButtonsTappedThenResultIsThree ()
		{
			TapButtons ("1", "+", "2", "=");
			app.Screenshot ("1+2=3");

			AssertResultTextIs ("3");
		}
	}
}

