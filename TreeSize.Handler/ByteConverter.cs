using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSize.Handler
{
    public static class ByteConverter
    {
        public static string GB(long bytes)
        {
            string postfix = KindsSizes.Bites.ToString();
            long result = bytes;
            if (bytes >= 1073741824)
            {
                result = bytes / 1073741824;
                postfix = KindsSizes.GB.ToString();
            }
            else if (bytes >= 1048576)
            {
                result = bytes / 1048576;
                postfix = KindsSizes.MB.ToString();
            }
            else if (bytes >= 1024)
            {
                result = bytes / 1024;
                postfix = KindsSizes.KB.ToString();
            }

            return result.ToString("F1") + " " + postfix;
        }

        public static string KB(long bytes)
        {
            return ((double)bytes / (double)1024).ToString()+" "+KindsSizes.KB.ToString();
        }

        public static string MB(long bytes)
        {
            return ((double)bytes / (double)1024 / (double)1024).ToString("0.###") + " " + KindsSizes.MB.ToString();
        }
    }
}
