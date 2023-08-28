using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splash_Screen.Models
{
    public class GamesHistory
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Date { get; set; }
        public string Map { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public int Problems { get; set; }
        public string TotalTime { get; set; }
        public int ID_IS { get; set; }
        public bool Mode { get; set; }
        public string Date_IS { get; set; }
        public string Map_IS { get; set; }
        public string Difficulty { get; set; }
        public double ScoreMultiplier { get; set; }
        public int Level_IS { get; set; }
        public int Score_IS { get; set; }
        public int Lives { get; set; }
        public int Problems_IS { get; set; }
        public string TotalTime_IS { get; set; }
        public int Streak { get; set; }
    }
}
