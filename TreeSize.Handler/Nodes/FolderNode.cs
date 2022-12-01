using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSize.Handler.Designations;

namespace TreeSize.Handler.Nodes
{
    public class FolderNode : Node
    {
        public DirectoryInfo DirectoryInfo { get; set; }

        public FolderNode(DirectoryInfo directoryInfo)
        {
            DirectoryInfo = directoryInfo;
            Icon = IconPath.Folder;
        }
    }
}
