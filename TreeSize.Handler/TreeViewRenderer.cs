using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TreeSize.Handler.Nodes;

namespace TreeSize.Handler
{
    public class TreeViewRenderer
    {
        private MainThreadDispatcher _mainThreadDispatcher = new MainThreadDispatcher();

        public ObservableCollection<Node> RefreshNodes()
        {
            ObservableCollection<Node> _nodes = new ObservableCollection<Node>();

            foreach (DriveInfo drive in GetDrives())
            {
                DriveNode disk = new DriveNode(drive)
                {
                    Name = drive.Name,
                    FreeSpace = drive.TotalFreeSpace,
                };
                _nodes.Add(disk);

                foreach (DirectoryInfo directory in drive.RootDirectory.GetDirectories()
                    .Where(x => (x.Attributes & FileAttributes.Hidden) == 0))
                {
                    disk.CountFoldersAndBytesAndFiles.Folders++;
                    Task.Run(new Action(() =>
                    {
                        FolderNode folder = new FolderNode(directory)
                        {
                            Name = directory.Name,
                        };
                        folder.CountFoldersAndBytesAndFiles = LoadDirectories(directory, folder, _nodes);
                        disk.CountFoldersAndBytesAndFiles.Files += folder.CountFoldersAndBytesAndFiles.Files;
                        disk.CountFoldersAndBytesAndFiles.Folders += folder.CountFoldersAndBytesAndFiles.Folders;
                        disk.CountFoldersAndBytesAndFiles.Bytes += folder.CountFoldersAndBytesAndFiles.Bytes;
                        _mainThreadDispatcher.Dispatch(new Action(() => disk.Nodes.Add(folder)));
                    }));
                }

                AddFilesToNode(drive.RootDirectory.GetFiles()
                   .Where(x => (x.Attributes & FileAttributes.Hidden) == 0).ToList(), disk, disk.CountFoldersAndBytesAndFiles);
            }

            return _nodes;
        }

        private CountFoldersAndBytesAndFiles LoadDirectories(DirectoryInfo directory, Node node, ObservableCollection<Node> root)
        {
            CountFoldersAndBytesAndFiles foldersAndBytesAndFilesInFolder = new CountFoldersAndBytesAndFiles();
            AddDirectoriesToNode(directory, node, foldersAndBytesAndFilesInFolder, root);
            AddFilesToNode(directory.GetFiles().ToList(), node, foldersAndBytesAndFilesInFolder);
            return foldersAndBytesAndFilesInFolder;
        }

        private void AddDirectoriesToNode(DirectoryInfo directory, Node node, CountFoldersAndBytesAndFiles foldersAndBytesAndFilesInFolder, ObservableCollection<Node> root)
        {
            try
            {
                foreach (DirectoryInfo di in directory.GetDirectories().Where(x => (x.Attributes & FileAttributes.Hidden) == 0))
                {
                    foldersAndBytesAndFilesInFolder.Folders++;
                    FolderNode folder = new FolderNode(di)
                    {
                        Name = di.Name,
                    };
                    try
                    {
                        folder.CountFoldersAndBytesAndFiles = LoadDirectories(di, folder, root);
                        foldersAndBytesAndFilesInFolder.Folders += folder.CountFoldersAndBytesAndFiles.Folders;
                        foldersAndBytesAndFilesInFolder.Bytes += folder.CountFoldersAndBytesAndFiles.Bytes;
                        foldersAndBytesAndFilesInFolder.Files += folder.CountFoldersAndBytesAndFiles.Files;
                    }
                    catch (Exception)
                    {
                        folder.CountFoldersAndBytesAndFiles.Bytes += 0;
                    }
                    _mainThreadDispatcher.Dispatch(new Action(() => node.Nodes.Add(folder)));
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        private void AddFilesToNode(List<FileInfo> directory, Node node, CountFoldersAndBytesAndFiles foldersAndBytesAndFilesInFolder)
        {
            try
            {
                foreach (FileInfo fi in directory)
                {
                    FileNode file = new FileNode(fi)
                    {
                        Name = fi.Name,
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
    }
}