using Splash_Screen.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Splash_Screen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReviewPage : ContentPage
    {
        private ObservableCollection<GroupedReviewModel> grouped { get; set; }
        public ReviewPage(List<ReviewModel> ReviewList)
        {
            InitializeComponent();

            grouped = new ObservableCollection<GroupedReviewModel>();
            var correctGroup = new GroupedReviewModel() { LongProblem = "Correct", ShortProblem = "C" };
            var incorrectGroup = new GroupedReviewModel() { LongProblem = "Incorrect", ShortProblem = "I" };
            foreach (var author in ReviewList)
            {
                if (author.IsCorrect)
                {
                    correctGroup.Add(author);
                }
                else
                {
                    incorrectGroup.Add(author);
                }
            }
            grouped.Add(correctGroup); grouped.Add(incorrectGroup);
            lstView.ItemsSource = grouped;
        }

        private async void NewGameButton_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
            
        }

        private async void MainMenuButton_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }
    }
}