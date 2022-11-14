using System.Windows;
using TreeSize.Handler;

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
            DataContext = new TreeViewRenderer(treeView1);
        }
    }
}
