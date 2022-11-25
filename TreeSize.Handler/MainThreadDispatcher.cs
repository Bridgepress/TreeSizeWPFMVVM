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
        public async Task DispatchAsync(Action action)
        {
            await Application.Current.Dispatcher.InvokeAsync(action);
        }
    }
}
