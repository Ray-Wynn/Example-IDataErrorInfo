using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Example_IDataErrorInfo
{
    public class DataProducts : ObservableCollection<DataProduct>
    {

    }

    public class DataProduct : INotifyPropertyChanged, IDataErrorInfo
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region IDataErrorInfo
        // This property is of little value in WPF usage.
        [Description ("Intended to represent non-property specific validation errors associated with the class."), Category("Miscellanious")]
        public string Error => string.Empty;

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="propertyName">The bound properties name.</param>
        /// <returns>The properties error messaage string.</returns>
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
        #endregion

        internal struct Inventory
        { // Using a structure rather than private fields, then ready to implement IEditableObject interface if required.
            internal string Product { get; set; }
            internal int Stock { get; set; }            
        }

        Inventory current; // internal backing fields of properties

        public string Product
        {
            get { return current.Product; }
            set
            {
                current.Product = value;
                OnPropertyChanged();                
            }
        }

        public int Stock
        {
            get { return current.Stock; }
            set
            {             
                current.Stock = value;
                OnPropertyChanged();             
            }
        }
    }
}
