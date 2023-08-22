using Splash_Screen.Views;
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
    public partial class NewGamePage : ContentPage
    {
        private MainGameViewModel viewModel;
        
        public NewGamePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new NewGameViewModel();
        }
        async void StartButton_Clicked(object sender, EventArgs e)
        {
            if (((mapSelector.SelectedIndex == -1 || levelSelector.Text.Equals(string.Empty) )))
               
            {
                await DisplayAlert("ERROR", "Not all fields have been filled!!!", "OK");
                //ERRRRRRRROOOOOORRRRRRRRR
            }
            else
            {

                
                string map = (mapSelector.SelectedItem.ToString());
                int level = (levelSelector.Text);
                MyAppManager.VariableInstance.Map = map;
                MyAppManager.VariableInstance.Level = level;
               
                await Navigation.PushAsync(new FiniteGamePage());

            }
        }
    }
}