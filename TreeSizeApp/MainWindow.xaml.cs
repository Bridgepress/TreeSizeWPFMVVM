using System;
using System.Windows;
using System.Windows.Threading;
using TreeSize.Handler;
using TreeSizeApp.ViewModels;

namespace TreeSizeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel(treeView);
        }
    }
}
