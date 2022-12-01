using System.IO;
using TreeSize.Handler.Designations;

namespace TreeSize.Handler.Nodes
{
    public class FileNode : Node
    {
        public FileInfo FileInfo { get; set; }

        public FileNode(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
            Icon = IconPath.File;
        }
    }
}
