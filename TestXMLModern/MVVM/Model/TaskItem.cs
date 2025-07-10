using TestXMLModern.Core;

namespace TestXMLModern.MVVM.Model
{
    public class TaskItem : ObservableObject
    {
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set { _isCompleted = value; OnPropertyChanged(); }
        }
    }
}