using Azure;
using Splash_Screen.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Splash_Screen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PracticeGamePage : ContentPage
    {
        public PracticeGamePage(PracticeGameModel model)
        {
            InitializeComponent();
            operation = model.Operation;
            range1 = model.Range1;  
            range2 = model.Range2;
           /* Reset();
            Setup();*/
        }

        private string operation;
        private int state = 0;
        private int range1, range2;
        ReviewModel record;
        List<ReviewModel> ReviewList = new List<ReviewModel>();
        Random randomizer = new Random();
        private string right = "Right", wrong = "Wrong";

        //RUNNING VARIABLES
        private int problemsDone;//problems completed
        private int problemsgotWrong;
        private int problemsSkipped;
        private int problemsgotRight;
        private int streak;
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
            //DISPLAY
            //VARIABLES
            problemsDone = 0;
            problemsgotRight = 0;
            problemsgotWrong = 0;
            problemsSkipped = 0;
            streak = 0;
            // // Display number of problems left
            ProblemsDoneLabel.Text = problemsDone.ToString();
            //2. Answer Streak
            StreakLabel.Text = "0";
            //5.Correct problems
            ProblemsCorrectLabel.Text = "0";
            //6.problems left
            //7.incorrect problems
            ProblemsIncorrectLabel.Text = "0";
            

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
                    //num1.Visible = false;
                    //Operation.Visible = false;
                    break;
                case "Cubed":
                    // Fill in the cube problem.
                    cube = randomizer.Next(range1, range2);
                    //Display
                    num2.Text = cube.ToString() + "^3";
                    //num1.Visible = false;
                    //Operation.Visible = false;
                    break;

                case "Square Root":
                    // Fill in the square root problem.
                    sqrt = randomizer.Next(range1, range2);
                    //Display
                    num2.Text = "√" + sqrt.ToString();
                    //num1.Visible = false;
                    //Operation.Visible = false;
                    break;
                case "Cube Root":
                    // Fill in the cube root problem.
                    cbrt = randomizer.Next(range1, range2);
                    //Display
                    num2.Text = "∛" + cbrt.ToString();
                    //num1.Visible = false;
                    //Operation.Visible = false;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;

            }
            state = 1;
            ResponseLabel.Text = "--------";
            ResponseLabel.TextColor = Color.Black;
        }

        async void SubmitButton_Clicked(object sender, EventArgs e)
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
                
                problemsDone++;
                problemsgotRight++;
                streak++;
                state = 1;
                // // Display number of problems left
                ProblemsDoneLabel.Text = problemsDone.ToString();
                //2. Answer Streak
                StreakLabel.Text = streak.ToString();
                //5.Correct problems
                ProblemsCorrectLabel.Text = problemsgotRight.ToString();
                //6.problems left
                //7.incorrect problems
                ProblemsIncorrectLabel.Text = problemsgotWrong.ToString();
                string problem = num1.Text + " " + Operation.Text + " " + num2.Text;
                string answer = GetAnswer().ToString();
                string userAnswer = AnswerBox.Text;
                ReviewList.Add(new ReviewModel(problem, answer, true, userAnswer));
                AnswerBox.Text = "";
                ResponseLabel.Text = right;
                ResponseLabel.TextColor = Color.LawnGreen;
                await Task.Delay(1500);
                NextQuestion();

            }
            else
            {

                
                if (state == 1)
                {
                    streak = 0;
                    problemsgotWrong++;
                    // // Display number of problems left
                    ProblemsDoneLabel.Text = problemsDone.ToString();
                    //2. Answer Streak
                    StreakLabel.Text = streak.ToString();
                    //5.Correct problems
                    ProblemsCorrectLabel.Text = problemsgotRight.ToString();
                    //7.incorrect problems
                    ProblemsIncorrectLabel.Text = problemsgotWrong.ToString();
                    string problem = num1.Text + " " + Operation.Text + " " + num2.Text;
                    string answer = GetAnswer().ToString();
                    string userAnswer = AnswerBox.Text;
                    
                    Review store = new Review();
                    ReviewList.Add(new ReviewModel(problem,answer,false,userAnswer));
                    //record.UserAnswer.Add(AnswerBox.Text);
                    //record.CorrectAnswer.Add(GetAnswer().ToString());
                    store.Equation.Add(problem + " = " + GetAnswer().ToString() + "; NOT: " + AnswerBox.Text);
                    state = 2;
                    AnswerBox.Text = "";

                }
                ResponseLabel.Text = wrong;
                ResponseLabel.TextColor = Color.Red;

            }
            

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
        async void ReviewButton_Clicked(object sender, EventArgs args)
        {
         //   record.ProblemCount = problemsDone;
            await Navigation.PushAsync(new ReviewPage(ReviewList));
        }
        async void SkipButton_Clicked(object sender, EventArgs args)
        {
            state = 1;
            problemsSkipped++;
            ProblemsSkippedLabel.Text = problemsSkipped.ToString();
            int answer = GetAnswer();
            await DisplayAlert("Correct Answer:",num1.Text + " " + Operation.Text + " " + num2.Text + " = " + answer, "Next Question");
            NextQuestion();
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

            ResponseLabel.Text = "--------";
            ResponseLabel.TextColor = Color.Black;
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