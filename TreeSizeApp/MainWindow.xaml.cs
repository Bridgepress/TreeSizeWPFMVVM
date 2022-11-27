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
    public partial class MyFirstWindow
    {
        public MyFirstWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel(treeView);
        }

    }
}
