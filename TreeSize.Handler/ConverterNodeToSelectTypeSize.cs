using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TreeSize.Handler.Interfaces;
using TreeSize.Handler.Nodes;

namespace TreeSize.Handler
{
    public class ConverterNodeToSelectTypeSize
    {
        private Dictionary<KindsSizes, Func<long, string>> _calculate;

        public ConverterNodeToSelectTypeSize()
        {
            _calculate = new Dictionary<KindsSizes, Func<long, string>>
            {
                {KindsSizes.MB,(bytes) => ByteConverter.MB(bytes)},
                 {KindsSizes.GB,(bytes) => ByteConverter.GB(bytes)},
                  {KindsSizes.KB,(bytes) => ByteConverter.KB(bytes)}
            };
        }

        public void Convert(KindsSizes kindsSize, ObservableCollection<Node> node)
        {
            foreach (var item in node.ToList())
            {
                item.GetSize = _calculate[kindsSize](item.CountFoldersAndBytesAndFiles.Bytes);
                Task.Run(() =>
                {
                    ConvertNode(kindsSize, item);
                });
            }  
        }

        private void ConvertNode(KindsSizes kindsSize, Node nodes)
        {
            foreach (Node node in nodes.Nodes)
            {
                node.GetSize = _calculate[kindsSize](node.CountFoldersAndBytesAndFiles.Bytes);
                if(node.Nodes.Count > 0)
                {
                    ConvertNode(kindsSize, node);
                }
            }
        }
    }
}
