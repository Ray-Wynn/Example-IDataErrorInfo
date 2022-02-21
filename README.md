# Example-IDataErrorInfo
Demonstrates IDataErrorInfo implementation using DataGrid and TextBox binding.


The file DataProducts.cs defines

	public class DataProduct : INotifyPropertyChanged, IDataErrorInfo

InotifyPropertyChanged provides the provides the PropertyChanged event to provide the binding interface between the class DataProduct and WPF.
 

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




