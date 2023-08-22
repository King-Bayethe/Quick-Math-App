using Splash_Screen.Models;
using Splash_Screen.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splash_Screen
{
    public class MyAppManager
    {
        private static readonly GamesHistory var = new GamesHistory();
        //private static readonly CompetitiveVariables var2 = new CompetitiveVariables();

        public static GamesHistory VariableInstance
        {
            get { return var; }
        }
        /*public static CompetitiveVariables VariableInstance2
        {
            get { return var2; }
        }*/
    }
}
