using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TillApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CheckoutPage : ContentPage
	{

        int currentState = 1;
        double firstNumber, secondNumber;
        float totalPrice = 0;


        public CheckoutPage ()
		{
            // Database details for accessing it remotely 
            string connectionString = "Server=18.216.25.150;Database=professionalpracticetillsystem;Uid=matt;Pwd=matt";
            InitializeComponent ();
            // function for details
            onLoad(connectionString);

        }

        

        // Function for loading details upon starting the project
        private void onLoad(string connectionString)
        {
            // selects the name,price and quantity of all products with more than 0 quantity
            string getNameQuantPrice = String.Format("select product_name name,product_price price,product_quantity quantity from product where product_quantity > 0;");
            MySqlConnection cConn = new MySqlConnection(connectionString);
            cConn.Open();

            MySqlCommand commandGet = new MySqlCommand(getNameQuantPrice, cConn);
            System.Diagnostics.Debug.WriteLine("Connected");

            commandGet.Connection = cConn;
            commandGet.CommandText = getNameQuantPrice;
            var result = commandGet.ExecuteReader();
            // creates a list then adds each variable to the list
            List<CheckoutItem> items =new List<CheckoutItem>();
            while (result.Read())
            {
                System.Diagnostics.Debug.WriteLine("Reading");
                string name = (string)result["name"];
                int quantity = (int)result["quantity"];
                float price = (float)result["price"];
                // total price
                totalPrice += quantity * price;
                items.Add(new CheckoutItem { Name = name, Price = price, Quantity = quantity});
            }
            // close reader and connection
            result.Close();
            cConn.Close();
            // Displays price upon loading
            DisplayAlert("Your Total Cost is: "+totalPrice, "Press finalize to complete your order", "Ok");
            ListViewchkout.ItemsSource = items;
        }

        public class CheckoutItem
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
            public float Price { get; set; }
        }


        // Finalize payment
        private async void CompleteButton_Clicked(object sender, EventArgs e)
        {
            bool answer = false;
            double paid = Convert.ToDouble(userInput.Text);
            double diff = 0;

            if (totalPrice > paid)
            {
                //if paid amount is less than total cost, display error message
                await DisplayAlert("Payment Incomplete", "Insufficient funds to complete payment", "OK");
            }

            else if (paid >= totalPrice)
            {
                //if paid amount is greater than or equal to total cost, calculate difference
                diff = paid - totalPrice;

                answer = await DisplayAlert("Payment Complete. Your change is: "+diff, "Finalize order?", "Yes", "No");

                //If user clicks yes, finalize order and then push new main page
                if (answer == true)
                {
                    string connectionString = "Server=18.216.25.150;Database=professionalpracticetillsystem;Uid=matt;Pwd=matt";
                    string selectQuery = String.Format("update product set product_quantity = 0 where product_quantity > 0;");
                    MySqlConnection cConn = new MySqlConnection(connectionString);
                    cConn.Open();
                    MySqlCommand command = new MySqlCommand(selectQuery, cConn);
                    command.Connection = cConn;
                    command.CommandText = selectQuery;
                    var result = command.ExecuteReader();
                    cConn.Close();

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
