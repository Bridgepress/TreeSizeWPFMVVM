using System;
using System.Collections.ObjectModel;

namespace TreeSize.Handler
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
        public string GetStandartSize
        {
            get
            {
                return ByteConverter.Standart(CountFoldersAndBytesAndFiles.Bytes);
            }
        }

    }
}
