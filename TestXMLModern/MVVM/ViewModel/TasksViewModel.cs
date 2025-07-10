using System.Collections.ObjectModel;
using TestXMLModern.Core;
using TestXMLModern.MVVM.Model;

namespace TestXMLModern.MVVM.ViewModel
{
    public class TasksViewModel : ObservableObject
    {
        public ObservableCollection<TaskItem> Tasks { get; set; }

        public RelayCommand AddTaskCommand { get; set; }
        public RelayCommand DeleteTaskCommand { get; set; }

        private string _newTaskDescription;
        public string NewTaskDescription
        {
            get { return _newTaskDescription; }
            set { _newTaskDescription = value; OnPropertyChanged(); }
        }

        public TasksViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>();
            Tasks.Add(new TaskItem { Description = "Create the main window", IsCompleted = true });
            Tasks.Add(new TaskItem { Description = "Add a tasks view", IsCompleted = false });
            Tasks.Add(new TaskItem { Description = "Learn WPF MVVM", IsCompleted = false });

            AddTaskCommand = new RelayCommand(o =>
            {
                if (!string.IsNullOrWhiteSpace(NewTaskDescription))
                {
                    Tasks.Add(new TaskItem { Description = NewTaskDescription });
                    NewTaskDescription = string.Empty;
                }
            });

            DeleteTaskCommand = new RelayCommand(task =>
            {
                if (task is TaskItem taskItem)
                {
                    Tasks.Remove(taskItem);
                }
            });
        }
    }
}