using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TillApp.Model;
using Xamarin.Forms;

namespace TillApp
{
    public partial class MainPage : ContentPage
    {
    
        int currentState = 1;
        double firstNumber, secondNumber;
        
        public MainPage()
        {
            InitializeComponent();
            OnClear(this, null);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {

        }

        private async void CheckoutButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CheckoutPage());
        }
          
        async void FoodButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FoodPage());
        }

        async void DrinksButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DrinksPage());
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

        void SignIn(object sender, EventArgs e)
        {
            Users user = new Users(userInput.Text);

            if (user.CheckUser())   // true
            {
                DisplayAlert("Login", "Login Successful", "Ok");
            }
            else   // false 
            {
                DisplayAlert("Login", "Login Failed", "Ok");
            }


        }   // SignIn()

    }   // Main Page
}   // namespace
