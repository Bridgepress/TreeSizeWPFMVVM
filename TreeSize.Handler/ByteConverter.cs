using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSize.Handler.Designations;

namespace TreeSize.Handler
{
    public static class ByteConverter
    {
        private const int _byteInKb = 1024;
        private const int _byteInMb = 1048576;
        private const int _byteInGb = 1073741824;

        public static string GB(long bytes)
        {
            string postfix = KindsSizes.Bites.ToString();
            long result = bytes;
            if (bytes >= _byteInGb)
            {
                result = bytes / _byteInGb;
                postfix = KindsSizes.GB.ToString();
            }
            else if (bytes >= _byteInMb)
            {
                result = bytes / _byteInMb;
                postfix = KindsSizes.MB.ToString();
            }
            else if (bytes >= _byteInKb)
            {
                result = bytes / _byteInKb;
                postfix = KindsSizes.KB.ToString();
            }

            return result.ToString("F1") + " " + postfix;
        }

        public static string KB(long bytes)
        {
            return ((double)bytes / _byteInKb).ToString()+" "+KindsSizes.KB.ToString();
        }

        public static string MB(long bytes)
        {
            return ((double)bytes / _byteInKb / _byteInKb).ToString("0.###") + " " + KindsSizes.MB.ToString();
        }
    }
}
