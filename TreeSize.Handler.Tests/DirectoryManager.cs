using System.Runtime;

namespace TreeSize.Handler.Tests
{
    public class DirectoryManager
    {
        public bool Create(string path, string nameDirectory)
        {
            DirectoryInfo dir = new DirectoryInfo(path + nameDirectory);
            if (!dir.Exists)
            {
                dir.Create();
                return true;
            }
            return false;
        }

        public bool Delete(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                dir.Delete(true);
                return true;
            }
            return false;
        }
    }
}
