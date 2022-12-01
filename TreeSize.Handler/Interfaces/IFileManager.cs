using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSize.Handler.Interfaces
{
    public interface IFileManager
    {
        public DirectoryInfo[] GetDrives();
    }
}
