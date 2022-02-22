# Example-IDataErrorInfo
Demonstrates IDataErrorInfo implementation using DataGrid and TextBox binding.


The file DataProducts.cs defines

	public class DataProduct : INotifyPropertyChanged, IDataErrorInfo

InotifyPropertyChanged provides the PropertyChanged event to provide the binding interface between class DataProduct and WPF.
 

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

The following structure implementation makes later additions to the data class, such as IEditableObject.
By declaring the structure and fields as internal hides the complexity and distraction.

        internal struct Inventory
        { 
            internal string Product { get; set; }
            internal int Stock { get; set; }            
        }

Then a properties set calls OnPropertyChanged() to update the WPF view.

        public string Product
        {
            get { return current.Product; }
            set
            {
                current.Product = value;
                OnPropertyChanged();                
            }
        }

## IDataErrorInfo Implemention

Error Gets an error message indicating what is wrong with this object.
Intended to represent non-property specific validation errors associated with the class. This property is of little value in WPF usage.

    public string Error => string.Empty;

Item[string] Gets the error message for the property with the given name.


