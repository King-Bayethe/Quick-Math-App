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
        public DateTime Date { get; set; }
        public string Map { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public int Problems { get; set; }
        public double TotalTime { get; set; }
    }
}
