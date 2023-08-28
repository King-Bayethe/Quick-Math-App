using Azure;
using Splash_Screen.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using static SkiaSharp.SKImageFilter;

namespace Splash_Screen.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private string pointsCS, timeCS, bestMapCS, levelCS, problemsIS, streakIS, pointsIS, timeIS, bestMapIS, levelIS;
        private string diffIS;
        private string scoreMultiplierIS;
        public string ScoreCS
        {
            get { return pointsCS; }
            set
            {
                pointsCS = value;
                OnPropertyChanged("ScoreCS");
            }
        }
        public string DurationCS
        {
            get { return timeCS; }
            set
            {
                timeCS = value;
                OnPropertyChanged("DurationCS");
            }
        }
        public string BestMapCS
        {
            get { return bestMapCS; }
            set
            {
                bestMapCS = value;
                OnPropertyChanged("BestMapCS");
            }
        }
        public string LevelCS
        {
            get { return levelCS; }
            set
            {
                levelCS = value;
                OnPropertyChanged("LevelCS");
            }
        }
        public string ProblemsIS
        {
            get { return problemsIS; }
            set
            {
                problemsIS = value;
                OnPropertyChanged("ProblemsIS");
            }
        }
        public string DifficultyIS
        {
            get { return diffIS; }
            set
            {
                diffIS = value;
                OnPropertyChanged("DifficultyIS");
            }
        }
        public string StreakIS
        {
            get { return streakIS; }
            set
            {
                streakIS = value;
                OnPropertyChanged("StreakIS");
            }
        }
        public string ScoreMultiplierIS
        {
            get { return scoreMultiplierIS; }
            set
            {
                scoreMultiplierIS = value;
                OnPropertyChanged("ScoreMultiplierIS");
            }
        }
        public string ScoreIS
        {
            get { return pointsIS; }
            set
            {
                pointsIS = value;
                OnPropertyChanged("ScoreIS");
            }
        }
        public string DurationIS
        {
            get { return timeIS; }
            set
            {
                timeIS = value;
                OnPropertyChanged("DurationIS");
            }
        }
        public string BestMapIS
        {
            get { return bestMapIS; }
            set
            {
                bestMapIS = value;
                OnPropertyChanged("BestMapIS");
            }
        }
        public string LevelIS
        {
            get { return levelIS; }
            set
            {
                levelIS = value;
                OnPropertyChanged("LevelIS");
            }
        }
        Records record = new Records();
        public HomeViewModel()
        {
            
        }
        public void find(string operation)
        {
            
            ScoreCS = Preferences.Get("highscore" + "_" + operation + "_CompetitiveMode","None");
            DurationCS = Preferences.Get("duration" + "_" + operation + "_CompetitiveMode", "None");
            LevelCS = Preferences.Get("level" + "_" + operation + "_" + "CompetitiveMode","None");
            BestMapCS = Preferences.Get("bestMap" + "_" + operation + "_" + "CompetitiveMode","None");
            ScoreIS = Preferences.Get("highscore" + "_" + operation + "_" + "InfiniteMode","None");
            ProblemsIS = Preferences.Get("problems" + "_" + operation + "_" + "InfiniteMode","None");
            LevelIS = Preferences.Get("level" + "_" + operation + "_" + "InfiniteMode", "None");
            StreakIS = Preferences.Get("streak" + "_" + operation + "_" + "InfiniteMode", "None");
            ScoreMultiplierIS = Preferences.Get("multiplier" + "_" + operation + "_" + "InfiniteMode", "None");
            DurationIS = Preferences.Get("duration" + "_" + operation + "_" + "InfiniteMode", "None");
            BestMapIS = Preferences.Get("bestMap" + "_" + operation + "_" + "InfiniteMode", "None");
            DifficultyIS = Preferences.Get("difficulty" + "_" + operation + "_" + "InfiniteMode", "None");


        }

    }
}
