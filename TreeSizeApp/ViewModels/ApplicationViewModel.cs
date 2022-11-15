using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Threading;
using TreeSize.Handler;

namespace TreeSizeApp.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        private ObservableCollection<Node> _nodes;
        private TreeView _treeView = new TreeView();
        private TreeViewRenderer _treeViewRenderer;

        public ApplicationViewModel(TreeView treeView)
        {
            _treeView = treeView;
            _treeViewRenderer = new TreeViewRenderer();
            _nodes = _treeViewRenderer.RefreshNodes();
            _dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
            _dispatcherTimer.Start();
        }
        public ObservableCollection<Node> Nodes
        {
            get
            {
                return _nodes;
            }
            set
            {
                OnPropertyChanged("Nodes");
                _nodes = value;
            }
        }

        public TreeView TreeSize
        {
            get
            {
                return _treeView;
            }
            set
            {
                OnPropertyChanged("TreeSize");
                _treeView = value;
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            TreeSize.Items.Refresh();
        }
    }
}
