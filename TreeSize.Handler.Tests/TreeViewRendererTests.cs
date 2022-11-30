using Moq;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Threading;
using TreeSize.Handler.Interfaces;
using TreeSize.Handler.Nodes;
using static System.Net.Mime.MediaTypeNames;

namespace TreeSize.Handler.Tests
{
    [TestClass]
    public class TreeViewRendererTests
    {

        [TestMethod]
        public async Task CountDirectoryToTheNode()
        {
            HostedTask hostedTask = new HostedTask();
            Mock<IFileManager> mock = new Mock<IFileManager>();
            Mock<IMainThreadDispatcher> dispetcherMock = new Mock<IMainThreadDispatcher>();
            DirectoryInfo dir = new DirectoryInfo("Test1");
            dir.CreateSubdirectory("sub1").CreateSubdirectory("sub2").CreateSubdirectory("sub3");
            Directory.CreateDirectory(dir.FullName + "\\test2");
            var file = File.Create(dir.FullName + "\\file1.txt");
            File.Create(dir.FullName + "\\sub1\\sub2\\sub3\\file2.txt");
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.WriteLine("123456677899057654654432");
            }
            var a = dir.GetFiles();
            mock.Setup(m => m.GetDrives()).Returns(new DirectoryInfo[]{
                dir
            });
            dispetcherMock.Setup(m => m.Dispatcher(It.IsAny<Action>()));
            TreeViewRenderer treeViewRenderer = new TreeViewRenderer(mock.Object, dispetcherMock.Object);
            ObservableCollection<Node> nodes = treeViewRenderer.RefreshNodes(hostedTask);
            hostedTask.Wait();
            Assert.IsTrue(nodes[0].CountFoldersAndBytesAndFiles.Folders == 4);
            Assert.IsTrue(nodes[0].CountFoldersAndBytesAndFiles.Files == 2);
            Assert.IsTrue(nodes[0].CountFoldersAndBytesAndFiles.Bytes == 26);
        }
    }
}