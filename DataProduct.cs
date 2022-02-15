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
        [Description("Gets an error message indicating what is wrong with this object.")]
        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case nameof(Product):
                        if (string.IsNullOrEmpty(Product))
                        {
                            error = "Product name cannot be empty";
                        }
                        break;

                    case nameof(Stock):
                        if (Stock <= 0)
                        {
                            error = "Stock <= 0 !";
                        }
                        break;
                }

                return error;
            }
        }
        #endregion

        internal struct Inventory
        { // Boilerplate ready to implement IEditableObject interface if required
            internal string Product { get; set; }
            internal int Stock { get; set; }            
        }

        Inventory current;

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
