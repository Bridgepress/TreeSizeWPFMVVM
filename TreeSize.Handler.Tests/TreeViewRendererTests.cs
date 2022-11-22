using Moq;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using TreeSize.Handler.Nodes;
using static System.Net.Mime.MediaTypeNames;

namespace TreeSize.Handler.Tests
{
    [TestClass]
    public class TreeViewRendererTests
    {
        TreeViewRenderer TreeViewRenderer = new TreeViewRenderer();
        DirectoryManager directoryManager = new DirectoryManager();
        FileManager fileManager = new FileManager();
        List<DriveInfo> allDrive = new List<DriveInfo>();
        SercherDirectoryAndFile sercherDirectoryAndFile = new SercherDirectoryAndFile();

        public TreeViewRendererTests()
        {
            foreach (var driveInfo in DriveInfo.GetDrives())
            {
                if (driveInfo.IsReady)
                {
                    allDrive.Add(driveInfo);
                }
            }
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            directoryManager.Create(allDrive[0].ToString(), "Test1");
            directoryManager.Create(allDrive[0].ToString(), "Test1/Test2");
            directoryManager.Create(allDrive[0].ToString(), "Test1/sub1");
            directoryManager.Create(allDrive[0].ToString(), "Test1/sub1/sub2");
            directoryManager.Create(allDrive[0].ToString(), "Test1/sub1/sub2/sub3");
            fileManager.Create(allDrive[0].ToString(), "Test1/sub1/sub2/sub3/testFile.txt");

            DirectoryInfo dir1 = new DirectoryInfo(allDrive[0].ToString() + "Test1");
            DirectoryInfo dir2 = new DirectoryInfo(allDrive[0].ToString() + "Test1/Test2");
            DirectoryInfo dir3 = new DirectoryInfo(allDrive[0].ToString() + "Test1/sub1");
            DirectoryInfo dir4 = new DirectoryInfo(allDrive[0].ToString() + "Test1/sub1/sub2/sub3");

            //mock.Setup(m => m.RootDirectory.GetDirectories()).Returns(new DirectoryInfo[]
            //{
            //    dir1
            //});
            //drives.Add(mock.Object);

            ObservableCollection<Node> nodes = TreeViewRenderer.RefreshNodes();

            Node node1 = sercherDirectoryAndFile.SerchDirectory("Test1", nodes);
            Node node2 = sercherDirectoryAndFile.SerchDirectory("Test2", nodes);
            Node node3 = sercherDirectoryAndFile.SerchDirectory("sub3", nodes);

            directoryManager.Delete(allDrive[0].ToString() + "Test1");
        }
    }
}