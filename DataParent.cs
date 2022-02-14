using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Example_IDataErrorInfo
{
    public class DataParents : ObservableCollection<DataParent>
    {

    }

    public class DataParent : INotifyPropertyChanged, IDataErrorInfo
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region IDataErrorInfo
        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case nameof(ParentName):
                        if (string.IsNullOrEmpty(ParentName))
                        {
                            error = "Parent name cannot be empty";
                        }
                        break;
                    case nameof(ParentAge):
                        if (ParentAge <= 0)
                        {
                            error = "Parent's age must be > 0";
                        }
                        break;
                }

                return error;
            }
        }
        #endregion
        internal struct Parent
        {
            internal string ParentName { get; set; }
            internal int ParentAge { get; set; }
            internal ObservableCollection<DataChildren> Children { get; set; }
        }

        Parent current;

        public string ParentName
        {
            get { return current.ParentName; }
            set
            {
                if (current.ParentName != value)
                {
                    current.ParentName = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ParentAge
        {
            get { return current.ParentAge; }
            set
            {
                if (current.ParentAge != value)
                {
                    current.ParentAge = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<DataChildren> Children
        {
            get { return current.Children; }
            set
            {
                if(current.Children != value)
                {
                    current.Children = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
