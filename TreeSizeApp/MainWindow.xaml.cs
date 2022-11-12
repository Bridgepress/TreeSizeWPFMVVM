using System;
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
using TreeSize.Handler;

namespace TreeSizeApp
{

    public partial class MainWindow : Window
    {
        private FormRender _formRender;
        public MainWindow()
        {
            InitializeComponent();
            _formRender = new FormRender(ListView, TreeView, TextBox, this);
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListView.SelectedItem != null)
            {
                if (TextBox.Text == String.Empty)
                {
                    _formRender.GoTo(((DirectoryContent)ListView.SelectedItem).Name);
                    TextBox.Text = _formRender.PathToFile;
                }
                else
                {
                    _formRender.GoToRelative(((DirectoryContent)ListView.SelectedItem).Name);
                    TextBox.Text = _formRender.PathToFile;
                }
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _formRender.GoTo(TextBox.Text);
            }
        }
    }
}
