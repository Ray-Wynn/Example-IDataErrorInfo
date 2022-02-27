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

	Inventory current; // internal struct of backing field of properties

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

## XAML
DataGrid "DataGridProducts" is bound to the DataContext which is assigned to DataProducts, an ObservableCollection of DataProduct.

TextBoxValidationError style and two templates, RowValidationError & ValidationError are implemented in <Window.Resources> to provide customised user interface (UI) error reporting.

### Binding properties

	<DataGrid Name="DataGridProducts" Grid.Column="0" Grid.ColumnSpan="2" Grid.Row="2" AutoGenerateColumns="False" CanUserAddRows="True" 
                  ItemsSource="{Binding}" 
                  RowValidationErrorTemplate="{StaticResource RowValidationError}"
                  RowDetailsTemplate="{StaticResource ValidationError}">
            <DataGrid.Columns>
                <!-- Product -->
                <DataGridTemplateColumn x:Name="NameColumn" Header="Product" MinWidth="100" Width="auto">
                    <DataGridTemplateColumn.CellTemplate>
                        <DataTemplate>
                            <TextBox Text="{Binding Path=Product, 

- **UpdateSourceTrigger**
	
	- **Default**
		- The default UpdateSourceTrigger value of the binding target property. The default value for most dependency properties is PropertyChanged, while the Text property has a default value of LostFocus.

	- **PropertyChanged**
		- Updates the binding source immediately whenever the binding target property changes.

	- **LostFocus**
		- Updates the binding source whenever the binding target element loses focus.

	- **Explicit**
		- Updates the binding source only when you call the UpdateSource() method.			    

                                UpdateSourceTrigger=PropertyChanged,

- **ValidatesOnDataErrors**	

Setting this property provides an alternative to using the DataErrorValidationRule element explicitly. The DataErrorValidationRule is a built-in validation rule that checks for errors that are raised by the IDataErrorInfo implementation of the source object. If an error is raised, the binding engine creates a ValidationError with the error and adds it to the Validation.Errors collection of the bound element. The lack of an error clears this validation feedback, unless another rule raises a validation issue.
				
                                ValidatesOnDataErrors=True, 
				
- **NotifyOnValidationError**
	
If the binding has ValidationRules associated with it, the binding engine checks each rule each time it transfers the target property value to the source property. If a rule invalidates a value, the binding engine creates a ValidationError object and adds it to the Validation.Errors collection of the bound object. When the Validation.Errors property is not empty, the Validation.HasError attached property of the object is set to true. If the NotifyOnValidationError property of the Binding is set to true, then the binding engine raises the Validation.Error attached event on the object.
				
				NotifyOnValidationError=True, 
				
- **SourceUpdated _(Event)_**

Occurs when a value is transferred from the binding target to the binding source, but only for bindings with the NotifyOnSourceUpdated value set to true.
				
                                SourceUpdated="TextBox_SourceUpdated"
				
- **NotifyOnSourceUpdated**

Gets or sets a value that indicates whether to raise the SourceUpdated event when a value is transferred from the binding target to the binding source.	

				NotifyOnSourceUpdated=True, 

- **TargetUpdated _(Event)_**

Occurs when a value is transferred from the binding source to the binding target, but only for bindings with the NotifyOnTargetUpdated value set to true.

				TargetUpdated="TextBox_TargetUpdated"
				
- **NotifyOnTargetUpdated**

Gets or sets a value that indicates whether to raise the TargetUpdated event when a value is transferred from the binding source to the binding target.
				
				NotifyOnTargetUpdated=True}"                                                                 
                                Style="{StaticResource TextBoxValidationError}"/>
                        </DataTemplate>
                    </DataGridTemplateColumn.CellTemplate>
                </DataGridTemplateColumn>
                <!-- Stock -->
                <DataGridTemplateColumn  Header="Stock" MinWidth="100" Width="auto">
                    <DataGridTemplateColumn.CellTemplate >
                        <DataTemplate>
                            <TextBox Text="{Binding Path=Stock, 
			    	UpdateSourceTrigger=PropertyChanged, 
                                ValidatesOnExceptions=True, 
				ValidatesOnDataErrors=True, 
				NotifyOnValidationError=True,
				
				SourceUpdated="TextBox_SourceUpdated"
                                NotifyOnSourceUpdated=True, 
				
				TargetUpdated="TextBox_TargetUpdated"
				NotifyOnTargetUpdated=True}" 
                                                                
                                Style="{StaticResource TextBoxValidationError}"/>
                        </DataTemplate>
                    </DataGridTemplateColumn.CellTemplate>
                </DataGridTemplateColumn>
            </DataGrid.Columns>
        </DataGrid>
	
DataGridProducts SelectedItem property determines TextBox's binding, for example 'Product' binding as shown below.
	
	<TextBox Grid.Column="1" Grid.ColumnSpan="2" Grid.Row="4" 
                 Style="{StaticResource TextBoxValidationError}" 
                 Text="{Binding SelectedItem.Product, ElementName=DataGridProducts, UpdateSourceTrigger=PropertyChanged, ValidatesOnDataErrors=True}"/>
	
	
