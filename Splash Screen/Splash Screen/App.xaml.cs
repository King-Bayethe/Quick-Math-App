using Splash_Screen.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Splash_Screen.Views;

namespace Splash_Screen
{
    public partial class App : Application
    {
        private static FiniteGameDatabase database;

        public static FiniteGameDatabase FiniteGameDatabase
        {
            get
            {
                if (database == null)
                {
                    database = new FiniteGameDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FiniteGame.db3"));
                }

                return database;
            }
        }/*
        private static InfiniteGameDatabase database2;

        public static InfiniteGameDatabase InfiniteGameDatabase
        {
            get
            {
                if (database2 == null)
                {
                    database2 = new InfiniteGameDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "InfiniteGame.db3"));
                }

                return database2;
            }
        }
        private static PracticeGameDatabase database3;

        public static PracticeGameDatabase PracticeGameDatabase
        {
            get
            {
                if (database3 == null)
                {
                    database3 = new PracticeGameDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PracticeGame.db3"));
                }

                return database3;
            }
        }*/
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
