# Example-IDataErrorInfo
Demonstrates IDataErrorInfo implementation using DataGrid and TextBox binding.
[Example project](https://github.com/Ray-Wynn/Example-IDataErrorInfo)


The file DataProducts.cs defines the data object DataProduct

	public class DataProduct : INotifyPropertyChanged, IDataErrorInfo

INotifyPropertyChanged provides the PropertyChanged event to provide the binding interface between class DataProduct and WPF.
 

	public event PropertyChangedEventHandler? PropertyChanged;

	protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
	{
	    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

The following structure implementation makes later additions to the data class, such as IEditableObject simple.
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

**Error**: gets an error message indicating what is wrong with this object.

_Intended to represent non-property specific validation errors associated with the class. This property is of little value in WPF usage._

	public string Error => string.Empty;



**Item[string]**: gets the error message for the property with the given name.

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

