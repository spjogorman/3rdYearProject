using MySql.Data.MySqlClient;
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
    public partial class DrinksPage : ContentPage
    {
    
        int currentState = 1;
        double firstNumber, secondNumber;

        public DrinksPage()
        {
            InitializeComponent();
            OnClear(this, null);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            string connectionString = "Server=18.216.25.150;Database=professionalpracticetillsystem;Uid=matt;Pwd=matt";
            Button button = (Button)sender;
            string name = button.Text;

            
            string selectQuery = String.Format("update product set product_quantity = product_quantity + 1 where product_name = '" + name+"';");
            MySqlConnection cConn = new MySqlConnection(connectionString);
            cConn.Open();
            

            MySqlCommand command = new MySqlCommand(selectQuery, cConn);
            System.Diagnostics.Debug.WriteLine("Connected");

            command.Connection = cConn;
            command.CommandText = selectQuery;
            var result = command.ExecuteReader();
            var exists = result.HasRows;
            System.Diagnostics.Debug.WriteLine(exists);
            cConn.Close();
        }

        void SignOut(object sender, EventArgs e)
        {

            Navigation.PushAsync(new MainPage());
            MainPage.login = 0;
        }
        private async void CheckoutButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CheckoutPage());
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
