using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Example_IDataErrorInfo
{
    public class DataChildren : INotifyPropertyChanged, IDataErrorInfo
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
                    case nameof(ChildName):
                        if (string.IsNullOrEmpty(ChildName))
                        {
                            error = "Child name cannot be empty";
                        }
                        break;
                    case nameof(ChildAge):
                        if(ChildAge <= 0)
                        {
                            error = "Child's age must be > 0";
                        }
                        break;
                }

                return error;
            }
        }
        #endregion
        internal struct Child
        {
            internal string ChildName { get; set; }
            internal int ChildAge { get; set; }
        }

        Child current;

        public string ChildName
        {
            get { return current.ChildName; }
            set
            {
                if (current.ChildName != value)
                {
                    current.ChildName = value;                    
                    OnPropertyChanged();
                }
            }
        }
       
        public int ChildAge
        {
            get { return current.ChildAge; }
            set
            {
                if (current.ChildAge != value)
                {
                    current.ChildAge = value;                    
                    OnPropertyChanged();
                }
            }
        }        
    }
}
