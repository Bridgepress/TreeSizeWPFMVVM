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
        private string _getSize;
        public string GetSize
        {
            get
            {
                if (String.IsNullOrEmpty(_getSize))
                {
                    _getSize = ByteConverter.GB(CountFoldersAndBytesAndFiles.Bytes);
                }
                return _getSize;
            }
            set
            {
                _getSize = value;
            }
        }
    }
}
