﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System;

namespace Example_IDataErrorInfo
{
    /// <summary>
    /// Code derived from https://kmatyaszek.github.io/wpf%20validation/2019/03/06/wpf-validation-using-idataerrorinfo.html
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> ErrorList;

        public MainWindow()
        {
            InitializeComponent();
            
            DataContext = TestData.CreateDataItems(4);
            ValidationErrorListBox.ItemsSource = ErrorList = new();
        }

        /// <summary>
        /// Grid's Validation.Error receives notification from bindings that have NotifyOnValidationError=True.
        /// </summary>
        /// <param name="sender">The control, in this case Grid, that raised the event.</param>
        /// <param name="e">ValidationErrorEventArgs associated with the binding.</param>
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {            
            string error = (string)e.Error.ErrorContent;

            if (e.Action == ValidationErrorEventAction.Added)
            {
                ErrorList.Add(error);
            }
            else
            {
                ErrorList.Remove(error);
            }
        }

        private void TextBox_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            
            string message = string.Format("@ {0} {1}", DateTime.Now.ToString("hh:mm:ss tt"), e.Property);
            SourceUpdatedTextBlock.Text = message;
        }

        private void TextBox_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            string message = string.Format("@ {0} {1} ", DateTime.Now.ToString("hh:mm:ss tt"), e.Property);
            TargetUpdatedTextBlock.Text = message;
        }
    }
}
