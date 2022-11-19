using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSize.Handler.Nodes
{
    public class DriveNode : Node
    {
        public DriveInfo DriveInfo { get; set; }

        public DriveNode(DriveInfo driveInfo)
        {
            DriveInfo = driveInfo;
            Icon = IconPath.Drive;
        }
    }
}
