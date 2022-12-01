using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using TreeSize.Handler.Extensions;
using TreeSize.Handler.Interfaces;
using TreeSize.Handler.Nodes;

namespace TreeSize.Handler
{
    public class TreeViewRenderer
    {
        private IFileManager _fileManager;
        private IMainThreadDispatcher _mainThreadDispatcher;

        public TreeViewRenderer(IFileManager fileManager, IMainThreadDispatcher mainThreadDispatcher)
        {
            _fileManager = fileManager;
            _mainThreadDispatcher = mainThreadDispatcher;
        }

        public ObservableCollection<Node> RefreshNodes(HostedTask hostedTask)
        {
            ObservableCollection<Node> _nodes = new ObservableCollection<Node>();
            List<DriveNode> drives = AddDriveToNode(_nodes);

            foreach (DriveNode drive in drives)
            {
                foreach (DirectoryInfo directory in drive.DriveInfo.GetDirectories()
                     .Where(x => (x.Attributes & FileAttributes.Hidden) == 0))
                {
                    drive.CountFoldersAndBytesAndFiles.Folders++;
                    hostedTask.Run(new Action(async () =>
                    {
                        FolderNode folder = new FolderNode(directory)
                        {
                            Name = directory.Name,
                        };
                        folder.CountFoldersAndBytesAndFiles = LoadDirectories(directory, folder, _nodes);
                        drive.CountFoldersAndBytesAndFiles.Files += folder.CountFoldersAndBytesAndFiles.Files;
                        drive.CountFoldersAndBytesAndFiles.Folders += folder.CountFoldersAndBytesAndFiles.Folders;
                        drive.CountFoldersAndBytesAndFiles.Bytes += folder.CountFoldersAndBytesAndFiles.Bytes;
                        _mainThreadDispatcher.Dispatcher(new Action(() => drive.Nodes.Add(folder)));
                        drive.GetSize = ByteConverter.GB(drive.CountFoldersAndBytesAndFiles.Bytes);
                    }));
                }

                AddFilesToNode(drive.DriveInfo.GetFiles()
                   .Where(x => (x.Attributes & FileAttributes.Hidden) == 0).ToList(), drive, drive.CountFoldersAndBytesAndFiles);
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
            if (HostedTask.CancellationToken.IsCancellationRequested)
            {
                return;
            }
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
                    _mainThreadDispatcher.Dispatcher(new Action(() => node.Nodes.Add(folder)));
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

        private List<DriveNode> AddDriveToNode(ObservableCollection<Node> nodes)
        {
            DirectoryInfo[] drives = _fileManager.GetDrives();
            List<DriveNode> driveNodes = new List<DriveNode>();

            foreach (DirectoryInfo drive in drives)
            {
                DriveNode disk = new DriveNode(drive)
                {
                    Name = drive.Name,
                };
                driveNodes.Add(disk);
                nodes.Add(disk);
            }

            return driveNodes;
        }
    }
}