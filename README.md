# Example-IDataErrorInfo
Demonstrates IDataErrorInfo implementation using DataGrid and TextBox binding.

Code derived from https://kmatyaszek.github.io/wpf%20validation/2019/03/06/wpf-validation-using-idataerrorinfo.html

<Window.Resources>
        <!-- TextBox Validation Style
                Tooltip single error reporting -->
        <Style x:Key="Example-TextBoxValidationError" TargetType="{x:Type TextBox}">
            <Style.Triggers>
                <Trigger Property="Validation.HasError" Value="true">
                    <Setter Property="ToolTip" Value="{Binding RelativeSource={x:Static RelativeSource.Self}, Path=(Validation.Errors)/ErrorContent}"/>
                </Trigger>
            </Style.Triggers>
        </Style>

        <!-- DataGrid.RowValidationErrorTemplate
                Display ToolTip single error reported, and yellow exclamation mark within a red circle on row containing validation errors.-->
        <ControlTemplate x:Key="Example-RowValidationErrorTemplate">
            <!-- Tooltip single error reported -->
            <Grid Margin="0,-2,0,-2" ToolTip="{Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type DataGridRow}}, Path=(Validation.Errors)/ErrorContent}">
                <Ellipse StrokeThickness="0" Fill="Red" Width="{TemplateBinding FontSize}" Height="{TemplateBinding FontSize}" />
                <TextBlock Text="!" FontSize="{TemplateBinding FontSize}" FontWeight="Bold" Foreground="Yellow" HorizontalAlignment="Center" VerticalAlignment="Center" />
            </Grid>
        </ControlTemplate>

        <!-- DataGrid.RowDetailsTemplate
                Display one or more row validation errors in RowDetails when row is selected. -->
        <DataTemplate x:Key="ShowValidationErrorInTextBlock">
            <StackPanel>
                <ItemsControl ItemsSource="{Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type DataGridRow}}, Path=(Validation.Errors)}">
                    <ItemsControl.ItemTemplate>
                        <DataTemplate>
                            <TextBlock Text="{Binding ErrorContent}" Foreground="Red" Padding="4,0,4,2"/>
                        </DataTemplate>
                    </ItemsControl.ItemTemplate>
                </ItemsControl>
            </StackPanel>
        </DataTemplate>
    </Window.Resources>
