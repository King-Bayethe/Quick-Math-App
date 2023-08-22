using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace Splash_Screen.Views
{
    public class Game
    {

        public string ReturnStringData { get; set; }
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Map { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public int Problems { get; set; }
        public double TotalTime { get; set; }
        //public int Level { get; set; }
        //public int Score { get; set; }

    }
}
