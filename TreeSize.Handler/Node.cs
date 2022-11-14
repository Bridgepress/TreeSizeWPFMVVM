using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TreeSize.Handler
{
    public class Node
    {

        public string? Icon { get; set; }
        public string? Name { get; set; }
        public CountFoldersAndBytesAndFiles CountFoldersAndBytesAndFiles { get; set; } = new CountFoldersAndBytesAndFiles();
        public long? FreeSpace { get; set; }
        public DateTime? LastWriteTime { get; set; }
        public string GetSize
        {
            get
            {
                string postfix = "Bytes";
                long result = CountFoldersAndBytesAndFiles.Bytes;
                if (CountFoldersAndBytesAndFiles.Bytes >= 1073741824)
                {
                    result = CountFoldersAndBytesAndFiles.Bytes / 1073741824;
                    postfix = "GB";
                }
                else if (CountFoldersAndBytesAndFiles.Bytes >= 1048576)
                {
                    result = CountFoldersAndBytesAndFiles.Bytes / 1048576;
                    postfix = "MB";
                }
                else if (CountFoldersAndBytesAndFiles.Bytes >= 1024)
                {
                    result = CountFoldersAndBytesAndFiles.Bytes / 1024;
                    postfix = "KB";
                }

                return result.ToString("F1") + " " + postfix;
            }
        }
        public ObservableCollection<Node> Nodes { get; set; }

        public Node()
        {
            Nodes = new ObservableCollection<Node>();
        }
    }
}
