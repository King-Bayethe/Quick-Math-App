using System;
using System.Collections.Generic;
using System.Text;

namespace Splash_Screen.Models
{
    public class Record
    {
        private int _streak, _correct, _incorrect, _levels, _sets, _problemsCompleted, _mode;
        private double _score, _scoremultiplier;
        private int problems; // 10 questions per level
        private int _time;
        private int stage;
        private string map;
        private string difficulty; // increases the number of questions per level
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

        //COMPETITIVE MODE
        public string Map { get => map; set => map = value; }
        public string Difficulty { get => difficulty; set => difficulty = value; }
        public int Time { get => _time; set => _time = value; }
        public int Stage { get => stage; set => stage = value; }
        ///Form 2
        //INFINITE MODE
        public double Score { get => _score; set => _score = value; }
        public int Streak { get => _streak; set => _streak = value; }
        public int Correct { get => _correct; set => _correct = value; }
        public int Incorrect { get => _incorrect; set => _incorrect = value; }
        public int Levels { get => _levels; set => _levels = value; }
        public int Sets { get => _sets; set => _sets = value; }
        public int ProblemsCompleted { get => _problemsCompleted; set => _sets = value; }
        public int Mode { get => _mode; set => _mode = value; }
        public double ScoreMultiplier { get => _scoremultiplier; set => _scoremultiplier = value; }
        //Store
        private List<string> _myProblems;
        private List<int> _correctAnswers, _myCorrectAnswers, _myAnswers, _incorrectAnswers;
        ///Store Values
        ///user answer and problems
        public List<int> MyAnswers { get => _myAnswers; set => _myAnswers = value; }
        public List<string> MyProblems { get => _myProblems; set => _myProblems = value; }
        ///Checking 
        public List<int> CorrectAnswers { get => _correctAnswers; set => _correctAnswers = value; }
        public List<int> MycorrectAnswers { get => _myCorrectAnswers; set => _myCorrectAnswers = value; }
        public List<int> IncorrectAnswers { get => _incorrectAnswers; set => _incorrectAnswers = value; }



    }
}
