using TestXMLModern.Core;

namespace TestXMLModern.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        // Свойства и команды для навигации
        public HomeViewModel HomeVM { get; set; }
        public TimerViewModel TimerVM { get; set; }
        public TasksViewModel TasksVM { get; set; }

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand TimerViewCommand { get; set; }
        public RelayCommand TasksViewCommand { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            // Инициализация ViewModel'ов
            HomeVM = new HomeViewModel();
            TimerVM = new TimerViewModel();
            TasksVM = new TasksViewModel();
            CurrentView = HomeVM;

            // Настройка команд
            HomeViewCommand = new RelayCommand(o => CurrentView = HomeVM);
            TimerViewCommand = new RelayCommand(o => CurrentView = TimerVM);
            TasksViewCommand = new RelayCommand(o => CurrentView = TasksVM);
        }
    }
}
