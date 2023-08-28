using Azure;
using Splash_Screen.Models;
using Splash_Screen.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using Microsoft.IdentityModel.Tokens;

namespace Splash_Screen.ViewModels
{
    public class PracticeGameViewModel:BaseViewModel
    {
        public ICommand SubmitButtonCommand { protected set; get; }
        public ICommand SkippedButtonCommand { protected set; get; }
        public ICommand ReviewButtonCommand { protected set; get; }
        public INavigation Navigation { get; set; }
        public ICommand BackCommand { protected set; get; }

        private int incorrect, correct, problems, skipped, streak;
        private string num1, num2, operand, answerbox, response;
        private string operation;
        private int state = 0;
        private int range1, range2;
        ReviewModel record;
        List<ReviewModel> ReviewList = new List<ReviewModel>();
        Random randomizer = new Random();
        private string right = "Right", wrong = "Wrong";

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
        public PracticeGameViewModel(PracticeGame note)
        {

            operation = note.Operation;
            range1 = note.Range1;
            range2 = note.Range2;
            Reset();
            Setup();
            SkippedButtonCommand = new Command(Skip);
            ReviewButtonCommand = new Command(Review);
            SubmitButtonCommand = new Command(Submit);
            BackCommand = new Command(Back);

        }
        
        public int Incorrect
        {
            get { return incorrect; }
            set
            {
                incorrect = value;
                OnPropertyChanged("Incorrect");
            }
        }
        public int Correct
        {
            get { return correct; }
            set
            {
                correct = value;
                OnPropertyChanged("Correct");
            }
        }
        public int Skipped
        {
            get { return skipped; }
            set
            {
                skipped = value;
                OnPropertyChanged("Skipped");
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
        public int Problems
        {
            get { return problems; }
            set
            {
                problems = value;
                OnPropertyChanged("Problems");
            }
        }
        public string Response
        {
            get { return response; }
            set
            {
                response = value;
                OnPropertyChanged("Response");
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
       
        
        private async void Skip()
        {
            state = 1;
            Skipped++;
            NextQuestion();
        }
        private async void Review()
        {
            await Navigation.PushAsync(new ReviewPage(ReviewList));

        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        private async void Submit()
        {
            if (!AnswerBox.IsNullOrEmpty() && CheckAnswer())
            {

                Problems++;
                Correct++;
                Streak++;
                state = 1;

                string problem = Num1 + " " + Operand + " " + Num2;
                string answer = GetAnswer().ToString();
                string userAnswer = AnswerBox;
                ReviewList.Add(new ReviewModel(problem, answer, true, userAnswer));
                AnswerBox = "";
                 Response = right;
                //ResponseLabel.TextColor = Color.LawnGreen;
                await Task.Delay(1500);
                NextQuestion();

            }
            else
            {


                if (state == 1)
                {
                    Streak = 0;
                    Incorrect++;
                    
                    string problem = Num1 + " " + Operand + " " + Num2;
                    string answer = GetAnswer().ToString();
                    string userAnswer = AnswerBox;

                    Review store = new Review();
                    ReviewList.Add(new ReviewModel(problem, answer, false, userAnswer));
                    //record.UserAnswer.Add(AnswerBox.Text);
                    //record.CorrectAnswer.Add(GetAnswer().ToString());
                    store.Equation.Add(problem + " = " + GetAnswer().ToString() + "; NOT: " + AnswerBox);
                    state = 2;
                    AnswerBox = "";

                }
                Response = wrong;
                //ResponseLabel.TextColor = Color.Red;

            }

        }
        private void Reset()
        {
            //DISPLAY
            //VARIABLES
            Problems = 0;
            Correct = 0;
            Incorrect = 0;
            Skipped = 0;
            streak = 0;
            // // Display number of problems left
            Problems = 0;
            //2. Answer Streak
            Streak = 0;
            //5.Correct problems
            Correct = 0;
            //6.problems left
            Skipped = 0;
            //7.incorrect problems
            Incorrect = 0;

            Response = "";

            AnswerBox = "";


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
            state = 1;
            Response = "--------";
            //ResponseLabel.TextColor = Color.Black;
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
            Response = "--------";
            //ResponseLabel.TextColor = Color.Black;
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
