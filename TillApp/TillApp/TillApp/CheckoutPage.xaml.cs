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
	public partial class CheckoutPage : ContentPage
	{

        int currentState = 1;
        double firstNumber, secondNumber;

        public CheckoutPage ()
		{
			InitializeComponent ();
		}

        

        private async void Button_OnClicked(object sender, EventArgs e)
        {
           

        }

        private async void CompleteButton_Clicked(object sender, EventArgs e)
        {
            //these aren't actually implemented, just placeholders for database variables
            bool answer = false;
            double totalCost = 0;
            double paid = 0;
            double diff = 0;

            if (totalCost > paid)
            {
                //if paid amount is less than total cost, display error message
                await DisplayAlert("Payment Incomplete", "Insufficient funds to complete payment", "OK");
            }

            else if (paid >= totalCost)
            {
                //if paid amount is greater than or equal to total cost, calculate difference
                diff = totalCost - paid;

                answer = await DisplayAlert("Payment ready to complete", "Finalize order?", "Yes", "No");

                //If user clicks yes, finalize order and then push new main page
                if (answer == true)
                {
                    //----------- upload order to database at this point ------------------------

                    //pushes new mainpage
                    await Navigation.PushAsync(new MainPage());
                }

                //if user clicks 'No', stays on checkout page
                else
                {
                    //No code needed here
                }
            }
            
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

        private void Button_Clicked(object sender, EventArgs e)
        {

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
