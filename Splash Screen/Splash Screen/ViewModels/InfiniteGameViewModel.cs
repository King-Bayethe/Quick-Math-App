using Azure;
using Microsoft.Identity.Client.Extensions.Msal;
using Microsoft.IdentityModel.Tokens;
using Splash_Screen.Data;
using Splash_Screen.Models;
using Splash_Screen.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Splash_Screen.ViewModels
{
    public class InfiniteGameViewModel : BaseViewModel
    {
        public ICommand SubmitButtonCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        public INavigation Navigation { get; set; }
        private string score, duration, problems, difficulty, num1, num2, operand, answerbox;
        //Setup Variables
        private double value = 1.0, scoreMultiplier, points;
        private int totalTime;
        private string operation;
        Records high = new Records();
        List<ReviewModel> ReviewList = new List<ReviewModel>();
        Random randomizer = new Random();
        Stopwatch _stopwatch = new Stopwatch();
        //Ranges
        private int range1 = 1, range2 = 10, time = 20, streak,problemsDone, level, lives;
        //Setup Variables

        private double mapMultiplier;
        private double levelMultiplier;
        private double difficultyMultiplier;

        //CONSTANT VARIABLES
        private const int totalProblems = 100;
        private const int addProblemValue = 250;
        private const int subtractProblemValue = 500;
        private const int multiplyProblemValue = 750;
        private const int divideProblemValue = 1000;
        private bool _GameInProgress;
        private int bonus;
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
        private int problemsgotRight;
        private int problemsgotWrong;
        private int problemLimit;
        private int totalLives;
        private int problemsCount;

        public RecordFiniteGame Expense { get; set; }


        public InfiniteGameViewModel(InfiniteGame note)
        {
            
            operation = note.Map.ToString();
            Reset();
            DifficultySetting();
            ProblemsSetup();
            Setup();
            SubmitButtonCommand = new Command(SubmitButton);
            BackCommand = new Command(Back);

        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        private void SubmitButton()
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
            if (CheckAnswer() && !AnswerBox.IsNullOrEmpty())
            {
                
                Scoring(true);
                //Display
                LevelingUp(streak);
                Level = level;
                Problems = problemsCount.ToString();
                //Generate next problem


                //button clicked
            }
            else
            {
                Scoring(false);

            }
            problemsDone++;
            AnswerBox = "";
            NextQuestion();
            Duration = "00:00:20";
            time = 20;
            value = 1.0;
            UpdateUI();
        }
        public string Score
        {
            get { return score; }
            set
            {
                score = value;
                OnPropertyChanged("Score");
            }
        }
        public double ProgressBar
        {
            get { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged("ProgressBar");
            }
        }
        public string Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged("Duration");
            }
        }
        public string Problems
        {
            get { return problems; }
            set
            {
                problems = value;
                OnPropertyChanged("Problems");
            }
        }
        public int Streak
        {
            get { return streak; }
            set
            {
                streak = value;
                OnPropertyChanged("Streak");
            }
        }
        public double ScoreMultiplier
        {
            get { return scoreMultiplier; }
            set
            {
                scoreMultiplier = value;
                OnPropertyChanged("ScoreMultiplier");
            }
        }
        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                OnPropertyChanged("Level");
            }
        }
        public int Bonus
        {
            get { return bonus; }
            set
            {
                bonus = value;
                OnPropertyChanged("Bonus");
            }
        }
        public int Lives
        {
            get { return lives; }
            set
            {
                lives = value;
                OnPropertyChanged("Lives");
            }
        }
        public string Num1
        {
            get { return num1; }
            set
            {
                num1 = value;
                OnPropertyChanged("Num1");
            }
        }
        public string Num2
        {
            get { return num2; }
            set
            {
                num2 = value;
                OnPropertyChanged("Num2");
            }
        }
        public string Operand
        {
            get { return operand; }
            set
            {
                operand = value;
                OnPropertyChanged("Operand");
            }
        }
        public string AnswerBox
        {
            get
            {

                return answerbox;
            }
            set
            {
                answerbox = value;
                OnPropertyChanged("AnswerBox");
            }
        }
        private void Reset()
        {
            //VARIABLES
            ///Problems
            problemsDone = 0;
            problemsgotRight = 0;
            problemsgotWrong = 0;
            //Score
            points = 0;
            //Multipliers
            scoreMultiplier = 0;
            //Time
            totalTime = 0;
            time = 20;

            
            streak = 0;

            //DISPLAY
            Bonus = 50;
            Score = "0";
            Lives = 0;
            Level = 0;

            Duration = "00:00:20";
            //VARIABLES
            problemsDone = 0;
            totalTime = 0;

        }
        private void LevelingUp(int consecutive)
        {
            int j = problemLimit;
            if (consecutive == problemLimit || ((consecutive % problemLimit) == 0))
            {
                //Dependent on Level
                range1 += (level);
                range2 += level;
                levelMultiplier = level * 0.25;
                ScoreMultiplier += 0.5;
                bonus += 1000;
                Level++;
                problemsCount = 0;
                Level = level;
                Problems = problemsCount.ToString();
                
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
                    problemLimit = 40;
                    totalLives = 1;
                    //SCORING
                    difficultyMultiplier = 3;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;

            }


        }
        private void Scoring(bool isCorrect)
        {
            if(isCorrect)
            {
                points += Reward();
                points += points * levelMultiplier * difficultyMultiplier * mapMultiplier * ScoreMultiplier;
            }
            else
            {
                Lives--;
                Lives = lives;
                points -= Penalty();
            }
            
            Score = points.ToString();

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
            ScoreMultiplier = 0.00;
            string problem = Num1 + " " + Operand + " " + Num2;
            string answer = GetAnswer().ToString();
            string userAnswer = AnswerBox;
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

            string problem = Num1 + " " + Operand + " " + Num2;
            string answer = GetAnswer().ToString();
            string userAnswer = AnswerBox;
            ReviewList.Add(new ReviewModel(problem, answer, true, userAnswer));
            return reward;
        }
        public int Trigger()
        {
            int trigger = 0;
            int answerSpeed = 20 - time;
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
                    Num1 = addend1.ToString();
                    Num2 = addend2.ToString();
                    Operand = "+";
                    mapMultiplier = 1.5;
                    break;
                case "Subtraction":
                    // Fill in the subtraction problem.
                    minuend = randomizer.Next(range1, range2);
                    subtrahend = randomizer.Next(range1, minuend);
                    //Display
                    Num1 = minuend.ToString();
                    Num2 = subtrahend.ToString();
                    Operand = "-";
                    mapMultiplier = 1.75;
                    break;
                case "Multiplication":
                    // Fill in the multiplication problem.
                    multiplicand = randomizer.Next(range1, range2);
                    multiplier = randomizer.Next(range1, range2);
                    //Display
                    Num1 = multiplicand.ToString();
                    Num2 = multiplier.ToString();
                    Operand = "*";
                    mapMultiplier = 2.0;
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
                    Num1 = dividend.ToString();
                    Num2 = divisor.ToString();
                    Operand = "/";
                    mapMultiplier = 2.5;
                    break;
                case "Squared":
                    // Fill in the square problem.
                    square = randomizer.Next(range1, range2);
                    //Display
                    Num2 = square.ToString() + "^2";
                    //num1.Visible = false;
                    //Operation.Visible = false;
                    mapMultiplier = 3.0;
                    break;
                case "Cubed":
                    // Fill in the cube problem.
                    cube = randomizer.Next(range1, range2);
                    //Display
                    Num2 = cube.ToString() + "^3";
                    //num1.Visible = false;
                    //Operation.Visible = false;
                    mapMultiplier = 3.0;
                    break;

                case "Square Root":
                    // Fill in the square root problem.
                    sqrt = randomizer.Next(range1, range2);
                    //Display
                    Num2 = "√" + sqrt.ToString();
                    //num1.Visible = false;
                    //Operation.Visible = false;
                    mapMultiplier = 3.5;
                    break;
                case "Cube Root":
                    // Fill in the cube root problem.
                    cbrt = randomizer.Next(range1, range2);
                    //Display
                    Num2 = "∛" + cbrt.ToString();
                    //num1.Visible = false;
                    //Operation.Visible = false;
                    mapMultiplier = 3.5;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;

            }




        }
        private void Setup()
        {
            Lives = totalLives;
            //3.Timer
            Duration = (time).ToString(@"hh\:mm\:ss");
            //6.problems left

            Level = level;
            Problems = problemsCount.ToString();

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


                if (time == 0)
                {
                    Scoring(false);
                    problemsDone++;
                    AnswerBox = "";
                    
                    NextQuestion();
                    Duration = "00:00:20";
                    time = 20;
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
                    time--;
                    value -= 0.05;
                    Duration = TimeSpan.FromSeconds(time).ToString(@"hh\:mm\:ss");
                    ProgressBar = value;
                }



            }

        }
        async void RecordStats()
        {

            
            string totalTime = TimeSpan.FromSeconds(_stopwatch.Elapsed.TotalSeconds).ToString(@"hh\:mm\:ss");
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            RecordInfiniteGame game = new RecordInfiniteGame
            {

                Date = date,
                Map = operation,
                Difficulty = difficulty,
                ScoreMultiplier = ScoreMultiplier,
                Streak = Streak,
                Level = Convert.ToInt32(Level),
                Score = Convert.ToInt32(Math.Ceiling(points)),
                Problems = problemsDone,
                Lives = Lives,
                TotalTime = totalTime,
            };
            await App.InfiniteGameDatabase.SaveGameAsync(game);
            SaveStats();


            await Navigation.PushAsync(new ReviewPage(ReviewList));

        }
        public void UpdateUI()
        {

            OnPropertyChanged("Score");
            OnPropertyChanged("Duration");
            OnPropertyChanged("Problems");
            OnPropertyChanged("ProgressBar");
            OnPropertyChanged("Lives");
            OnPropertyChanged("Level");
            OnPropertyChanged("Bonus");
            OnPropertyChanged("ScoreMultiplier");
            OnPropertyChanged("Lives");
            OnPropertyChanged("Num1");
            OnPropertyChanged("Num2");
            OnPropertyChanged("Operand");
        }
        public void SaveStats()
        {
            string key = "highscore" + "_" + operation + "_" + "InfiniteMode";
            string key2 = "problems" + "_" + operation + "_" + "InfiniteMode";
            string key3 = "level" + "_" + operation + "_" + "InfiniteMode";
            string key4 = "streak" + "_" + operation + "_" + "InfiniteMode";
            string key5 = "multiplier" + "_" + operation + "_" + "InfiniteMode";
            string key6 = "duration" + "_" + operation + "_" + "InfiniteMode";
            string key7 = "bestMap" + "_" + operation + "_" + "InfiniteMode";
            string key8 = "difficulty" + "_" + operation + "_" + "InfiniteMode";

            int oldScore = Preferences.Get(key, 0);
            if (Convert.ToInt32(Score) > oldScore)
            {
                Preferences.Set(key, Score);
                Preferences.Set(key7, operation);
                Preferences.Set(key8, difficulty);
            }
            int oldCount = Preferences.Get(key2, 0);
            if (problemsDone > oldCount)
            {
                Preferences.Set(key2, problemsDone);
            }
            int oldLevel = Preferences.Get(key3, 0);
            if (Convert.ToInt32(Level) > oldLevel)
            {
                Preferences.Set(key3, level);
            }
            int oldStreak = Preferences.Get(key4, 0);
            if (Streak > oldStreak)
            {
                Preferences.Set(key4, Streak);
            }
            int oldMultiplier = Preferences.Get(key5, 0);
            if (Convert.ToInt32(ScoreMultiplier) > oldMultiplier)
            {
                Preferences.Set(key5, ScoreMultiplier);
            }
            string totalTime = TimeSpan.FromSeconds(_stopwatch.Elapsed.TotalSeconds).ToString(@"hh\:mm\:ss");
            string oldDuration = Preferences.Get(key6, "None");
            int hh = Convert.ToInt32(oldDuration.Substring(0, 2));
            int mm = Convert.ToInt32(oldDuration.Substring(3, 2));
            int ss = Convert.ToInt32(oldDuration.Substring(6, 2));
            if(hh == _stopwatch.Elapsed.Hours)
            {
                if (mm == _stopwatch.Elapsed.Minutes)
                {

                    if (ss < _stopwatch.Elapsed.Seconds)
                    {

                        Preferences.Set(key6, totalTime);
                    }
                }
                else
                {
                    if (mm < _stopwatch.Elapsed.Minutes)
                    {

                        Preferences.Set(key6, totalTime);
                    }
                }
            }
            else
            {
                if (hh < _stopwatch.Elapsed.Hours)
                {
                
                    Preferences.Set(key6, totalTime);
                }

            }
            
        }
        public bool CheckAnswer()
        {
            switch (operation)
            {
                case "Addition":
                    if (addend1 + addend2 == Convert.ToInt32(AnswerBox))
                        return true;
                    else
                        return false;

                case "Subtraction":
                    if (minuend - subtrahend == Convert.ToInt32(AnswerBox))
                        return true;
                    else
                        return false;

                case "Multiplication":
                    if (multiplicand * multiplier == Convert.ToInt32(AnswerBox))
                        return true;
                    else
                        return false;
                case "Division":
                    if (dividend / divisor == Convert.ToInt32(AnswerBox))
                        return true;
                    else
                        return false;
                case "Squared":
                    if ((int)Math.Pow(square, 2) == Convert.ToInt32(AnswerBox))
                        return true;
                    else
                        return false;
                case "Cubed":

                    if ((int)Math.Pow(cube, 3) == Convert.ToInt32(AnswerBox))
                        return true;
                    else
                        return false;
                case "Square Root":
                    if ((int)Math.Sqrt((int)Math.Pow(sqrt, 2)) == Convert.ToInt32(AnswerBox))
                        return true;
                    else
                        return false;
                case "Cube Root":
                    if ((int)Math.Ceiling(Math.Pow(Math.Pow(cbrt, 3), (int)1 / 3)) == Convert.ToInt32(AnswerBox))
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
                    // Fill in the addition problem.
                    // Generate two random numbers to add.
                    // store2 the values in the variables 'addend1' and 'addend2'.
                    addend1 = randomizer.Next(range2);
                    addend2 = randomizer.Next(range2);
                    //Display
                    Num1 = addend1.ToString();
                    Num2 = addend2.ToString();
                    Operand = "+";
                    break;
                case "Subtraction":
                    // Fill in the subtraction problem.
                    minuend = randomizer.Next(range1, range2);
                    subtrahend = randomizer.Next(range1, minuend);
                    //Display
                    Num1 = minuend.ToString();
                    Num2 = subtrahend.ToString();
                    Operand = "-";
                    break;
                case "Multiplication":
                    // Fill in the multiplication problem.
                    multiplicand = randomizer.Next(range1, range2);
                    multiplier = randomizer.Next(range1, range2);
                    //Display
                    Num1 = multiplicand.ToString();
                    Num2 = multiplier.ToString();
                    Operand = "*";
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
                    Num1 = dividend.ToString();
                    Num2 = divisor.ToString();
                    Operand = "/";
                    break;
                case "Squared":
                    // Fill in the square problem.
                    square = randomizer.Next(range1, range2);
                    //Display
                    Num2 = square.ToString() + "^2";
                    //Num1.Visible = false;
                    //Operand.Visible = false;
                    break;
                case "Cubed":
                    // Fill in the cube problem.
                    cube = randomizer.Next(range1, range2);
                    //Display
                    Num2 = cube.ToString() + "^3";
                    //Num1.Visible = false;
                    //Operand.Visible = false;
                    break;

                case "Square Root":
                    // Fill in the square root problem.
                    sqrt = randomizer.Next(range1, range2);
                    //Display
                    Num2 = "√" + sqrt.ToString();
                    //Num1.Visible = false;
                    //Operand.Visible = false;
                    break;
                case "Cube Root":
                    // Fill in the cube root problem.
                    cbrt = randomizer.Next(range1, range2);
                    //Display
                    Num2 = "∛" + cbrt.ToString();
                    //Num1.Visible = false;
                    //Operand.Visible = false;
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
