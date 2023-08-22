using Azure;
using Microsoft.IdentityModel.Tokens;
using Splash_Screen.Models;
using Splash_Screen.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Splash_Screen.ViewModels
{
    public class FiniteGameViewModel : BaseViewModel
    {
        public ICommand CreateNoteCommand { protected set; get; }
        public ICommand SubmitButtonCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        public INavigation Navigation { get; set; }
        private string score,duration,problems,num1,num2,operand, level, answerbox;
        //Setup Variables
        private double value = 1.0;

        private string operation;
        Records high = new Records();
        Random randomizer = new Random();
        Stopwatch _stopwatch = new Stopwatch();
        //Ranges
        private int range1 = 1, range2 = 10, time = 20;
        //Setup Variables

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
        private int problemsDone;
        private int points;

        public RecordFiniteGame Expense { get; set; }


        public FiniteGameViewModel(FiniteGame note)
        {
            Level = note.Level.ToString();
            operation = note.Map.ToString();
            //Categories = new ObservableCollection<CategoryViewModel>();
            //GetItems();
            Reset();
            LevelSetup();
            Scoring();
            Setup();
            SubmitButtonCommand = new Command(SubmitButton);
            BackCommand = new Command(Back);

        }
        private void LevelSetup()
        {
            //Dependent on Level
            range1 += Convert.ToInt32(level);
            range2 += Convert.ToInt32(level);


        }

        

        private void Reset()
        {
            //DISPLAY
            Score = " ";
            Problems = " ";
            Duration = "00:00:20";
            //VARIABLES
            problemsDone = 0;
            points = 0;
            _stopwatch.Reset();

        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        private void Scoring()
        {
            bonus = 50;
            // To Load the high scores

        }
        public async void StartTimer()
        {


            // tick every second while game is in progress
            while (_GameInProgress)
            {


                if (time == 0)
                {
                    // If the user ran out of time, stop the timer, show
                    // a MessageBox, and fill in the answers.
                    //timer1.Stop();
                    _GameInProgress = false;
                    _stopwatch.Stop();
                    RecordStats();
                    //MessageBox.Show("Time's up! Sorry, you didn't finish in time. Here are your results:");

                    //Open up View Results
                    //FinalStats();
                    //ViewResultsPage vr = new ViewResultsPage(store);
                    //await Navigation.PushAsync(vr);
                }
                if (problemsDone > totalProblems)
                {
                    // If the user ran out of time, stop the timer, show
                    // a MessageBox, and fill in the answers.
                    //timer1.Stop();
                    _GameInProgress = false;
                    _stopwatch.Stop();
                    RecordStats();
                    //MessageBox.Show("You finished. Here are your results:");

                    //Open up View Results
                    // FinalStats();
                    //ViewResultsPage vr = new ViewResultsPage(store);
                    //await Navigation.PushAsync(vr);
                }
                if (!_GameInProgress)
                {
                    // If the user ran out of time, stop the timer, show
                    // a MessageBox, and fill in the answers.
                    //timer1.Stop();

                    RecordStats();
                    //MessageBox.Show("You finished. Here are your results:");

                    //Open up View Results
                    // FinalStats();
                    //ViewResultsPage vr = new ViewResultsPage(store);
                    //await Navigation.PushAsync(vr);
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
                points += Reward();
                problemsDone++;
                // display
                //1. Score
                Score = points.ToString();
                //3.Timer

                //6.problems left
                Problems = problemsDone.ToString();
                Level = level.ToString();
                AnswerBox = "";
                //Generate next problem
                NextQuestion();
                Duration = "00:00:20";
                time = 20;
                value = 1.0;
                UpdateUI();
                //button clicked
            }
            else
            {
                //Game Over
                _GameInProgress = false;
                _stopwatch.Stop();
                RecordStats();
            }

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
        public string Level
        {
            get { return level; }
            set
            {
                level = value;
                OnPropertyChanged("Level");
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
        private void Setup()
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

            // Display number of problems left
            /*
                1. userScore
                3.  Timer
                6.problems left

             
             */

            //3.Timer
            Duration = (time).ToString(@"hh\:mm\:ss");
            //6.problems left
            Level = level;
            Problems = problemsDone.ToString();

            // Start the timer.
            _GameInProgress = true;
            _stopwatch.Start();
            StartTimer();
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
        async void RecordStats()
        {


            string totalTime = TimeSpan.FromSeconds(_stopwatch.Elapsed.TotalSeconds).ToString(@"hh\:mm\:ss");
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            RecordFiniteGame game = new RecordFiniteGame
            {

                Date = date,
                Map = operation,
                Level = Convert.ToInt32(Level),
                Score = points,
                Problems = problemsDone,
                TotalTime = totalTime,
            };
            await App.FiniteGameDatabase.SaveGameAsync(game);
            SaveStats();


            Navigation.ShowPopup(new MyPopup(game));

        }

        public void SaveStats()
        {
            string key = "highscore" + "_" + operation + "_CompetitiveMode";
            string key2 = "MostProblems" + "_" + operation + "_CompetitiveMode";
            string key3 = "HighestLevel" + "_" + operation + "_" + "CompetitiveMode";
            //string key4 = "BestMap" + "_" + operation + "_" + "InfiniteMode";
            int oldCount = Preferences.Get(key2, 0);
            if (problemsDone > oldCount)
            {
                Preferences.Set(key2, problemsDone);
            }
            int oldScore = Preferences.Get(key, 0);
            if (Convert.ToInt32(Score) > oldScore)
            {
                Preferences.Set(key, score);
            }
            int oldLevel = Preferences.Get(key3, 0);
            if (Convert.ToInt32(Level) > oldLevel)
            {
                Preferences.Set(key, level);
            }
        }
        public void UpdateUI()
        {
            
            OnPropertyChanged("Score");
            OnPropertyChanged("Duration");
            OnPropertyChanged("Problems");
            OnPropertyChanged("ProgressBar");
            //OnPropertyChanged("Operand");
            OnPropertyChanged("Num1");
            OnPropertyChanged("Num2");
            OnPropertyChanged("Operand");
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
            reward += Trigger() + bonus;
            return reward;
        }

        public int Trigger()
        {
            int trigger = 0;
            int answerSpeed = 20 - Convert.ToInt32(time);
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
    }
}
