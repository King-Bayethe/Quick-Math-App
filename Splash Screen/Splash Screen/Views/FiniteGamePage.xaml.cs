﻿using Microcharts.Forms;
using Splash_Screen.Data;
using Splash_Screen.Models;
using Splash_Screen.ViewModels;
using Splash_Screen.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Splash_Screen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FiniteGamePage : ContentPage
    {
        public FiniteGameViewModel finiteGame;
        public FiniteGamePage(FiniteGame finite)
        {
            InitializeComponent();
            finiteGame = new FiniteGameViewModel(finite);
            this.BindingContext = finiteGame;
            
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
        protected override void OnAppearing()
        {
            
            base.OnAppearing();
        }
       
    }
}