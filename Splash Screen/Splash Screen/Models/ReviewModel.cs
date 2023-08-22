using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Splash_Screen.Models
{
    public class ReviewModel
    {
        public string Problem { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
        public string UserAnswer { get; set; }
        public ReviewModel(string problem, string answer, bool isCorrect, string userAnswer)
        {
            this.Problem = problem;
            this.Answer = answer;
            this.IsCorrect = isCorrect;
            this.UserAnswer = userAnswer;
        }
    }

    public class GroupedReviewModel : ObservableCollection<ReviewModel>
    {
        public string LongProblem { get; set; }
        public string ShortProblem { get; set; }
    }
}
