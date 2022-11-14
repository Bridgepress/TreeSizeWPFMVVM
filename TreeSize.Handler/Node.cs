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
        public long? TotalSize { get; set; }
        public long? FreeSpace { get; set; }
        public DateTime? LastWriteTime { get; set; }
        public ObservableCollection<Node> Nodes { get; set; }

        public Node()
        {
            Nodes = new ObservableCollection<Node>();
        }
    }
}
