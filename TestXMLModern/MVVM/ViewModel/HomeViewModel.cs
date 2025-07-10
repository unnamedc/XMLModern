using System;
using System.Windows.Threading;
using TestXMLModern.Core; // Changed from TestXMLModern.MVVM.Core

namespace TestXMLModern.MVVM.ViewModel
{
    public class HomeViewModel : ObservableObject
    {
        // Timer Properties
        private DispatcherTimer _timer;
        private TimeSpan _time;

        private string _timeDisplay;
        public string TimeDisplay
        {
            get { return _timeDisplay; }
            set { _timeDisplay = value; OnPropertyChanged(); }
        }

        public RelayCommand StartTimerCommand { get; set; }
        public RelayCommand PauseTimerCommand { get; set; }
        public RelayCommand ResetTimerCommand { get; set; }

        public HomeViewModel()
        {
            // Initialize Timer
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            ResetTimer(null);

            StartTimerCommand = new RelayCommand(o => _timer.Start());
            PauseTimerCommand = new RelayCommand(o => _timer.Stop());
            ResetTimerCommand = new RelayCommand(ResetTimer);
        }

        // Timer Methods
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_time > TimeSpan.Zero)
            {
                _time = _time.Add(TimeSpan.FromSeconds(-1));
                TimeDisplay = _time.ToString(@"mm\:ss");
            }
            else
            {
                _timer.Stop();
            }
        }

        private void ResetTimer(object obj)
        {
            _timer.Stop();
            _time = TimeSpan.FromMinutes(25); // Pomodoro standard
            TimeDisplay = _time.ToString(@"mm\:ss");
        }
    }
}