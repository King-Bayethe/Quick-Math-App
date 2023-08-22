using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickMathApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickMathApp.Views.Mode.PM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class YesTimedPage : ContentPage
	{
        // Create a Random object called randomizer 
        // to generate random numbers.
        Random randomizer = new Random();
        PracticeModeViewModel viewModel;
        Record store;
        public int modeState = 1;
        private int totalProblems;//amount of problems
        private int _duration = 0;
        private int totalTime = 0;
        private bool _GameInProgress;
        private int problemsDone;//problems completed
        private int problemsLeft;// problems left
        private int streak;
        private int problemsgotWrong;
        private int problemsgotRight;
        private int range1;//Ranges
        private int range2;//Ranges
        private string operation;//Operation
        // These integer variables store the numbers 
        // for the addition problem. 
        int addend1;
        int addend2;

        // These integer variables store the numbers 
        // for the subtraction problem. 
        int minuend;
        int subtrahend;

        // These integer variables store the numbers 
        // for the multiplication problem. 
        int multiplicand;
        int multiplier;

        // These integer variables store the numbers 
        // for the division problem. 
        int dividend;
        int divisor;

        // These integer variables store the numbers 
        // for the square problem. 
        double square;
        //int divisor;

        // These integer variables store the numbers 
        // for the sqrt problem. 
        double sqrt;
        //int divisor;

        // These integer variables store the numbers 
        // for the cube problem. 
        int cube;
        //int divisor;

        // These integer variables store the numbers 
        // for the cbrt problem. 
        double cbrt;

        public YesTimedPage (PracticeModeViewModel view)
		{
            
            InitializeComponent();
            viewModel = view;
            StartTheQuiz();
        }

        public void StartTheQuiz()
        {
            
            //Fill Variables
            
            totalProblems = viewModel.TotalProblems;
            problemsLeft = totalProblems;
            range1 = viewModel.Range1;
            range2 = viewModel.Range2;
            operation += viewModel.Operation;
            _duration = viewModel.Seconds + viewModel.Minutes*60 + viewModel.Hours * 3600;
            totalTime = _duration;
            // Convert the two randomly generated numbers
            // into strings so that they can be displayed
            // in the label controls.

            switch (operation)
            {

                case "Addition":
                    // Fill in the addition problem.
                    // Generate two random numbers to add.
                    // Store the values in the variables 'addend1' and 'addend2'.
                    addend1 = randomizer.Next(range2);
                    addend2 = randomizer.Next(range2);
                    //Display
                    num1.Text = addend1.ToString();
                    num2.Text = addend2.ToString();
                    Operation.Text = "+";
                    break;
                case "Subtraction":
                    // Fill in the subtraction problem.
                    minuend = randomizer.Next(range1, range2);
                    subtrahend = randomizer.Next(range1, minuend);
                    //Display
                    num1.Text = minuend.ToString();
                    num2.Text = subtrahend.ToString();
                    Operation.Text = "-";
                    break;
                case "Multiplication":
                    // Fill in the multiplication problem.
                    multiplicand = randomizer.Next(range1, range2);
                    multiplier = randomizer.Next(range1, range2);
                    //Display
                    num1.Text = multiplicand.ToString();
                    num2.Text = multiplier.ToString();
                    Operation.Text = "*";
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
                    break;
                case "Squared":
                    // Fill in the square problem.
                    square = randomizer.Next(range1, range2);
                    //Display
                    num2.Text = square.ToString() + "^2";
                    num1.Text = "";
                    Operation.Text = "";
                    break;
                case "Cubed":
                    // Fill in the cube problem.
                    cube = randomizer.Next(range1, range2);
                    //Display
                    num2.Text = cube.ToString() + "^3";
                    num1.Text = "";
                    Operation.Text = "";
                    break;

                case "Square Root":
                    // Fill in the square root problem.
                    sqrt = randomizer.Next(range1, range2);
                    //Display
                    num2.Text = "√" + sqrt.ToString();
                    num1.Text = "";
                    Operation.Text = "";
                    break;
                case "Cube Root":
                    // Fill in the cube root problem.
                    cbrt = randomizer.Next(range1, range2);
                    //Display
                    num2.Text = "∛" + cbrt.ToString();
                    num1.Text = "";
                    Operation.Text = "";
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;

            }


            
            // // Display number of problems left
            ProblemsLeftLabel.Text = totalProblems.ToString();
            //2. Answer Streak
            StreakLabel.Text = "0";
            //5.Correct problems
            ProblemsCorrectLabel.Text = "0";
            //6.problems left
            //7.incorrect problems
            ProblemsIncorrectLabel.Text = "0";
            problemsDone = 0;
            problemsgotRight = 0;
            problemsgotWrong = 0;
            problemsLeft = totalProblems;
            store = new Record();
            // Start the timer.
            _GameInProgress = true;
            StartTimer();

            //Array.Clear(MyAppManager.VariableInstance2.MyProblems, 0, MyAppManager.VariableInstance2.MyProblems.Length);
            //Array.Clear(MyAppManager.VariableInstance2.CorrectAnswers, 0, MyAppManager.VariableInstance2.CorrectAnswers.Length);
            //Array.Clear(MyAppManager.VariableInstance2.MyAnswers, 0, MyAppManager.VariableInstance2.MyAnswers.Length);



            
        }

        public async void StartTimer()
        {


            // tick every second while game is in progress
            while (_GameInProgress)
            {
                await Task.Delay(1000);
                _duration--;
                string s = TimeSpan.FromSeconds(_duration).ToString(@"hh\:mm\:ss");
                timeLeftLabel.Text = s;
                if (_duration == 0)
                {
                    // If the user ran out of time, stop the timer, show
                    // a MessageBox, and fill in the answers.
                    //timer1.Stop();
                    _GameInProgress = false;
                    //MessageBox.Show("Time's up! Sorry, you didn't finish in time. Here are your results:");

                    //Open up View Results
                    FinalStats();
                    ViewResultsPage vr = new ViewResultsPage(store);
                    await Navigation.PushAsync(vr);
                }
                if (problemsLeft == 0 && problemsDone == totalProblems)
                {
                    // If the user ran out of time, stop the timer, show
                    // a MessageBox, and fill in the answers.
                    //timer1.Stop();
                    _GameInProgress = false;
                    //MessageBox.Show("You finished. Here are your results:");

                    //Open up View Results
                    FinalStats();
                    ViewResultsPage vr = new ViewResultsPage(store);
                    await Navigation.PushAsync(vr);
                }

                
            }

        }

        private async void viewResultsTP_Click(object sender, EventArgs e)
        {
            //Open up View Results
            FinalStats();
            ViewResultsPage vr = new ViewResultsPage(store);
            await Navigation.PushAsync(vr);









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
                    //num1.Text = "";
                    //Operation.Text = "";
                    break;
                case "Cubed":
                    //Generate next problem
                    cube = randomizer.Next(range1, range2);
                    num2.Text = cube.ToString() + "^3";
                    //num1.Text = "";
                    //Operation.Text = "";
                    break;
                case "Square Root":
                    //Generate next problem
                    sqrt = randomizer.Next(range1, range2);
                    num2.Text = "√" + sqrt.ToString();
                    //num1 = false;
                    // Operation.Text = "";
                    break;
                case "Cube Root":
                    //Generate next problem
                    cbrt = randomizer.Next(range1, range2);
                    num2.Text = "∛" + cbrt.ToString();
                    //num1.Text = "";
                    //Operation.Text = "";
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

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            /*
             * 1. enter key to submit//////111
             * 2. store problem ////111
             * 3. store user answer /////111
             * 4. check user answer  ////111
             * 5. store correct answer ==switch///
             * 6. display next problem////111
            */


            /* if (AnswerBox.Text.Equals("") || AnswerBox.Text.Equals(null) || AnswerBox.Text.Length == 0)
             {
                 AnswerBox.Text = "0";
             }*/
            //Check Answer
           // Record
            //Record();

           problemsLeft--;
            problemsDone++;


            //button clicked
            

            if (CheckAnswer())
            {
                problemsgotRight++;
            }
            else
            {
                problemsgotWrong++;
            }
            if (problemsDone == totalProblems)
            {

                //viewModel2.TimerRunning = false;

                //MessageBox.Show("Congratulations! You answer them all! Here are your results:");
                //open results
                FinalStats();
                ViewResultsPage vr = new ViewResultsPage(store);
                await Navigation.PushAsync(vr);
            }
            
            // display
            //2. Answer Streak
            StreakLabel.Text = streak.ToString();
            //5.Correct problems
            ProblemsCorrectLabel.Text = problemsgotRight.ToString();
            //6.problems left
            ProblemsLeftLabel.Text = problemsLeft.ToString();
            //7.incorrect problems
            ProblemsIncorrectLabel.Text = problemsgotWrong.ToString();
            AnswerBox.Text = "";
            
            //Generate next problem
            NextQuestion();
            //button clicked
        }

        public void FinalStats()
        {

            
            store.Streak = streak;
            store.Correct = Convert.ToInt32(ProblemsCorrectLabel.Text);
            store.Incorrect = Convert.ToInt32(ProblemsIncorrectLabel.Text);
            store.Range1 = range1;
            store.Range2 = range2;
            store.ProblemsCompleted = problemsDone;
            store.Operation = viewModel.Operation;
            store.Time = totalTime;
            store.TotalProblems = viewModel.TotalProblems;
            store.Mode = modeState;

        }
        public void Record()
        {
            store.MyProblems[problemsDone] = num1.Text + " " + Operation.Text + " " + num2.Text + "  = ";
            store.CorrectAnswers[problemsDone] = GetAnswer();
            store.MyAnswers[problemsDone] = Convert.ToInt32(AnswerBox.Text);
            
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
    }
}