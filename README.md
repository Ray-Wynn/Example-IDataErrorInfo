# Example-IDataErrorInfo
Demonstrates IDataErrorInfo implementation using DataGrid and TextBox binding.


The file DataProducts.cs defines

	public class DataProduct : INotifyPropertyChanged, IDataErrorInfo

INotifyPropertyChanged provides the PropertyChanged event to provide the binding interface between class DataProduct and WPF.
 

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

**Error** Gets an error message indicating what is wrong with this object.

    public string Error => string.Empty;
_Intended to represent non-property specific validation errors associated with the class. This property is of little value in WPF usage._

**Item[string]** Gets the error message for the property with the given name.

	public string this[string propertyName]
        {
            get
            {
                string error = string.Empty;

                switch (propertyName)
                {
                    case nameof(Product):
                        if (string.IsNullOrEmpty(Product))
                        {
                            error = "Product name required.";
                            break;
                        }
                        else
                        {
                            if(Product.Length < 3)
                            {
                                error = "Product name < 3 characters long.";
                            }
                        }
                        break;

                    case nameof(Stock):
                        if (Stock <= 0)
                        {
                            error = "Stock <= 0!";
                        }
                        break;
                }

                return error;
            }
        }

