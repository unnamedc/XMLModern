using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using TestXMLModern.Core;
using TestXMLModern.MVVM.Model;

namespace TestXMLModern.MVVM.ViewModel
{
    public class TimerViewModel : ObservableObject
    {
        private DispatcherTimer _timer;
        private TimeSpan _time;
        private TimeSpan _initialDuration;

        public ObservableCollection<TimerSession> SessionHistory { get; set; }

        private string _timeDisplay;
        public string TimeDisplay
        {
            get => _timeDisplay;
            set { _timeDisplay = value; OnPropertyChanged(); }
        }

        private double _selectedMinutes = 25;
        public double SelectedMinutes
        {
            get => _selectedMinutes;
            set
            {
                _selectedMinutes = Math.Round(value);
                OnPropertyChanged();
                ResetTimer(null);
            }
        }

        public RelayCommand StartCommand { get; set; }
        public RelayCommand StopCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }

        public TimerViewModel()
        {
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += Timer_Tick;

            SessionHistory = new ObservableCollection<TimerSession>();

            StartCommand = new RelayCommand(o =>
            {
                _initialDuration = TimeSpan.FromMinutes(SelectedMinutes);
                _timer.Start();
            });
            StopCommand = new RelayCommand(o =>
            {
                _timer.Stop();
                SaveSession();
            });
            ResetCommand = new RelayCommand(ResetTimer);

            ResetTimer(null);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_time <= TimeSpan.Zero)
            {
                _time = TimeSpan.Zero;
                StopCommand.Execute(null);
            }
            else
            {
                _time = _time.Add(TimeSpan.FromSeconds(-1));
                // Исправлено: Отображаем часы, если время больше или равно одному часу
                TimeDisplay = _time.TotalHours >= 1 ? _time.ToString(@"hh\:mm\:ss") : _time.ToString(@"mm\:ss");
            }
        }

        private void ResetTimer(object obj)
        {
            _timer.Stop();
            _time = TimeSpan.FromMinutes(SelectedMinutes);
            // Исправлено: Отображаем часы, если время больше или равно одному часу
            TimeDisplay = _time.TotalHours >= 1 ? _time.ToString(@"hh\:mm\:ss") : _time.ToString(@"mm\:ss");
        }

        private void SaveSession()
        {
            int elapsedMinutes = (int)Math.Ceiling((_initialDuration - _time).TotalMinutes);

            if (elapsedMinutes > 0)
            {
                var session = new TimerSession
                {
                    Date = DateTime.Now,
                    DurationMinutes = elapsedMinutes
                };
                SessionHistory.Insert(0, session);
            }
        }
    }
}
