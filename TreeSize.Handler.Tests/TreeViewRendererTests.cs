namespace TreeSize.Handler.Tests
{
    [TestClass]
    public class TreeViewRendererTests
    {
        TreeViewRenderer treeViewRenderer = new TreeViewRenderer();
        [TestMethod]
        public void TestMethod1()
        {
            DriveInfo drive = new DriveInfo(@"C:\");
            DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(typeof(TreeViewRendererTests).Assembly.Location));
            drive.RootDirectory.CreateSubdirectory();
        }
    }
}