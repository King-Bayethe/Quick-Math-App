using QuickMathApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Text;

using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace QuickMathApp.ViewModels
{
	public class PracticeModeViewModel 
    {
        
        
        //time
        private int _hours, _seconds, _minutes, _range1, _range2, _totalProblems;
        private string _operation;




        ///get and set methods
        ///Form 2
        public string Operation { get => _operation; set => _operation = value; }
        ///Form 3
        ///Time
        public int Seconds { get => _seconds; set => _seconds = value; }

        public int Minutes { get => _minutes; set => _minutes = value; }
        public int Hours { get => _hours; set => _hours = value; }
        //NOP
        ///Range
        public int Range1 { get => _range1; set => _range1 = value; }
        public int Range2 { get => _range2; set => _range2 = value; }
        ///Questions
        public int TotalProblems { get => _totalProblems; set => _totalProblems = value; }
        
	}
    
}