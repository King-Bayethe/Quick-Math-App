using System;
using System.Collections.Generic;
using System.Text;

namespace Splash_Screen.Models
{
    public class Review
    {
        private List<string> _myProblems;
        private List<string> _correctAnswers, _myAnswers,_EQ;
        public List<string> UserAnswer { get => _myAnswers; set => _myAnswers = value; }
        public List<string> Problem { get => _myProblems; set => _myProblems = value; }

        public List<string> Equation { get => _EQ; set => _EQ = value; }
        ///Checking 
        public List<string> CorrectAnswer { get => _correctAnswers; set => _correctAnswers = value; }
        public int ProblemCount { get; set; }

    }
}
