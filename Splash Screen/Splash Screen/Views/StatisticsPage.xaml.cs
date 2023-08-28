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
    public partial class StatisticsPage : ContentPage
    {
        public StatisticsPage()
        {
            InitializeComponent();
        }

        
        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new StatisticsItemPage
                {
                    BindingContext = e.SelectedItem as Statistics
                });
            }
        }

        private async void mapStatsSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            string operation = mapStatsSelector.SelectedItem as string;
            listView.ItemsSource = await App.FiniteGameDatabase.GetAllGamesWithOperationAsync(operation);

        }
    }
}