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
            Mock<IFileManager> mock = new Mock<IFileManager>();
            Mock<IMainThreadDispatcher> dispetcherMock = new Mock<IMainThreadDispatcher>();
            DirectoryInfo dir = new DirectoryInfo("Test1");
            dir.CreateSubdirectory("sub1").CreateSubdirectory("sub2").CreateSubdirectory("sub2");
            mock.Setup(m => m.GetDrives()).Returns(new DirectoryInfo[]{
                dir
            });
            dispetcherMock.Setup(m => m.DispatchAsync(It.IsAny<Action>()));
            TreeViewRenderer treeViewRenderer = new TreeViewRenderer(mock.Object, dispetcherMock.Object);
            ObservableCollection<Node> nodes = await treeViewRenderer.RefreshNodes();
            Assert.IsTrue(nodes[0].CountFoldersAndBytesAndFiles.Folders == 3);
        }
    }
}