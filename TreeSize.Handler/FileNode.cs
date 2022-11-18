using System.IO;

namespace TreeSize.Handler
{
    public class FileNode : Node
    {
        public FileInfo FileInfo { get; set; }
        public FileNode(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }
    }
}
