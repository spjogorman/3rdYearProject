using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TillApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            
        }

        private void NumberButton_OnClicked(object sender, EventArgs e)
        {

        }

        async void FoodButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FoodPage());
        }
    }
}
