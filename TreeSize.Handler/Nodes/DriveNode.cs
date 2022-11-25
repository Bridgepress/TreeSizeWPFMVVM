using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSize.Handler.Designations;

namespace TreeSize.Handler.Nodes
{
    public class DriveNode : Node
    {
        public DirectoryInfo DriveInfo { get; set; }

        public DriveNode(DirectoryInfo driveInfo)
        {
            DriveInfo = driveInfo;
            Icon = IconPath.Drive;
        }
    }
}
