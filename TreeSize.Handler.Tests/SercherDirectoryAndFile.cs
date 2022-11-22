using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSize.Handler.Designations;
using TreeSize.Handler.Interfaces;
using TreeSize.Handler.Nodes;

namespace TreeSize.Handler.Tests
{
    public class SercherDirectoryAndFile
    {
        private MainThreadDispatcher _mainThreadDispatcher = new MainThreadDispatcher();

        public Node SerchDirectory(string name, ObservableCollection<Node> node)
        {
            return GoTheNode(name, node);
        }

        private Node GoTheNode(string name, ObservableCollection<Node> node)
        {
            Node foundNode = null;
            foreach (var item in node.ToList())
            {
                if(item.Name == name)
                {
                    return item;
                }
                foundNode = GoTheAllNode(item, name);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }
            return null;
        }

        private Node GoTheAllNode(Node nodes, string name)
        {
            Node foundNode = null;
            foreach (Node node in nodes.Nodes)
            {
                if(node.Name == name)
                {
                    return node;
                }
                if (node.Nodes.Count > 0)
                {
                    _mainThreadDispatcher.Dispatch(new Action(() => foundNode = GoTheAllNode(node, name)));
                }
            }
            return null;
        }
    }
}
