using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TreeSize.Handler
{
    public class DirectoryContent
    {
        public BitmapImage Icon { get; set; }
        public string Name { get; set; }
        public long? TotalSize { get; set; }
        public long? FreeSpace { get; set; }
        public DateTime? LastWriteTime { get; set; }
    }
}
