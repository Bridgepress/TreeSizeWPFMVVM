using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSize.Handler
{
    public class FolderNode : Node
    {
        public DirectoryInfo DirectoryInfo { get; set; }

        public FolderNode(DirectoryInfo directoryInfo)
        {
            DirectoryInfo = directoryInfo;
        }
    }
}
