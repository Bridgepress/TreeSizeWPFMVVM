using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TreeSize.Handler
{
    public class TreeViewRenderer
    {
        public ObservableCollection<Node> Nodes { get; set; }
        private TreeView _treeView = new TreeView();

        public TreeViewRenderer(TreeView treeView)
        {
            _treeView = treeView;
            //RefreshDataForTreeView();
            RefreshNodes();
            _treeView.ItemsSource = Nodes;
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

                foreach (DirectoryInfo directory in drive.RootDirectory.GetDirectories())
                {

                    Node folder = new Node()
                    {
                        Name = directory.Name,
                        Icon = @"C:\Users\sasha\source\repos\TreeSizeApp\TreeSizeApp\icons\folder.png"
                    };
                    try
                    {
                        Task<long?> task = Task<long?>.Factory.StartNew(() => LoadDirectories(directory, folder));
                        folder.TotalSize = task.Result;
                    }
                    catch (Exception)
                    {
                        folder.TotalSize = 0;
                    }
                    disk.Nodes.Add(folder);
                }
            }
        }

        public static long? LoadDirectories(DirectoryInfo directory, Node node)
        {
            long? size = 0;
            try
            {
                foreach (DirectoryInfo di in directory.GetDirectories())
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
                            folder.TotalSize = LoadDirectories(di, folder);
                        }
                        catch (Exception)
                        {
                            folder.TotalSize = 0;
                        }
                        Application.Current.Dispatcher.Invoke(
                            () =>
                            {
                                node.Nodes.Add(folder);
                            });
                        size += folder.TotalSize;
                    }));
                }
            }
            catch (Exception)
            {
            }
            try
            {
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

        public static List<DriveInfo> GetDrives()
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
    }
}
