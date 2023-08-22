using Azure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splash_Screen.Models
{
    class Records
    {
        public string BestMap { get; set; }

        public int GameID { get; set; }
        public int HighestScore { get; set; }

        public int HighScore { get; set; }
        public double ShortestTime { get; set; }

        public int MostProblems { get; set; }
        public List<string> Keys { get; }
        public Records()
        {
            Keys = new List<string> {
                "highscore_Addition_InfiniteMode",  "level_Addition_InfiniteMode",  "duration_Addition_InfiniteMode",   "bestMap_Addition_InfiniteMode",    "problems_Addition_InfiniteMode",   "difficulty_Addition_InfiniteMode", "streak_Addition_InfiniteMode", "multiplier_Addition_InfiniteMode",
"highscore_Subtraction_InfiniteMode",   "level_Subtraction_InfiniteMode",   "duration_Subtraction_InfiniteMode",    "bestMap_Subtraction_InfiniteMode", "problems_Subtraction_InfiniteMode",    "difficulty_Subtraction_InfiniteMode",  "streak_Subtraction_InfiniteMode",  "multiplier_Subtraction_InfiniteMode",
"highscore_Multiplication_InfiniteMode",    "level_Multiplication_InfiniteMode",    "duration_Multiplication_InfiniteMode", "bestMap_Multiplication_InfiniteMode",  "problems_Multiplication_InfiniteMode", "difficulty_Multiplication_InfiniteMode",   "streak_Multiplication_InfiniteMode",   "multiplier_Multiplication_InfiniteMode",
"highscore_Division_InfiniteMode",  "level_Division_InfiniteMode",  "duration_Division_InfiniteMode",   "bestMap_Division_InfiniteMode",    "problems_Division_InfiniteMode",   "difficulty_Division_InfiniteMode", "streak_Division_InfiniteMode", "multiplier_Division_InfiniteMode",
"highscore_Squared_InfiniteMode",   "level_Squared_InfiniteMode",   "duration_Squared_InfiniteMode",    "bestMap_Squared_InfiniteMode", "problems_Squared_InfiniteMode",    "difficulty_Squared_InfiniteMode",  "streak_Squared_InfiniteMode",  "multiplier_Squared_InfiniteMode",
"highscore_Cubed_InfiniteMode", "level_Cubed_InfiniteMode", "duration_Cubed_InfiniteMode",  "bestMap_Cubed_InfiniteMode",   "problems_Cubed_InfiniteMode",  "difficulty_Cubed_InfiniteMode",    "streak_Cubed_InfiniteMode",    "multiplier_Cubed_InfiniteMode",
"highscore_SquareRoot_InfiniteMode",    "level_SquareRoot_InfiniteMode",    "duration_SquareRoot_InfiniteMode", "bestMap_SquareRoot_InfiniteMode",  "problems_SquareRoot_InfiniteMode", "difficulty_SquareRoot_InfiniteMode",   "streak_SquareRoot_InfiniteMode",   "multiplier_SquareRoot_InfiniteMode",
"highscore_CubeRoot_InfiniteMode",  "level_CubeRoot_InfiniteMode",  "duration_CubeRoot_InfiniteMode",   "bestMap_CubeRoot_InfiniteMode",    "problems_CubeRoot_InfiniteMode",   "difficulty_CubeRoot_InfiniteMode", "streak_CubeRoot_InfiniteMode", "multiplier_CubeRoot_InfiniteMode",
"highscore_Addition_CompetitiveMode",   "level_Addition_CompetitiveMode",   "duration_Addition_CompetitiveMode",    "bestMap_Addition_CompetitiveMode",
"highscore_Subtraction_CompetitiveMode",    "level_Subtraction_CompetitiveMode",    "duration_Subtraction_CompetitiveMode", "bestMap_Subtraction_CompetitiveMode",
"highscore_Multiplication_CompetitiveMode", "level_Multiplication_CompetitiveMode", "duration_Multiplication_CompetitiveMode",  "bestMap_Multiplication_CompetitiveMode",
"highscore_Division_CompetitiveMode",   "level_Division_CompetitiveMode",   "duration_Division_CompetitiveMode",    "bestMap_Division_CompetitiveMode",
"highscore_Squared_CompetitiveMode",    "level_Squared_CompetitiveMode",    "duration_Squared_CompetitiveMode", "bestMap_Squared_CompetitiveMode",
"highscore_Cubed_CompetitiveMode",  "level_Cubed_CompetitiveMode",  "duration_Cubed_CompetitiveMode",   "bestMap_Cubed_CompetitiveMode",
"highscore_SquareRoot_CompetitiveMode", "level_SquareRoot_CompetitiveMode", "duration_SquareRoot_CompetitiveMode",  "bestMap_SquareRoot_CompetitiveMode",
"highscore_CubeRoot_CompetitiveMode",   "level_CubeRoot_CompetitiveMode",   "duration_CubeRoot_CompetitiveMode",    "bestMap_CubeRoot_CompetitiveMode",
"highscore_InfiniteMode",  "level_InfiniteMode",  "duration_InfiniteMode",   "bestMap_InfiniteMode",    "problems_InfiniteMode",   "difficulty_InfiniteMode", "streak_InfiniteMode", "multiplier_InfiniteMode",
"highscore_CompetitiveMode",   "level_CompetitiveMode",   "duration_CompetitiveMode",    "bestMap_CompetitiveMode",
            };
        }
    }
}
