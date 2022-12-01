using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TreeSize.Handler
{
    public class HostedTask
    {
        private List<Task> _tasks { get; set; } = new List<Task>();
        private Object taskLocker = new object();
        public static CancellationTokenSource CancellationToken = new CancellationTokenSource();

        public async Task Run(Action action)
        {
            if (CancellationToken.IsCancellationRequested)
            {
                return;
            }
            var task = Task.Factory.StartNew(action, CancellationToken.Token);
            lock (taskLocker)
            {
                _tasks.Add(task);
            }
            await task;
            lock (taskLocker)
            {
                _tasks.Remove(task);
            }
        }

        public void Wait()
        {
            IEnumerable<Task> queueTask;
            do
            {
                lock (taskLocker)
                {
                    queueTask = _tasks.Where(t => !t.IsCompleted);
                }

                if ((queueTask != null) && (queueTask.Count() > 0))
                {
                    Task.WhenAll(queueTask).Wait();
                }
            } while (queueTask == null);
        }

        public void StopTasks()
        {
            CancellationToken.Cancel();
        }
        public void StartTasks()
        {
            CancellationToken.Dispose();
            CancellationToken = new CancellationTokenSource();
        }
    }
}
