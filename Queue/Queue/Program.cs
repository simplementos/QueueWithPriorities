using System;
using System.Linq;

namespace Queue
{
    class Program
    {
        static Random rand = new Random();
        
        static void Test()
        {
            var queue = new QueueWithPriorities<char>();

            for (var i = 0; i < 20; i++)
            {
                var item = (char)rand.Next(50, 80);
                var priority = rand.Next(1, 6);
                queue.Enqueue(item, priority);
            }

            Console.WriteLine("Items in queue:");
            for (var k = 0; k < queue.Count(); k++)
            {
                if (k == queue.Count() / 2) Console.WriteLine(); ;
                Console.Write(queue[k] + "  ");
            }
            Console.WriteLine($"Count of items: { queue.Count() }");

            Console.WriteLine(new string('-', 30));

            Console.WriteLine("Dequeuing some items with priority 3:");
            Console.WriteLine(new string('-', 30));

            while (queue.IsSuchPriorityInQueue(3))
            {
                Console.WriteLine($"Dequeuing item: { queue.Dequeue(3) }  ");
            }

            Console.WriteLine(new string('-', 30));
            Console.WriteLine("Items in queue after dequeuing:");
            for (var k = 0; k < queue.Count(); k++)
            {
                if (k == queue.Count() / 2) Console.WriteLine(); ;
                Console.Write(queue[k] + "  ");
            }
            Console.WriteLine($"\n\nCount of items: { queue.Count() }");

            Console.WriteLine($"\nPeek of queue: { queue.Peek() }");

            Console.ReadKey();
        }
 
        static void Main(string[] args)
        {
            Test();
        }
    }
}
