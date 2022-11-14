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
                    FreeSpace = drive.TotalFreeSpace,
                    Icon = @"C:\Users\sasha\OneDrive - ITSTEP\Программирование\WPF\TreeSizeApp\TreeSizeApp\icons\drive.png"
                };
                disk.CountFoldersAndBytesAndFiles.Bytes = drive.TotalSize;
                Nodes.Add(disk);

                foreach (DirectoryInfo directory in drive.RootDirectory.GetDirectories()
                    .Where(x => (x.Attributes & FileAttributes.Hidden) == 0))
                {
                    disk.CountFoldersAndBytesAndFiles.Folders++;
                    Task.Run(new Action(() =>
                    {
                        Node folder = new Node()
                        {
                            Name = directory.Name,
                            Icon = @"C:\Users\sasha\source\repos\TreeSizeApp\TreeSizeApp\icons\folder.png"
                        };
                        try
                        {
                            folder.CountFoldersAndBytesAndFiles = LoadDirectories(directory, folder, _treeView, Nodes);
                            disk.CountFoldersAndBytesAndFiles.Files += folder.CountFoldersAndBytesAndFiles.Files;
                            disk.CountFoldersAndBytesAndFiles.Folders += folder.CountFoldersAndBytesAndFiles.Folders;
                        }
                        catch (Exception)
                        {
                            folder.CountFoldersAndBytesAndFiles.Bytes += 0;
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
                        Icon = @"C:\Users\sasha\source\repos\TreeSizeApp\TreeSizeApp\icons\file.png"
                    };
                    file.CountFoldersAndBytesAndFiles.Bytes += fi.Length;
                    disk.CountFoldersAndBytesAndFiles.Files++;
                    disk.Nodes.Add(file);
                }
            }
        }

        private CountFoldersAndBytesAndFiles LoadDirectories(DirectoryInfo directory, Node node, TreeView treeView, ObservableCollection<Node> root)
        {
            CountFoldersAndBytesAndFiles foldersAndBytesAndFilesInFolder = new CountFoldersAndBytesAndFiles();
            try
            {
                foreach (DirectoryInfo di in directory.GetDirectories().Where(x => (x.Attributes & FileAttributes.Hidden) == 0))
                {
                    foldersAndBytesAndFilesInFolder.Folders++;
                    Node folder = new Node()
                    {
                        Name = di.Name,
                        Icon = @"C:\Users\sasha\source\repos\TreeSizeApp\TreeSizeApp\icons\folder.png"
                    };
                    try
                    {
                        folder.CountFoldersAndBytesAndFiles = LoadDirectories(di, folder, treeView, root);
                        foldersAndBytesAndFilesInFolder.Folders += folder.CountFoldersAndBytesAndFiles.Folders;
                        foldersAndBytesAndFilesInFolder.Bytes += folder.CountFoldersAndBytesAndFiles.Bytes;
                        foldersAndBytesAndFilesInFolder.Files += folder.CountFoldersAndBytesAndFiles.Files;
                    }
                    catch (Exception)
                    {
                        folder.CountFoldersAndBytesAndFiles.Bytes += 0;
                    }
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        node.Nodes.Add(folder);
                    });
                }

                foreach (FileInfo fi in directory.GetFiles())
                {
                    Node file = new Node
                    {
                        Name = fi.Name,
                        Icon = @"C:\Users\sasha\source\repos\TreeSizeApp\TreeSizeApp\icons\file.png"
                    };
                    file.CountFoldersAndBytesAndFiles.Bytes += fi.Length;
                    foldersAndBytesAndFilesInFolder.Bytes += fi.Length;
                    foldersAndBytesAndFilesInFolder.Files++;
                    node.Nodes.Add(file);
                }
            }
            catch (UnauthorizedAccessException)
            {
            }

            return foldersAndBytesAndFilesInFolder;
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