﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Example_IDataErrorInfo
{
    /// <summary>
    /// Code derived from https://kmatyaszek.github.io/wpf%20validation/2019/03/06/wpf-validation-using-idataerrorinfo.html
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = TestData.CreateDataItems(4);            
        }
    }
}
