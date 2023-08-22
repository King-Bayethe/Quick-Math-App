using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splash_Screen.Models
{
    public class FiniteGame
    {

        public string Map { get; set; }
        public int Level { get; set; }
        
        public FiniteGame(string m, int l) 
        {
            Map = m;
            Level = l;
        
        }

    }
    public class RecordFiniteGame
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Date { get; set; }
        public string Map { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public int Problems { get; set; }
        public string TotalTime { get; set; }
        public RecordFiniteGame()
        {

        }
        public RecordFiniteGame(int id, string d, string m, int l, int s, int p, string tt)
        {
            ID = id;
            Date = d;
            Map = m;
            Level = l;
            Score = s;
            Problems = p;
            TotalTime = tt;

        }

    }
}
