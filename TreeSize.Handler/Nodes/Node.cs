using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;

namespace TreeSize.Handler.Nodes
{
    public class Node
    {
        public string? Icon { get; set; }
        public string? Name { get; set; }
        public CountFoldersAndBytesAndFiles CountFoldersAndBytesAndFiles { get; set; } = new CountFoldersAndBytesAndFiles();
        public long FreeSpace { get; set; } = 0;
        public DateTime? LastWriteTime { get; set; }
        public ObservableCollection<Node> Nodes { get; set; } = new ObservableCollection<Node>();
        public bool IsSelected { get; set; }
        public bool IsExpanded { get; set; }
        public string GetSize
        {
            get
            {
                if (GetSize == "")
                {
                    GetSize = ByteConverter.GB(CountFoldersAndBytesAndFiles.Bytes);
                }
                return GetSize;
            }
            set
            {
                GetSize = value;
            }
        }
    }
}
