using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSize.Handler.Tests
{
    public class FileManager
    {
        public bool Create(string path, string nameDirectory)
        {
            FileInfo file = new FileInfo(path + "\\" + nameDirectory);
            if (!file.Exists)
            {
                file.Create();
                return true;
            }
            return false;
        }
    }
}
