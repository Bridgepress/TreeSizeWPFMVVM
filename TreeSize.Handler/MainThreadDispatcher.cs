using System;
using System.Windows;
using TreeSize.Handler.Interfaces;

namespace TreeSize.Handler
{
    public class MainThreadDispatcher : IMainThreadDispatcher
    {
        public void Dispatcher(Action action)
        {
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.Invoke(action);
            }
        }
    }
}
