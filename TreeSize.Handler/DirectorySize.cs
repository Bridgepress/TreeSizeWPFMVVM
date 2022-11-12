using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSize.Handler
{
    public class DirectorySize
    {
        public long Size(DirectoryInfo directory)
        {
            long size = 0;
            FileInfo[] fis = directory.GetFiles();

            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }

            DirectoryInfo[] dis = directory.GetDirectories();

            foreach (DirectoryInfo di in dis)
            {
                size += Size(di);
            }

            return size;
        }
    }
}
