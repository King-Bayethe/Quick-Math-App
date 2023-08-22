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
    public partial class PracticeSetupPage : ContentPage
    {
        public PracticeSetupPage()
        {
            InitializeComponent();
        }
        async void PracticeStartButton_Clicked(object sender, EventArgs e)
        {
            //Time user input//////
            if ((range1TextBox.Equals(string.Empty)) || (range2TextBox.Equals(string.Empty)) || (operationSelector.SelectedIndex == -1))
            {
                await DisplayAlert("ERROR", "Not all fields have been filled!!!", "OK");
            }
            else
            {
                PracticeGameModel model = new PracticeGameModel
                {

                    //int range1;
                    Range1 = Convert.ToInt32(range1TextBox.Text),

                    //int range2;
                    Range2 = Convert.ToInt32(range2TextBox.Text) + 1

                    // operation
                    ,
                    Operation = operationSelector.SelectedItem.ToString()
                };

                //Open up Practice Game

                await Navigation.PushAsync(new PracticeGamePage(model));

            }
        }
    }
}