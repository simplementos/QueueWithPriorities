using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queue
{
    class Program
    {
        static async Task Foo(CancellationToken token)
        {
            for(int i = 0; i < 1000; i++)
            {
                Console.WriteLine(i);
                await Task.Delay(1000, token);
                token.ThrowIfCancellationRequested();
            }
        }
        static Random rand = new Random();
        static void Main(string[] args)
        {
            QueueWithPriorities<int> queue = new QueueWithPriorities<int>();

            queue.Enqueue(12, 1);
            queue.Enqueue(2, 1);
            queue.Enqueue(4, 5);
            Console.WriteLine(queue.GetMaxPriority());
            Console.ReadKey();
        }
    }
}
