using Moq;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows.Threading;
using TreeSize.Handler.Interfaces;
using TreeSize.Handler.Nodes;
using static System.Net.Mime.MediaTypeNames;

namespace TreeSize.Handler.Tests
{
    [TestClass]
    public class TreeViewRendererTests
    {
        DirectoryManager directoryManager = new DirectoryManager();
        List<DriveInfo> allDrive = new List<DriveInfo>();
        FileManager fileManager = new FileManager();
        MainThreadDispatcher mainThreadDispatcher = new MainThreadDispatcher();

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
            Mock<IFileManager> mock = new Mock<IFileManager>();
            Mock<IMainThreadDispatcher> dispetcherMock = new Mock<IMainThreadDispatcher>();
            directoryManager.Create(allDrive[0].ToString(), "Test1");
            directoryManager.Create(allDrive[0].ToString(), "Test1/sub1");
            directoryManager.Create(allDrive[0].ToString(), "Test1/sub1/sub2");
            directoryManager.Create(allDrive[0].ToString(), "Test1/sub1/sub2/sub3");
            fileManager.Create(allDrive[0].ToString(), "Test1/sub1/sub2/sub3/testFile1.txt");
            fileManager.Create(allDrive[0].ToString(), "Test1/sub1/sub2/sub3/testFile2.txt");
            DirectoryInfo dir = new DirectoryInfo(allDrive[0].ToString() + "Test1");
            mock.Setup(m => m.GetDrives()).Returns(new DirectoryInfo[]{
                dir
            });
            TreeViewRenderer treeViewRenderer = new TreeViewRenderer(mock.Object, mainThreadDispatcher);
            ObservableCollection<Node> nodes = await treeViewRenderer.RefreshNodes();
            directoryManager.Delete(allDrive[0].ToString() + "Test1");
            Assert.IsTrue(nodes[0].CountFoldersAndBytesAndFiles.Folders == 3);
            Assert.IsTrue(nodes[0].CountFoldersAndBytesAndFiles.Files == 2);
        }
    }
}