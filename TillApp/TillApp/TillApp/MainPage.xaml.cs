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
            var dbID = userInput.Text;
            var newdbID = dbID.Replace(",", "");
            string connectionString = "Server=18.216.25.150;Database=professionalpracticetillsystem;Uid=matt;Pwd=matt";
            string selectQuery = String.Format("select user_name from users where user_id = MD5('"+newdbID+"');");
            MySqlConnection cConn = new MySqlConnection(connectionString);
            cConn.Open();
            

            MySqlCommand command = new MySqlCommand(selectQuery, cConn);
            System.Diagnostics.Debug.WriteLine("Connected");

            command.Connection = cConn;
            command.CommandText = selectQuery;
            var result = command.ExecuteReader();
            var exists = result.HasRows;

            if (exists == true)   // true
            {
                while (result.Read())
                {
                    String name = (string)result["user_name"];
                    DisplayAlert("Login", "Welcome " + name, "Ok");
                    login = 1;
                    loggedOn();
                }
               
            }
            else   // false 
            {
                DisplayAlert("Login", "Login Failed", "Ok");
            }

            cConn.Close();
        }

    }   // Main Page
}   // namespace
