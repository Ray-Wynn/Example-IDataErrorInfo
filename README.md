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

## XAML
DataGrid "DataGridProducts" is bound to the DataContext which is assigned to DataProducts, an ObservableCollection of DataProduct.

TextBoxValidationError style and two templates, RowValidationError & ValidationError are implemented in <Window.Resources> to provide customised user interface (UI) error reporting.

	<DataGrid Name="DataGridProducts" Grid.Column="0" Grid.ColumnSpan="2" Grid.Row="2" AutoGenerateColumns="False" CanUserAddRows="True" 
                  ItemsSource="{Binding}" 
                  RowValidationErrorTemplate="{StaticResource RowValidationError}"
                  RowDetailsTemplate="{StaticResource ValidationError}">
            <DataGrid.Columns>
                <!-- Product -->
                <DataGridTemplateColumn x:Name="NameColumn" Header="Product" MinWidth="100" Width="auto">
                    <DataGridTemplateColumn.CellTemplate>
                        <DataTemplate>
                            <TextBox Text="{Binding Path=Product, UpdateSourceTrigger=PropertyChanged, 
                                     ValidatesOnDataErrors=True, NotifyOnValidationError=True, 
                                     NotifyOnSourceUpdated=True, NotifyOnTargetUpdated=True}" 
                                     SourceUpdated="TextBox_SourceUpdated"
                                     TargetUpdated="TextBox_TargetUpdated"
                                     Style="{StaticResource TextBoxValidationError}"/>
                        </DataTemplate>
                    </DataGridTemplateColumn.CellTemplate>
                </DataGridTemplateColumn>
                <!-- Stock -->
                <DataGridTemplateColumn  Header="Stock" MinWidth="100" Width="auto">
                    <DataGridTemplateColumn.CellTemplate >
                        <DataTemplate>
                            <TextBox Text="{Binding Path=Stock, UpdateSourceTrigger=PropertyChanged, 
                                     ValidatesOnExceptions=True, ValidatesOnDataErrors=True, NotifyOnValidationError=True,
                                     NotifyOnSourceUpdated=True, NotifyOnTargetUpdated=True}" 
                                     SourceUpdated="TextBox_SourceUpdated"
                                     TargetUpdated="TextBox_TargetUpdated"
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
	
	
