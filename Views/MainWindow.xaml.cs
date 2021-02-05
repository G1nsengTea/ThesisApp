﻿using System.Windows;
using ThesisApp.ViewModels;

namespace ThesisApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }
    }
}
