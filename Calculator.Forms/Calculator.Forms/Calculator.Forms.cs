using System;

using Xamarin.Forms;

namespace Calculator.Forms
{
	public class App : Application
	{
		public App ()
		{
			var viewModel = new CalculatorViewModel ();
			MainPage = new MainPage ();
			MainPage.BindingContext = viewModel;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

