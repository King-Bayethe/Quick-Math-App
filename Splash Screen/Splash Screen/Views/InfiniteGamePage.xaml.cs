using Splash_Screen.Data;
using Splash_Screen.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Splash_Screen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfiniteGamePage : ContentPage
    {
        public InfiniteGamePage(InfiniteGame infinite)
        {
            InitializeComponent();
            difficulty = infinite.Difficulty;
            operation = infinite.Map;
           /* DifficultySetting();
            ProblemsSetup();
            Setup();
            */

        }
        //Setup Variables
        private double value = 1.0;

        private int lives;
        private int totalLives;
        private int level = 0;
        private int problemLimit;
        private int streak;
        private string operation;
        Random randomizer = new Random();
        Record store;

        List<ReviewModel> ReviewList = new List<ReviewModel>();
        Stopwatch _stopwatch = new Stopwatch();
        //Variables dependent on Level
        private double mapScoreMultiplier;
        private double levelMultiplier;
        //TIME 
        private int duration = 20;
        //Ranges
        private int range1 = 1;
        private int range2 = 10;
        //DIFFICULTY
        private double difficultyMultiplier;
        private string difficulty;
        private int range3;
        //CONSTANT VARIABLES
        private const int totalProblems = 100;
        private const int addProblemValue = 250;
        private const int subtractProblemValue = 500;
        private const int multiplyProblemValue = 750;
        private const int divideProblemValue = 1000;
        //RUNNING VARIABLES
        private int problemsDone;//problems completed
        private int problemsCount;// problems left
        private double totalTime;
        private int problemsgotWrong;
        private int problemsgotRight;
        private bool _GameInProgress;
        //SCORING
        private double increase_multiplier;
        private double points;
        private int bonus;
        private int completion;
        private int right;
        private int score;

        private double score_multiplier;
        private double TotalProblemValue;
        //penalty;
        private int timeLost;
        private double decrease_multiplier;
        private int scoreLost;
        //SPEED

        //RECORD
        private int highestStreak;
        private double highestScoreMultiplier;

        //EQUATION
        int addend1,
            addend2,
            minuend,
            subtrahend,
            multiplicand,
            multiplier,
            dividend,
            divisor,
            square,
            sqrt,
            cube,
            cbrt;

        private void Reset()
        {
            //VARIABLES
            ///Problems
            problemsDone = 0;
            problemsgotRight = 0;
            problemsgotWrong = 0;
            //Score
            score = 0; 
            //Multipliers
            score_multiplier = 0; 
            //Time
            totalTime = 0;
            duration = 0;
            
            level = 0;
            streak = 0;

            //DISPLAY
            BonusLabel.Text = problemsDone.ToString();
            DisplayLabel.Text = "000";
            userScore.Text = "00000000000";
            //LivesLabel.Text = "0";

            timeLeftLabel.Text = "00:00:00";
            //VARIABLES
            problemsDone = 0;
            score = 0;
            totalTime = 0;

        }
        private void LevelingUp(int consecutive)
        {
            int j = problemLimit;
            if(consecutive == problemLimit || ((consecutive % problemLimit) == 0))
            {
                 //Dependent on Level
                range1 += level;
                range2 += level;
                levelMultiplier = level * 0.25;
                score_multiplier += 2;
                bonus += 1000;
                level++;
                problemsCount = 0;
                string display = level + " - " + problemsCount;
                DisplayLabel.Text = display;
            }
            
           



        }
        private void DifficultySetting()
        {

            switch (difficulty)
            {

                case "VERY EASY":
                    //TIME

                    //NUMBER OF PROBLEMS FOR A SET
                    problemLimit = 10;
                    totalLives = 20;

                    //SCORING
                    difficultyMultiplier = .25;
                    break;
                case "EASY":
                    //TIME
                    //NUMBER OF PROBLEMS FOR A SET
                    problemLimit = 15;
                    totalLives = 15;
                    //SCORING
                    difficultyMultiplier = 1.75;
                    break;
                case "MORMAL":
                    //NUMBER OF PROBLEMS FOR A SET
                    problemLimit = 20;
                    totalLives = 10;
                    //SCORING
                    difficultyMultiplier = 1;
                    break;

                case "MEDIUM":
                    //NUMBER OF PROBLEMS FOR A SET
                    problemLimit = 25;
                    totalLives = 5;
                    //SCORING
                    difficultyMultiplier = 1.5;
                    break;
                case "HARD":

                    //NUMBER OF PROBLEMS FOR A SET
                    problemLimit = 30;
                    totalLives = 3;
                    //SCORING
                    difficultyMultiplier = 2;
                    break;
                case "VERY HARD":
                    problemLimit =  40;
                    totalLives = 1;
                    //SCORING
                    difficultyMultiplier = 3;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;

            }


        }
        private void Scoring()
        {
            bonus = 50;
            // To Load the high scores


        }
        public int Penalty()
        {
            int penalty = 0;
            switch (operation)
            {
                case "Addition":
                    penalty -= addProblemValue;
                    break;
                case "Subtraction":
                    penalty -= subtractProblemValue;
                    break;
                case "Multiplication":
                    penalty -= multiplyProblemValue;
                    break;
                case "Division":
                    penalty -= divideProblemValue;
                    break;
                case "Squared":
                    penalty -= 350;
                    break;
                case "Cubed":
                    penalty -= 400;
                    break;
                case "Square Root":
                    penalty -= 450;
                    break;
                case "Cube Root":
                    penalty -= 450;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            problemsgotWrong++;
            bonus = 0;
            streak = 0;
            score_multiplier = 0.00;
            string problem = num1.Text + " " + Operation.Text + " " + num2.Text;
            string answer = GetAnswer().ToString();
            string userAnswer = AnswerBox.Text;
            ReviewList.Add(new ReviewModel(problem, answer, false, userAnswer));

            return penalty;
        }
        public int Reward()
        {
            int reward = 0;

            switch (operation)
            {
                case "Addition":
                    reward += addProblemValue;
                    break;
                case "Subtraction":
                    reward += subtractProblemValue;
                    break;
                case "Multiplication":
                    reward += multiplyProblemValue;
                    break;
                case "Division":
                    reward += divideProblemValue;
                    break;
                case "Squared":
                    reward += 350;
                    break;
                case "Cubed":
                    reward += 400;
                    break;
                case "Square Root":
                    reward += 450;
                    break;
                case "Cube Root":
                    reward += 450;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            bonus++;
            problemsgotRight++;
            reward += Trigger() + bonus;

            string problem = num1.Text + " " + Operation.Text + " " + num2.Text;
            string answer = GetAnswer().ToString();
            string userAnswer = AnswerBox.Text;
            ReviewList.Add(new ReviewModel(problem, answer, true, userAnswer));
            return reward;
        }

        public int Trigger()
        {
            int trigger = 0;
            int answerSpeed = 20 - duration;
            //Quickness///
            if (answerSpeed <= 2)
            {
                trigger += 100;
            }
            else if (answerSpeed <= 3)
            {
                trigger += 90;
            }

            else if (answerSpeed <= 4)
            {
                trigger += 80;
            }
            else if (answerSpeed <= 5)
            {
                trigger += 70;
            }
            else if (answerSpeed <= 6)
            {
                trigger += 60;
            }
            else if (answerSpeed <= 7)
            {
                trigger += 50;
            }
            else if (answerSpeed <= 8)
            {
                trigger += 40;
            }
            else if (answerSpeed <= 9)
            {
                trigger += 30;
            }
            else { trigger += 10; }
            answerSpeed = 0;
            return trigger;
        }
        private void ProblemsSetup()
        {
            // Convert the two randomly generated numbers
            // into strings so that they can be displayed
            // in the label controls.
            // operation = randomizer.Next(1, 8);
            switch (operation)
            {

                case "Addition":
                    // Fill in the addition problem.
                    // Generate two random numbers to add.
                    // store2 the values in the variables 'addend1' and 'addend2'.
                    addend1 = randomizer.Next(range2);
                    addend2 = randomizer.Next(range2);
                    //Display
                    num1.Text = addend1.ToString();
                    num2.Text = addend2.ToString();
                    Operation.Text = "+";
                    mapScoreMultiplier = 1.5;
                    break;
                case "Subtraction":
                    // Fill in the subtraction problem.
                    minuend = randomizer.Next(range1, range2);
                    subtrahend = randomizer.Next(range1, minuend);
                    //Display
                    num1.Text = minuend.ToString();
                    num2.Text = subtrahend.ToString();
                    Operation.Text = "-";
                    mapScoreMultiplier = 1.75;
                    break;
                case "Multiplication":
                    // Fill in the multiplication problem.
                    multiplicand = randomizer.Next(range1, range2);
                    multiplier = randomizer.Next(range1, range2);
                    //Display
                    num1.Text = multiplicand.ToString();
                    num2.Text = multiplier.ToString();
                    Operation.Text = "*";
                    mapScoreMultiplier = 2.0;
                    break;

                case "Division":
                    // Fill in the division problem.
                    dividend = randomizer.Next(range1, range2);
                    int temporarydivisor = randomizer.Next(range1, range2);

                    if (temporarydivisor == 0)
                    {
                        while (temporarydivisor == 0)
                        {
                            temporarydivisor = randomizer.Next(range1, range2);

                        }


                    }

                    divisor = temporarydivisor;
                    //Display
                    num1.Text = dividend.ToString();
                    num2.Text = divisor.ToString();
                    Operation.Text = "/";
                    mapScoreMultiplier = 2.5;
                    break;
                case "Squared":
                    // Fill in the square problem.
                    square = randomizer.Next(range1, range2);
                    //Display
                    num2.Text = square.ToString() + "^2";
                    //num1.Visible = false;
                    //Operation.Visible = false;
                    mapScoreMultiplier = 3.0;
                    break;
                case "Cubed":
                    // Fill in the cube problem.
                    cube = randomizer.Next(range1, range2);
                    //Display
                    num2.Text = cube.ToString() + "^3";
                    //num1.Visible = false;
                    //Operation.Visible = false;
                    mapScoreMultiplier = 3.0;
                    break;

                case "Square Root":
                    // Fill in the square root problem.
                    sqrt = randomizer.Next(range1, range2);
                    //Display
                    num2.Text = "√" + sqrt.ToString();
                    //num1.Visible = false;
                    //Operation.Visible = false;
                    mapScoreMultiplier = 3.5;
                    break;
                case "Cube Root":
                    // Fill in the cube root problem.
                    cbrt = randomizer.Next(range1, range2);
                    //Display
                    num2.Text = "∛" + cbrt.ToString();
                    //num1.Visible = false;
                    //Operation.Visible = false;
                    mapScoreMultiplier = 3.5;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;

            }

            

            
        }
        private void Setup()
        {
            lives = totalLives;
            //3.Timer
            timeLeftLabel.Text = (duration).ToString(@"hh\:mm\:ss");
            //6.problems left
            string display = level + " - " + problemsCount;
            DisplayLabel.Text = display;

            LivesLabel.Text = lives.ToString();
            store = new Record();

            // Start the timer.
            _GameInProgress = true;
            _stopwatch.Start();
            StartTimer();
        }
        



        public async void StartTimer()
        {


            // tick every second while game is in progress
            while (_GameInProgress)
            {


                if (duration == 0)
                {
                    score -= Penalty();
                    lives--;
                    LivesLabel.Text = lives.ToString();
                    problemsDone++;
                    AnswerBox.Text = "";
                    userScore.Text = score.ToString();
                    NextQuestion();
                    timeLeftLabel.Text = "00:00:20";
                    duration = 20;
                    value = 1.0;

                }
                else if (lives == 0)
                {
                    // If the user ran out of time, stop the timer, show
                    // a MessageBox, and fill in the answers.
                    //timer1.Stop();
                    _GameInProgress = false;
                    _stopwatch.Stop();
                    RecordStats();
                    
                }
                else
                {
                    await Task.Delay(1000);
                    duration--;
                    value -= 0.05;
                    string s = TimeSpan.FromSeconds(duration).ToString(@"hh\:mm\:ss");
                    timeLeftLabel.Text = s;
                    pb.Progress = value;
                }



            }

        }
        private void SubmitButton_Clicked(object sender, EventArgs e)
        {
            /*
             * 1. enter key to submit//////111
             * 2. store2 problem ////111
             * 3. store2 user answer /////111
             * 4. check user answer  ////111
             * 5. store2 correct answer ==switch///
             * 6. display next problem////111
            */
            // Record
            //Record();

            //Check Answer
            if (CheckAnswer())
            {
                score += Reward();

                // display
                //1. Score

                //3.Timer

                //6.problems left
                LevelingUp(streak);
                string display = level + " - " + problemsCount;
                DisplayLabel.Text = display;

                //Generate next problem
                

                //button clicked
            }
            else
            {
                score -= Penalty();
                lives--;
                LivesLabel.Text = lives.ToString();




            } 
            problemsDone++;
            AnswerBox.Text = "";
            userScore.Text = score.ToString();
            NextQuestion();
                timeLeftLabel.Text = "00:00:20";
                duration = 20;
                value = 1.0;
        }
        void OnDigitButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            AnswerBox.Text += (string)button.StyleId;
            backspaceButton.IsEnabled = true;
        }

        void OnBackspaceButtonClicked(object sender, EventArgs args)
        {
            string text = AnswerBox.Text;
            AnswerBox.Text = text.Substring(0, text.Length - 1);
            backspaceButton.IsEnabled = AnswerBox.Text.Length > 0;
        }

        void OnClearButtonClicked(object sender, EventArgs e)
        {
            AnswerBox.Text = "";
            backspaceButton.IsEnabled = false;
        }
        async void RecordStats()
        {

            totalTime = _stopwatch.Elapsed.TotalSeconds;
            RecordInfiniteGame game = new RecordInfiniteGame
            {
                ID = 0,
                Date = DateTime.Now,
                Map = operation,
                Difficulty = difficulty,
                ScoreMultiplier = score_multiplier,
                Streak = streak,
                Level = level,
                Score = score,
                Problems = problemsDone,
                TotalTime = totalTime,
            };
        InfiniteGameDatabase database = await InfiniteGameDatabase.Instance;
            await database.SaveItemAsync(game);
            SaveStats();
            Color c1 = Color.FromRgb(32, 178, 170);
            //RecordsDatabase database2 = await RecordsDatabase.Instance;
            //database2.UpdateBestGameAsync(game);


            await Navigation.PushAsync(new ReviewPage(ReviewList));

        }

        public void SaveStats()
        {
            string key = "highscore" + "_" + operation + "_" + "InfiniteMode";
            string key2 = "MostProblems" + "_" + operation + "_" + "InfiniteMode";
            string key3 = "HighestLevel" + "_" + operation + "_" + "InfiniteMode";
            string key4 = "HighestStreak" + "_" + operation + "_" + "InfiniteMode";


            
            int oldScore = Preferences.Get(key, 0);
            if (score > oldScore)
            {
                Preferences.Set(key, score);
            }
            int oldCount = Preferences.Get(key2, 0);
            if (problemsDone > oldCount)
            {
                Preferences.Set(key2, problemsDone);
            }
            int oldLevel = Preferences.Get(key3, 0);
            if (level > oldLevel)
            {
                Preferences.Set(key3, level);
            }
            int oldStreak = Preferences.Get(key4, 0);
            if (streak > oldStreak)
            {
                Preferences.Set(key4, streak);
            }
        }
        public bool CheckAnswer()
        {
            switch (operation)
            {
                case "Addition":
                    if (addend1 + addend2 == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;

                case "Subtraction":
                    if (minuend - subtrahend == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;

                case "Multiplication":
                    if (multiplicand * multiplier == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;
                case "Division":
                    if (dividend / divisor == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;
                case "Squared":
                    if ((int)Math.Pow(square, 2) == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;
                case "Cubed":

                    if ((int)Math.Pow(cube, 3) == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;
                case "Square Root":
                    if ((int)Math.Sqrt((int)Math.Pow(sqrt, 2)) == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;
                case "Cube Root":
                    if ((int)Math.Ceiling(Math.Pow(Math.Pow(cbrt, 3), (int)1 / 3)) == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;
                default:
                    Console.WriteLine("Default case");
                    return false;

            }
        }

        public void NextQuestion()
        {
            switch (operation)
            {
                case "Addition":
                    addend1 = randomizer.Next(range2);
                    addend2 = randomizer.Next(range2);
                    num1.Text = addend1.ToString();
                    num2.Text = addend2.ToString();
                    Operation.Text = "+";
                    break;
                case "Subtraction":
                    //Generate next problem
                    minuend = randomizer.Next(range1, range2);
                    subtrahend = randomizer.Next(range1, minuend);
                    num1.Text = minuend.ToString();
                    num2.Text = subtrahend.ToString();
                    Operation.Text = "-";
                    break;
                case "Multiplication":
                    //Generate next problem
                    multiplicand = randomizer.Next(range1, range2);
                    multiplier = randomizer.Next(range1, range2);
                    num1.Text = multiplicand.ToString();
                    num2.Text = multiplier.ToString();
                    Operation.Text = " * ";
                    break;
                case "Division":
                    //Generate next problem
                    dividend = randomizer.Next(range1, range2);
                    int temporarydivisor = randomizer.Next(range1, range2);

                    if (temporarydivisor == 0)
                    {
                        while (temporarydivisor == 0)
                        {
                            temporarydivisor = randomizer.Next(range1, range2);

                        }


                    }

                    divisor = temporarydivisor;
                    //Display
                    num1.Text = dividend.ToString();
                    num2.Text = divisor.ToString();
                    Operation.Text = "/";
                    break;
                case "Squared":
                    //Generate next problem
                    square = randomizer.Next(range1, range2);
                    num2.Text = square.ToString() + "^2";
                    ////num1.Visible = false;
                    ////Operation.Visible = false;
                    break;
                case "Cubed":
                    //Generate next problem
                    cube = randomizer.Next(range1, range2);
                    num2.Text = cube.ToString() + "^3";
                    ////num1.Visible = false;
                    ////Operation.Visible = false;
                    break;
                case "Square Root":
                    //Generate next problem
                    sqrt = randomizer.Next(range1, range2);
                    num2.Text = "√" + sqrt.ToString();
                    //num1 = false;
                    // //Operation.Visible = false;
                    break;
                case "Cube Root":
                    //Generate next problem
                    cbrt = randomizer.Next(range1, range2);
                    num2.Text = "∛" + cbrt.ToString();
                    ////num1.Visible = false;
                    ////Operation.Visible = false;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }

        public int GetAnswer()
        {
            switch (operation)
            {
                case "Addition":
                    return (addend1 + addend2);

                case "Subtraction":

                    return (minuend - subtrahend);

                case "Multiplication":
                    return (multiplicand * multiplier);

                case "Division":
                    return (dividend / divisor);

                case "Squared":
                    return ((int)Math.Pow(square, 2));


                case "Cubed":

                    return ((int)Math.Pow(cube, 3));

                case "Square Root":
                    return ((int)Math.Sqrt((int)Math.Pow(sqrt, 2)));

                case "Cube Root":
                    return ((int)Math.Ceiling(Math.Pow(Math.Pow(cbrt, 3), (int)1 / 3)));


                default:
                    Console.WriteLine("Default case");
                    return 0;

            }
        }
    }
}