using Azure;
using Splash_Screen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Splash_Screen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPopup : Popup<Game>
    {
        FiniteGame game;
        public MyPopup(RecordFiniteGame games)
        {
            InitializeComponent();
            userScore.Text = games.Score.ToString();
            userTotalProblems.Text = games.Problems.ToString();
            game = new FiniteGame(games.Map,games.Level);
            string key = "highscore" + "_" + games.Map;
            string key2 = "MostProblems" + "_" + games.Map;
            
            int score = Preferences.Get(key, 0);
            int count = Preferences.Get(key2,  0);
            HighestScore.Text = score.ToString();
            HighestProblems.Text = count.ToString();
            
            
        }
        private void Start(Game round)
        {
            
        }
        private async void RetryGameButton_Clicked(object sender, EventArgs args)
        {



            await Application.Current.MainPage.Navigation.PopToRootAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new FiniteGamePage(game));
            Dismiss(null);
        }
        private async void NewGameButton_Clicked(object sender, EventArgs args)
        {

            

            await Application.Current.MainPage.Navigation.PopAsync();
            Dismiss(null);
            
        }
        private async void MainMenuButton_Clicked(object sender, EventArgs args)
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
            Dismiss(null);
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}