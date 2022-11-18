using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSize.Handler.Interfaces
{
    public interface IMainThreadDispatcher
    {
        public void Dispatch(Action action);
    }
}
