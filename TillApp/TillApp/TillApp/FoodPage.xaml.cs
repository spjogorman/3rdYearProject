using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TillApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodPage : ContentPage
	{
	    	int currentState = 1;
        	double firstNumber, secondNumber;
		
		public FoodPage ()
		{
			InitializeComponent ();
			OnClear(this, null);
		}

        private void Button_OnClicked(object sender, EventArgs e)
        {

        }

        private void NumberButton_OnClicked(object sender, EventArgs e)
        {

        }
	
	// Number Button Functionality
        void OnSelectNum(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;   // string of numbers user inputs

           
            if (this.userInput.Text == "0" || currentState < 0) // currentState initalised as 1
            {
                this.userInput.Text = "";
                if (currentState < 0)
                    currentState *= -1; // < 0
            }

            this.userInput.Text += pressed;

            double number;
            if (double.TryParse(this.userInput.Text, out number))
            {
                this.userInput.Text = number.ToString("N0");
                if (currentState == 1)
                {
                    firstNumber = number;
                }
                else
                {
                    secondNumber = number;
                }
            }
        }

        // Clears input
        void OnClear(object sender, EventArgs e)
        {
            firstNumber = 0;
            secondNumber = 0;
            currentState = 1;
            this.userInput.Text = "0";
        }
	
    }
}
