using System;
using System.Collections;
using System.IO;
using System.Windows.Documents;
using TreeSize.Handler.Interfaces;

namespace TreeSize.Handler
{
    public class FileManager : IFileManager
    {
        public DirectoryInfo[] GetDrives()
        {
            string[] drives = Environment.GetLogicalDrives();
            DirectoryInfo[] directories = new DirectoryInfo[drives.Length];

            for (int i = 0; i < drives.Length; i++)
            {
                directories[i] = new DirectoryInfo(drives[i]);
            }

            return directories;
        }
    }
}
