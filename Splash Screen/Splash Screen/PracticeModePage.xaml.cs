using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using QuickMathApp.Views.Mode.PM;
using QuickMathApp.ViewModels;

namespace QuickMathApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PracticeModePage : ContentPage
    {
        
        public PracticeModePage()
        {
            InitializeComponent();
        }

        async void StartButton_Clicked(object sender, EventArgs e)
        {
            //Time user input//////
            if ((hoursTextBox.Equals("0") || hoursTextBox.Equals(" ") && 
                (minutesTextBox.Equals("0") || minutesTextBox.Equals(" ")) && 
                (secondsTextBox.Equals("0") || secondsTextBox.Equals(" ") )&& 
                (timeOnOff.On == true)) ||
                (range1TextBox.Equals(" ") )||
                (range2TextBox.Equals(" ") )||
                (numberOfProblemsTextBox.Equals("0") || numberOfProblemsTextBox.Equals(" ")) && 
                (operationSelector.SelectedIndex == -1))
            {
                await DisplayAlert("ERROR", "Not all fields have been filled!!!", "OK");
                this.InitializeComponent();
            }
            else
            {
                PracticeModeViewModel viewModel = new PracticeModeViewModel
                {
                    Hours = Convert.ToInt32(hoursTextBox.Text),
                    Minutes = Convert.ToInt32(minutesTextBox.Text),
                    Seconds = Convert.ToInt32(secondsTextBox.Text),
                    //int range1;
                    Range1 = Convert.ToInt32(range1TextBox.Text),

                    //int range2;
                    Range2 = Convert.ToInt32(range2TextBox.Text) + 1

                    //# of problems
                    , TotalProblems = Convert.ToInt32(numberOfProblemsTextBox.Text)
                    // operation
                    , Operation = operationSelector.SelectedItem.ToString()
                };
               
                //Open up Practice Game

                if (timeOnOff.On)
                {
                    await Navigation.PushAsync(new YesTimedPage(viewModel));

                }
                else
                {

                    //await Navigation.PushAsync(new NoTimerPage());


                }
            }
        }
        
        private void SwitchCell_OnChanged(object sender, ToggledEventArgs e)
        {
            if (timeOnOff.On)
            {
                hoursTextBox.IsEnabled = true;
                minutesTextBox.IsEnabled = true;
                secondsTextBox.IsEnabled = true;
            }
            else
            {
                hoursTextBox.IsEnabled = false;
                minutesTextBox.IsEnabled = false;
                secondsTextBox.IsEnabled = false;
            }
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            numberOfProblemsTextBox.Text = string.Empty;
            hoursTextBox.Text = string.Empty;   
            minutesTextBox.Text = string.Empty;
            secondsTextBox.Text = string.Empty;
            range1TextBox.Text = string.Empty;
            range2TextBox.Text = string.Empty;
            operationSelector.SelectedIndex = -1;
        }
    }
}