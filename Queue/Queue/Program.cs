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
            List<Tuple<int, int>> ss = new List<Tuple<int, int>>(); int yy = 2;
            for (var i = 0; i < 1233; i++)
            {
                int t1 = rand.Next(1, 2), t2 = rand.Next(1, 22);
            
              queue.Enqueue(t1, t2);
                if (t2 >= 17) queue.Dequeue();
             
                ss.Add(Tuple.Create(t1, t2));
             }
            int y = 0;

            var queue2 = new QueueWithPriorities<int>();
             foreach (var item in queue)
            {
                Console.WriteLine(ss[y] +   "      " +  item); 
                y++;
            }
            queue.Enqueue(12, 1);
            queue.Enqueue(12, 1);
            queue.Enqueue(12, 1);
            queue.Enqueue(12, 1);
            queue.Enqueue(12, 1);
            queue.Dequeue();
            queue.Dequeue();
            queue.Enqueue(12, 1);
            queue.Enqueue(12, 1);
            queue.Enqueue(12, 1); queue.Enqueue(12, 1);
            queue.Enqueue(12, 1);
            queue.Enqueue(12, 1);
            queue.Dequeue();
            queue.Dequeue(); queue.Dequeue();
            queue.Dequeue();
            queue.Dequeue(); queue.Dequeue();
            queue.Dequeue(); queue.Dequeue();
            queue.Dequeue(); queue.Enqueue(12, 1);
            queue.Enqueue(12, 1);
            queue.Enqueue(12, 1);
            queue.Enqueue(12, 1); queue.Dequeue();
            queue.Dequeue(); queue.Dequeue();
            queue.Dequeue();
            queue.Enqueue(12, 1);
            queue.Enqueue(12, 1);
            queue.Enqueue(12, 1);
            queue.Enqueue(12, 1); queue.Enqueue(12, 1);
            queue.Enqueue(12, 1);
            queue.Enqueue(12, 1); queue.Dequeue(); queue.Dequeue();
            queue.Dequeue(); queue.Dequeue(); queue.Dequeue();
            queue.Dequeue(); queue.Dequeue(); queue.Dequeue();
            queue.Dequeue(); queue.Dequeue(); queue.Dequeue();
            queue.Dequeue(); queue.Dequeue(); queue.Dequeue();
            queue.Dequeue(); queue.Dequeue(); queue.Dequeue();
            queue.Dequeue();
            queue.Enqueue(12, 1);



            Console.WriteLine(queue.Count());  //    for (int i = 0; i < 23; i++) queue.Dequeue(13);
            Console.WriteLine(queue._queuesCounts.Sum());


            Console.ReadKey();
        }
    }
}
