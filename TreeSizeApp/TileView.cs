using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace TreeSizeApp
{
    public class TileView : ViewBase
    {
        public DataTemplate ItemTemplate { get; set; }

        protected override object DefaultStyleKey
        {
            get
            {
                return new ComponentResourceKey(GetType(), "TileView");
            }
        }

        protected override object ItemContainerDefaultStyleKey
        {
            get
            {
                return new ComponentResourceKey(GetType(), "TileViewItem");
            }
        }
    }
}
