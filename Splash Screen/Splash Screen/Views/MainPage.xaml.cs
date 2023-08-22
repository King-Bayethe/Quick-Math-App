using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Splash_Screen.Views;
using Xamarin.CommunityToolkit.Extensions;
using Splash_Screen.Models;
using Splash_Screen.ViewModels;

namespace Splash_Screen.Views
{
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {

        

        public MainPage()
        {
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            InitializeComponent();
            var home = new HomePage();
            var fSetup = new FiniteSetupPage();
            var iSetup = new InfiniteSetupPage();
            var pSetup = new PracticeSetupPage();
            var settings = new SettingsPage();

            Children.Add(home);
            Children.Add(fSetup);
            Children.Add(iSetup);
            Children.Add(pSetup);
            Children.Add(settings);
            

        }
        

        
       

        

        
    }
}
