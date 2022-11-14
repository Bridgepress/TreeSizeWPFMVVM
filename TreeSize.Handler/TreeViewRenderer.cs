using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace TreeSize.Handler
{
    public class TreeViewRenderer
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public ObservableCollection<Node> Nodes { get; set; }
        private TreeView _treeView = new TreeView();


        public TreeViewRenderer(TreeView treeView)
        {
            _treeView = treeView;
            RefreshNodes();
            _treeView.ItemsSource = Nodes;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();
        }

        private void RefreshNodes()
        {
            _treeView.Items.Clear();
            Nodes = new ObservableCollection<Node>();

            foreach (DriveInfo drive in GetDrives())
            {
                Node disk = new Node
                {
                    Name = drive.Name,
                    TotalSize = drive.TotalSize,
                    FreeSpace = drive.TotalFreeSpace,
                    Icon = @"C:\Users\sasha\OneDrive - ITSTEP\Программирование\WPF\TreeSizeApp\TreeSizeApp\icons\drive.png"
                };
                Nodes.Add(disk);

                foreach (DirectoryInfo directory in drive.RootDirectory.GetDirectories()
                    .Where(x => (x.Attributes & FileAttributes.Hidden) == 0))
                {
                    Task.Run(new Action(() =>
                    {
                        Node folder = new Node()
                        {
                            Name = directory.Name,
                            Icon = @"C:\Users\sasha\source\repos\TreeSizeApp\TreeSizeApp\icons\folder.png"
                        };
                        try
                        {
                            Task<long?> task = Task<long?>.Factory.StartNew(() => LoadDirectories(directory, folder, _treeView, Nodes));
                            folder.TotalSize = task.Result;
                        }
                        catch (Exception)
                        {
                            folder.TotalSize = 0;
                        }
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            disk.Nodes.Add(folder);
                        });
                    }));
                }

                foreach (FileInfo fi in drive.RootDirectory.GetFiles()
                        .Where(x => (x.Attributes & FileAttributes.Hidden) == 0).ToArray())
                {
                    Node file = new Node
                    {
                        Name = fi.Name,
                        TotalSize = fi.Length,
                        Icon = @"C:\Users\sasha\source\repos\TreeSizeApp\TreeSizeApp\icons\file.png"
                    };
                    disk.Nodes.Add(file);
                }
            }
        }

        private long? LoadDirectories(DirectoryInfo directory, Node node, TreeView treeView, ObservableCollection<Node> root)
        {
            long? size = 0;
            try
            {
                foreach (DirectoryInfo di in directory.GetDirectories().Where(x => (x.Attributes & FileAttributes.Hidden) == 0))
                {
                    Task.Run(new Action(() =>
                    {
                        Node folder = new Node()
                        {
                            Name = di.Name,
                            Icon = @"C:\Users\sasha\source\repos\TreeSizeApp\TreeSizeApp\icons\folder.png"
                        };
                        try
                        {
                            folder.TotalSize = LoadDirectories(di, folder, treeView, root);
                        }
                        catch (Exception)
                        {
                            folder.TotalSize = 0;
                        }
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            node.Nodes.Add(folder);
                            node.TotalSize += folder.TotalSize;
                        });
                        
                    }));
                }

                foreach (FileInfo fi in directory.GetFiles())
                {
                    Node file = new Node
                    {
                        Name = fi.Name,
                        TotalSize = fi.Length,
                        Icon = @"C:\Users\sasha\source\repos\TreeSizeApp\TreeSizeApp\icons\file.png"
                    };
                    node.Nodes.Add(file);
                    size += fi.Length;
                }
            }
            catch (UnauthorizedAccessException)
            {
            }

            return size;
        }

        private List<DriveInfo> GetDrives()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<DriveInfo> newAllDrives = new List<DriveInfo>();

            foreach (var driveInfo in allDrives)
            {
                if (driveInfo.IsReady)
                {
                    newAllDrives.Add(driveInfo);
                }
            }

            return newAllDrives;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _treeView.ItemsSource = Nodes;
        }
    }
}