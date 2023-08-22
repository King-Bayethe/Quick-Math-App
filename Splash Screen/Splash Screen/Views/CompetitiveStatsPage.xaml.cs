using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entry = Microcharts.ChartEntry;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using Microcharts;
using Splash_Screen.Models;
using Splash_Screen.Data;
using System.Globalization;
using Splash_Screen.ViewModels;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Splash_Screen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompetitiveStatsPage : ContentPage
    {
        public ChartsViewModel ChartsView { get; private set; }
        private object selected, selected2, selected3, selected4;
        private  ChartEntry[] stats;
        private string map, level, timeFrame, stat; 
        public CompetitiveStatsPage()
        {
            InitializeComponent();
            ChartsView = new ChartsViewModel();
            this.BindingContext = ChartsView;
            

            
            mapFiniteSelector.SelectedIndex = 0;
            timeframeFiniteSelector.SelectedIndex = 0;
            statFiniteSelector.SelectedIndex = 0;
            
            selected = picker.SelectedItem;
            selected2 = picker.SelectedItem;
            selected3 = picker.SelectedItem;
            selected4 = picker.SelectedItem;
            map = mapFiniteSelector.SelectedItem.ToString();
            timeFrame = timeframeFiniteSelector.SelectedItem.ToString();
            stat = statFiniteSelector.SelectedItem.ToString();
            level = levelFiniteSelector.SelectedItem.ToString();
            GetStats();
            Chart1.Chart = new LineChart { Entries = stats, LineMode = LineMode.Straight };
        }
        private async void GetStats()
        {
            
            List<RecordFiniteGame> games = await App.FiniteGameDatabase.GetQueryAsync(timeFrame, map, level);
            int dt = 5;
            if(timeFrame.Equals("All Time"))
            {
                dt = 0;
            }
            if (stat.Equals("Score"))
            {
                foreach (var item in games)
                {
                    stats = new[]
                    {
                        new ChartEntry(item.Score)
                        {
                            Label = item.Date.Substring(dt),
                            ValueLabel = item.Score.ToString(),
                            Color = SKColor.Parse("#2c3e50")
                        },
                    };
                }
            }
            else if (stat.Equals("Duration"))
            {
                foreach (var item in games)
                {
                    stats = new[]
                    {
                        new ChartEntry(float.Parse(item.TotalTime, CultureInfo.InvariantCulture.NumberFormat))
                        {
                            Label = item.Date.Substring(dt),
                            ValueLabel = item.TotalTime,
                            Color = SKColor.Parse("#2c3e50")
                        },
                    };
                }
            }
            else
            {
                foreach (var item in games)
                {
                    stats = new[]
                    {
                        new ChartEntry(item.Level)
                        {
                            Label = item.Date.Substring(dt),
                            ValueLabel = item.Level.ToString(),
                            Color = SKColor.Parse("#2c3e50")
                        },
                    };
                }
            }
        }
        private void timeframeFiniteSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            timeFrame = timeframeFiniteSelector.SelectedItem.ToString();
            GetStats();
            ChartsView.UpdateCharts();

        }
        private void mapSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            map = mapFiniteSelector.SelectedItem.ToString();
            GetStats();
        }
        private void levelFiniteSelector_SelectedIndexChanged(object sender, EventArgs e)
        {

            level = levelFiniteSelector.SelectedItem.ToString();
            GetStats();
        }

        private void statFiniteSelector_SelectedIndexChanged(object sender, EventArgs e)
        {

            stat = statFiniteSelector.SelectedItem.ToString();
            GetStats();
        }
    }
}