using Microcharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Splash_Screen.ViewModels
{
    public class CompetitiveStatsViewModel : INotifyPropertyChanged
    {
        public Chart Chart { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Invoked whenever a property changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

       
        public CompetitiveStatsViewModel() 
        {

        }
    }
}
