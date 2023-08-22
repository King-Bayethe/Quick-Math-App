using Splash_Screen.Models;
using Splash_Screen.ViewModels;
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
    public partial class FiniteSetupPage : ContentPage
    {
        private FiniteSetupViewModel viewModel;
        public FiniteSetupPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new FiniteSetupViewModel();
        }
        async void FiniteStartButton_Clicked(object sender, EventArgs e)
        {
            if (((mapFiniteSelector.SelectedIndex == -1 || levelSelector.Text.Equals(string.Empty))))

            {
                await DisplayAlert("ERROR", "Not all fields have been filled!!!", "OK");
                //ERRRRRRRROOOOOORRRRRRRRR
            }
            else
            {


                string map = (mapFiniteSelector.SelectedItem.ToString());
                int level = (levelSelector.Text);
                FiniteGame finite = new FiniteGame(map, level);

                await Navigation.PushAsync(new FiniteGamePage(finite));

            }
        }
    }
}