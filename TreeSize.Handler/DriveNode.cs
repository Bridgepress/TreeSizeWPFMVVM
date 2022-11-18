using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSize.Handler
{
    public class DriveNode : Node
    {
        public DriveInfo DriveInfo { get; set; } 
        public DriveNode(DriveInfo driveInfo)
        {
            DriveInfo = driveInfo;
        }
    }
}
