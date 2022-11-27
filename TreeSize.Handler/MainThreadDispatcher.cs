using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TreeSize.Handler.Interfaces;

namespace TreeSize.Handler
{
    public class MainThreadDispatcher : IMainThreadDispatcher
    {
        public void Dispatcher(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}
