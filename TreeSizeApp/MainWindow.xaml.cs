using System;
using System.Windows;
using System.Windows.Threading;
using TreeSize.Handler;

namespace TreeSizeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new TreeViewRenderer(treeView);
        }
    }
}
