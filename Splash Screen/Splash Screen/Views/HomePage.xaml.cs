using Splash_Screen.Models;
using Splash_Screen.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Splash_Screen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomeViewModel homeGame;
        public Records records;
        public HomePage()
        {
            InitializeComponent();
            homeGame = new HomeViewModel();
            this.BindingContext = homeGame;
        }

        private void mapStatsSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            homeGame.find(mapStatsSelector.SelectedItem.ToString());

        }
    }
}