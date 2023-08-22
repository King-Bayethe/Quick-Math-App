using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickMathApp.ViewModels;
using QuickMathApp.ViewModels.Variable;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickMathApp.Views.Mode.PM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class YesTimed : ContentPage
	{
		
        
        
        
        // Create a Random object called randomizer 
        // to generate random numbers.
        Random randomizer = new Random();
        private int totalProblems;//amount of problems
        private int problemsDone;//problems completed
        private int problemsLeft;// problems left
        private int problemsgotWrong;
        private int problemsgotRight;
        private int range1;//Ranges
        private int range2;//Ranges
        private int operation;//Operation
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
        
        public YesTimed ()
		{
            BindingContext = new YesTimedViewModel();
            BindingContext = new KeypadViewModel();
			InitializeComponent ();
            StartTheQuiz();
            
            
            
		}

        private void resultsButton_Clicked(object sender, EventArgs e)
        {

        }

        public void StartTheQuiz()
        {
            PracticeModeViewModel viewModel = new PracticeModeViewModel();
            //Fill Variables
            problemsDone = 0;
            totalProblems = viewModel.TotalProblems;
            problemsLeft = totalProblems;
            range1 = viewModel.Range1;
            range2 = viewModel.Range2;
            operation = viewModel.Operation;
            // Display number of problems left
            problemsLeftlabel.Text = "+ " + problemsgotRight + "/ " + "-" + problemsgotWrong;
            // Convert the two randomly generated numbers
            // into strings so that they can be displayed
            // in the label controls.
            // operation = randomizer.Next(1, 8);

            switch (operation)
            {

                case 0:
                    // Fill in the addition problem.
                    // Generate two random numbers to add.
                    // Store the values in the variables 'addend1' and 'addend2'.
                    addend1 = randomizer.Next(viewModel.Range2);
                    addend2 = randomizer.Next(viewModel.Range2);
                    //Display
                    Num1.Text = addend1.ToString();
                    Num2.Text = addend2.ToString();
                    Operation.Text = "+";

                    break;
                case 1:
                    // Fill in the subtraction problem.
                    minuend = randomizer.Next(viewModel.Range1, viewModel.Range2);
                    subtrahend = randomizer.Next(viewModel.Range1, minuend);
                    //Display
                    Num1.Text = minuend.ToString();
                    Num2.Text = subtrahend.ToString();
                    Operation.Text = "-";
                    break;
                case 2:
                    // Fill in the multiplication problem.
                    multiplicand = randomizer.Next(range1, range2);
                    multiplier = randomizer.Next(range1, range2);
                    //Display
                    Num1.Text = multiplicand.ToString();
                    Num2.Text = multiplier.ToString();
                    Operation.Text = "*";
                    break;

                case 3:
                    // Fill in the division problem.
                    
                    dividend = randomizer.Next(range1, range2);
                    int temporarydivisor = randomizer.Next(range1,range2);
                    
                    if(temporarydivisor == 0)
                    {
                        while (temporarydivisor == 0)
                        {
                            temporarydivisor = randomizer.Next(range1, range2);
                                
                        }
                                

                    }
                    
                    divisor = temporarydivisor;
                    //Display
                    Num1.Text = dividend.ToString();
                    Num2.Text = divisor.ToString();
                    Operation.Text = "/";
                    break;
                case 4:
                    // Fill in the square problem.
                    square = randomizer.Next(viewModel.Range1, viewModel.Range2);
                    //Display
                    Num2.Text = square.ToString() + "^2";
                    Num1.Text = "";
                    Operation.Text = "";
                    break;
                case 5:
                    // Fill in the cube problem.
                    cube = randomizer.Next(range1, range2);
                    //Display
                    Num2.Text = cube.ToString() + "^3";
                    Num1.Text = "";
                    Operation.Text = "";
                    break;

                case 6:
                    // Fill in the square root problem.
                    sqrt = randomizer.Next(range1, range2);
                    //Display
                    Num2.Text = "√" + sqrt.ToString();
                    Num1.Text = "";
                    Operation.Text = "";
                    break;
                case 7:
                    // Fill in the cube root problem.   
                    cbrt = randomizer.Next(range1, range2);
                    //Display
                    Num2.Text = "∛" + cbrt.ToString();
                    Num1.Text = "";
                    Operation.Text = "";
                    break;
                case 8:
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;

            }


            //Submit button
            //AcceptButton = submitButton;

            

            //Timer
            //hoursLeft = MyAppManager.VariableInstance2.Hours;
            //minutesLeft = MyAppManager.VariableInstance2.Minutes;
            //secondsLeft = MyAppManager.VariableInstance2.Seconds;

            //Hours.Text = hoursLeft.ToString();
            //Minutes.Text = minutesLeft.ToString();
            //Seconds.Text = secondsLeft.ToString();

            //Array.Clear(MyAppManager.VariableInstance2.MyProblems, 0, MyAppManager.VariableInstance2.MyProblems.Length);
            //Array.Clear(MyAppManager.VariableInstance2.CorrectAnswers, 0, MyAppManager.VariableInstance2.CorrectAnswers.Length);
            //Array.Clear(MyAppManager.VariableInstance2.MyAnswers, 0, MyAppManager.VariableInstance2.MyAnswers.Length);



            // Start the timer.
            BindingContext = new YesTimedViewModel();
        }

        

        private async void viewResultsTP_Click(object sender, EventArgs e)
        {
            //Open up View Results
            ViewResultsPage vr = new ViewResultsPage();
            await Navigation.PushAsync(vr);






            


        }

        
        public bool CheckAnswer()
        {

            switch (operation)
            {

                case 0:
                    if (addend1 + addend2 == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;

                case 1:
                    if (minuend - subtrahend == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;

                case 2:
                    if (multiplicand * multiplier == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;

                case 3:
                    if (dividend / divisor == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;
                case 4:
                    if ((int)Math.Pow(square, 2) == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;
                case 5:

                    if ((int)Math.Pow(cube, 3) == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;


                case 6:
                    if ((int)Math.Sqrt((int)Math.Pow(sqrt, 2)) == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;
                case 7:
                    if ((int)Math.Ceiling(Math.Pow(Math.Pow(cbrt, 3), (int)1 / 3)) == Convert.ToInt32(AnswerBox.Text))
                        return true;
                    else
                        return false;
                case 8:
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

                case 0:

                    addend1 = randomizer.Next(range1, range2);
                    addend2 = randomizer.Next(range1, range2);
                    Num1.Text = addend1.ToString();
                    Num2.Text = addend2.ToString();
                    Operation.Text = "+";
                    break;
                case 1:

                    //Generate next problem
                    minuend = randomizer.Next(range1, range2);
                    subtrahend = randomizer.Next(range1, range2);
                    Num1.Text = minuend.ToString();
                    Num2.Text = subtrahend.ToString();
                    Operation.Text = "-";
                    break;
                case 2:

                    //Generate next problem
                    multiplicand = randomizer.Next(range1, range2);
                    multiplier = randomizer.Next(range1, range2);
                    Num1.Text = multiplicand.ToString();
                    Num2.Text = multiplier.ToString();
                    break;

                case 3:
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
                    Num1.Text = dividend.ToString();
                    Num2.Text = divisor.ToString();
                    Operation.Text = "/";
                    
                    break;
                case 4:
                    //Generate next problem
                    square = randomizer.Next(range1, range2);
                    Num2.Text = square.ToString() + "^2";
                    Num1.Text = "";
                    Operation.Text = "";
                    break;
                case 5:
                    //Generate next problem
                    cube = randomizer.Next(range1, range2);
                    Num2.Text = cube.ToString() + "^3";
                    Num1.Text = "";
                    Operation.Text = "";
                    break;

                case 6:
                    //Generate next problem
                    sqrt = randomizer.Next(range1, range2);
                    Num2.Text = "√" + sqrt.ToString();
                    Num1.Text = "";
                    Operation.Text = "";
                    break;
                case 7:
                    //Generate next problem
                    cbrt = randomizer.Next(range1, range2);
                    Num2.Text = "∛" + cbrt.ToString();
                    Num1.Text = "";
                    Operation.Text = "";
                    break;
                case 8:
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

                case 0:
                    return (addend1 + addend2);

                case 1:

                    return (minuend - subtrahend);

                case 2:
                    return (multiplicand * multiplier);



                case 3:
                    return (dividend / divisor);

                case 4:
                    return ((int)Math.Pow(square, 2));


                case 5:

                    return ((int)Math.Pow(cube, 3));

                case 6:
                    return ((int)Math.Sqrt((int)Math.Pow(sqrt, 2)));

                case 7:
                    return ((int)Math.Ceiling(Math.Pow(Math.Pow(cbrt, 3), (int)1 / 3)));


                default:
                    Console.WriteLine("Default case");
                    return 0;

            }
        }

        private void SubmitButton_Clicked(object sender, EventArgs e)
        {
            //increment/decrement
            problemsDone++;
            problemsLeft--;
            // display
            YesTimedViewModel viewModel2 = new YesTimedViewModel();
            viewModel2.ProblemsDone = problemsDone;
            viewModel2.ProblemsLeft = problemsLeft;
            if (problemsDone == totalProblems)
            {
                problemsLeftlabel.Text = "Done";
                viewModel2.TimerRunning = false;

                //MessageBox.Show("Congratulations! You answer them all! Here are your results:");
                //open results
            }

            if (CheckAnswer())
            {
                problemsgotRight++;
            }
            else
            {
                problemsgotWrong++;
            }
            //AnswerBox.Text;
            AnswerBox.Focus();
            // Record
            Record();
            //Generate next problem
            NextQuestion();
            //button clicked
        }

        private void Quitbutton1_Click(object sender, EventArgs e)
        {

            //open view results
        }
        public void Record()
        {
            MyAppManager.VariableInstance.MyProblems[problemsDone] = Num1.Text + " " + Operation.Text + " " + Num2.Text + "  = ";
            MyAppManager.VariableInstance.CorrectAnswers[problemsDone] = GetAnswer();
            MyAppManager.VariableInstance.MyAnswers[problemsDone] = Convert.ToInt32(AnswerBox.Text);
            AnswerBox.Focus();   // Moves input focus to input box, so user doesn't have to click it.
            //problemsLeft++;
            problemsLeftlabel.Text = "+ " + problemsgotRight + "/ " + "-" + problemsgotWrong;

            //AnswerBox.Text = "";
            AnswerBox.Focus();
        }
    }
}