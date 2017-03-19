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
static        Random rand = new Random();

        static async Task Foo(CancellationToken token)
        {
            for(int i = 0; i < 1000; i++)
            {
                Console.WriteLine(i);
                await Task.Delay(1000, token);
                token.ThrowIfCancellationRequested();
            }
        }
        static void Main(string[] args)
        {
            QueueWithPriorities<int> queue = new QueueWithPriorities<int>();
            List<Tuple<int, int>> ss = new List<Tuple<int, int>>();
            for (var i = 0; i < 222; i++)
            {
                int t1 = rand.Next(1, 5), t2 = rand.Next(1, 17);
              queue.Enqueue(t1, t2);
                ss.Add(Tuple.Create(t1, t2));
             }
            int y = 0;

            var queue2 = new QueueWithPriorities<int>();
            for (int f = 0; f < ss.Count; f++) queue2.Enqueue(ss[f].Item1, ss[f].Item2);
            foreach (var item in queue)
            {
                Console.WriteLine(ss[y] +   "      " +  item);
                y++;
            }
            Console.WriteLine("peek" + queue.Peek(-16));

            Console.ReadKey();
        }
    }
}
