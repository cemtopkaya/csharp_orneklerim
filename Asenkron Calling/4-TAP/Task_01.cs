using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Threading_Event
{
    class Task_01
    {
        public static void Calis()
        {
            BasitTask();
        }

        static void BasitTask()
        {
            /*
public Task(Action action);
public Task(Action action, CancellationToken cancellationToken);             
public Task(Action action, TaskCreationOptions creationOptions);        }
public Task(Action<object> action, object state);    }
public Task(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions);}
public Task(Action<object> action, object state, CancellationToken cancellationToken);
public Task(Action<object> action, object state, TaskCreationOptions creationOptions);
public Task(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions);
*/
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            cancellationToken.Register(() =>
            {
                Console.WriteLine("Cancel oldu...");
            });

            Task t = new Task(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("i: {0}", i);
                    Thread.Sleep(1000);
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Taskın durudurulması istendi");
                        return;
                    }
                }
            }, cancellationToken);

            t.Start();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Main thread i:" + i);
                Thread.Sleep(1000);
                if (i == 2)
                {
                    cancellationTokenSource.Cancel();
                }
            }
            t.Wait();
        }
    }
}