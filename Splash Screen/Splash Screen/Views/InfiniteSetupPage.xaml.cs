using Splash_Screen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Splash_Screen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfiniteSetupPage : ContentPage
    {
        public InfiniteSetupPage()
        {
            InitializeComponent();
        }
        async void InfiniteStartButton_Clicked(object sender, EventArgs e)
        {
            if (((mapSelector.SelectedIndex == -1 || diffSelector.SelectedIndex == -1)))

            {
                await DisplayAlert("ERROR", "Not all fields have been filled!!!", "OK");
                //ERRRRRRRROOOOOORRRRRRRRR
            }
            else
            {


                string map = (mapSelector.SelectedItem.ToString());
                string difficulty = (diffSelector.SelectedItem.ToString());
                InfiniteGame infiniteGame = new InfiniteGame(map, difficulty);
                await Navigation.PushAsync(new InfiniteGamePage(infiniteGame));

            }
        }
    }
}