using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSize.Handler
{
    public static class ByteConverter
    {
        public static string Standart(long bytes)
        {
            string postfix = "Bytes";
            long result = bytes;
            if (bytes >= 1073741824)
            {
                result = bytes / 1073741824;
                postfix = "GB";
            }
            else if (bytes >= 1048576)
            {
                result = bytes / 1048576;
                postfix = "MB";
            }
            else if (bytes >= 1024)
            {
                result = bytes / 1024;
                postfix = "KB";
            }

            return result.ToString("F1") + " " + postfix;
        }
    }
}
