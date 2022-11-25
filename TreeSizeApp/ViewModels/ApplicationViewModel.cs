using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TreeSize.Handler;
using TreeSize.Handler.Designations;
using TreeSize.Handler.Extensions;
using TreeSize.Handler.Nodes;
using TreeSizeApp.RelayCommand;

namespace TreeSizeApp.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private ConverterNodeToSelectTypeSize _converterNodeToSelectTypeSize = new ConverterNodeToSelectTypeSize();
        public event PropertyChangedEventHandler PropertyChanged;
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        private ObservableCollection<Node> _nodes;
        private TreeView _treeView = new TreeView();
        private TreeViewRenderer _treeViewRenderer;
        private FileManager _fileManager = new FileManager();
        private MainThreadDispatcher _mainThreadDispatcher = new MainThreadDispatcher(); 

        public ApplicationViewModel(TreeView treeView)
        {
            _treeView = treeView;
            _treeViewRenderer = new TreeViewRenderer(_fileManager, _mainThreadDispatcher);
            StartProgram();
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

        private async Task StartProgram()
        {
            _nodes = await _treeViewRenderer.RefreshNodes();
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            TreeSize.Items.Refresh();
        }

        private RellayCommand _refrashCommand;
        public RellayCommand RefrashCommand
        {
            get
            {
                return _refrashCommand ??
                (_refrashCommand = new RellayCommand(async (obj) =>
                {
                    Nodes = await _treeViewRenderer.RefreshNodes();
                    TreeSize.ItemsSource = Nodes;
                }));
            }
        }

        private RellayCommand _convertMbCommand;
        public RellayCommand ConvertMbCommand
        {
            get
            {
                return _convertMbCommand ??
                (_convertMbCommand = new RellayCommand(obj =>
                {
                    _converterNodeToSelectTypeSize.Convert(KindsSizes.MB, _nodes);
                    TreeSize.ItemsSource = Nodes;
                }));
            }
        }

        private RellayCommand _convertGbCommand;
        public RellayCommand ConvertGbCommand
        {
            get
            {
                return _convertGbCommand ??
                (_convertGbCommand = new RellayCommand(obj =>
                {
                    _converterNodeToSelectTypeSize.Convert(KindsSizes.GB, _nodes);
                    TreeSize.ItemsSource = Nodes;
                }));
            }
        }

        private RellayCommand _convertKbCommand;
        public RellayCommand ConvertKbCommand
        {
            get
            {
                return _convertKbCommand ??
                (_convertKbCommand = new RellayCommand(obj =>
                {
                    _converterNodeToSelectTypeSize.Convert(KindsSizes.KB, _nodes);
                    TreeSize.ItemsSource = Nodes;
                }));
            }
        }
    }
}
