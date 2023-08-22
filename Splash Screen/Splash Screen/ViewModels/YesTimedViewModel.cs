using QuickMathApp.ViewModels.Variable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QuickMathApp.ViewModels
{
    public class YesTimedViewModel : INotifyPropertyChanged
    {
        
        //TIME
        Stopwatch stopwatch = new Stopwatch();
        private Timer time = new Timer();
        private bool _timerRunning;

        public bool TimerRunning
        {
            get { return _timerRunning; }
            set
            {
                _timerRunning = value;
            }
        }

        private string _stopWatchHours;
        public string StopWatchHours
        {
            get { return _stopWatchHours; }
            set 
            { 
                  _stopWatchHours = value;
                OnPropertyChanged("StopWatchHours");
            }
        }
        private string _stopWatchMinutes;
        public string StopWatchMinutes
        {
            get { return _stopWatchMinutes; }
            set
            {
                _stopWatchMinutes = value;
                OnPropertyChanged("StopWatchMinutes");
            }
        }

        private string _stopWatchSeconds;
        public string StopWatchSeconds
        {
            get { return _stopWatchSeconds; }
            set
            {
                _stopWatchSeconds = value;
                OnPropertyChanged("StopWatchSeconds");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public YesTimedViewModel(PracticeModeViewModel viewModel)
        {
            int totalProblems = viewModel.TotalProblems;
            //viewModel.ProblemsLeft = totalProblems;
            stopwatch.Start();
            int hoursLeft = viewModel.Hours - stopwatch.Elapsed.Hours;
            int minutesLeft = viewModel.Minutes - stopwatch.Elapsed.Minutes;
            int secondsLeft = viewModel.Seconds - stopwatch.Elapsed.Seconds;
            //Time
            

            //TimeSpan diff = remainingTime.Subtract(stopwatch.Elapsed);
            string fmt = "00";
            StopWatchHours = hoursLeft.ToString(fmt);
            StopWatchMinutes = minutesLeft.ToString(fmt);
            StopWatchSeconds = secondsLeft.ToString(fmt);

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                bool run = true;
                // do something every 60 seconds
                Device.BeginInvokeOnMainThread(() =>
                {
                    // interact with UI elements
                    if (secondsLeft == 0 && minutesLeft == 0 && hoursLeft == 0)
                    {
                        // If the user ran out of time, stop the timer, show
                        // a MessageBox, and fill in the answers.
                        run = false;

                        //MessageBox.Show("Time's up! Sorry, you didn't finish in time. Here are your results:");
                        //open results

                    }
                    else if (!TimerRunning)
                    {
                        // If the user ran out of time, stop the timer, show
                        // a MessageBox, and fill in the answers.
                        
                        run = false;
                        
                        //MessageBox.Show("You finished. Here are your results:");
                        //open view results
                    }

                    else
                    {
                        // Display the new time left
                        // by updating the Time Left label.

                        secondsLeft = secondsLeft - 1;
                        if (hoursLeft != 0 && minutesLeft == 0 && secondsLeft == 0)
                        {
                            secondsLeft = 59;
                            minutesLeft = 59;
                            hoursLeft = hoursLeft - 1;

                            StopWatchHours = hoursLeft.ToString(fmt);
                            StopWatchMinutes = minutesLeft.ToString(fmt);
                            StopWatchSeconds = secondsLeft.ToString(fmt);
                        }
                        if (secondsLeft == 0 && minutesLeft != 0)
                        {
                            secondsLeft = 59;
                            minutesLeft = minutesLeft - 1;

                            StopWatchHours = hoursLeft.ToString(fmt);
                            StopWatchMinutes = minutesLeft.ToString(fmt);
                            StopWatchSeconds = secondsLeft.ToString(fmt);
                        }
                        

                        StopWatchHours = hoursLeft.ToString(fmt);
                        StopWatchMinutes = minutesLeft.ToString(fmt);
                        StopWatchSeconds = secondsLeft.ToString(fmt);

                    }
                });
                
                
                StopWatchHours = hoursLeft.ToString(fmt);
                StopWatchMinutes = minutesLeft.ToString(fmt);
                StopWatchSeconds = secondsLeft.ToString(fmt);
                //var theTimerProgress = text;
                return run;
            });

            

        }


        private string _problemsGotWrong;
        public string ProblemsGotWrong
        {
            get { return _problemsGotWrong; }
            set
            {
                _problemsGotWrong = value;
                OnPropertyChanged("ProblemsGotWrong");
            }
        }
        private string _problemsGotRight;
        public string ProblemsGotRight
        {
            get { return _problemsGotRight; }
            set
            {
                _problemsGotRight = value;
                OnPropertyChanged("ProblemsGotRight");
            }
        }

        

        
        


    }
}
