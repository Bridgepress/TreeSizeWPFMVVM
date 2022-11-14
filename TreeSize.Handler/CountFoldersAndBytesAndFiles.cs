using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace TreeSize.Handler
{
    public class CountFoldersAndBytesAndFiles
    {
        public long Bytes { get; set; }
        public int Files { get; set; }
        public int Folders { get; set; }
    }
}
