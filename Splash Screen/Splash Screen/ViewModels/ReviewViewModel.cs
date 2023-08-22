using Splash_Screen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splash_Screen.ViewModels
{
    public class ReviewViewModel : List<ReviewModel>
    {
        public string Title { get; set; }
        public string ShortProblem { get; set; } //will be used for jump lists
        public string Subtitle { get; set; }
        private ReviewViewModel(string title, string shortName)
        {
            Title = title;
            ShortProblem = shortName;
        }

        public static IList<ReviewViewModel> All { private set; get; }
    }
}
