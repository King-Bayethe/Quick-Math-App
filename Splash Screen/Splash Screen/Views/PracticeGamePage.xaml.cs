using Azure;
using Splash_Screen.Models;
using Splash_Screen.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Splash_Screen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PracticeGamePage : ContentPage
    {
        public PracticeGameViewModel practice;
        public PracticeGamePage(PracticeGame model)
        {
            InitializeComponent();
            practice = new PracticeGameViewModel(model);
            this.BindingContext = model;
        }
        void OnDigitButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            AnswerBox.Text += (string)button.StyleId;
            backspaceButton.IsEnabled = true;
        }

        void OnBackspaceButtonClicked(object sender, EventArgs args)
        {
            string text = AnswerBox.Text;
            AnswerBox.Text = text.Substring(0, text.Length - 1);
            backspaceButton.IsEnabled = AnswerBox.Text.Length > 0;
        }

        void OnClearButtonClicked(object sender, EventArgs e)
        {
            AnswerBox.Text = "";
            backspaceButton.IsEnabled = false;
        }
        private async void Skip(object sender, EventArgs e)
        {
            
            
            int answer = practice.GetAnswer();
            await DisplayAlert("Correct Answer:", practice.Num1 + " " + practice.Operand + " " + practice.Num2 + " = " + answer, "Next Question");
        }
    }
}