using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    class QueueWithPriorities<T> : IEnumerable<Tuple<T, int>>
    {
        private Tuple<T, int>[] _queues;
        private List<Tuple<T, int>> _queues2;
        private int[] queuesCounts;
        private int _count;
        private int _capasity;


        public int Count => _count;

        public bool Empty => (_count == 0) ? true : false;

        public int GetMaxPriority() => _queues.Where(q => q != null).Select(q => q.Item2).Max();

        

        public void Enqueue(T item, int priority)
        {
            if (priority <= 0) throw new ArgumentOutOfRangeException($"{ nameof(priority) } must be natural number.");

            if (_count == _capasity)
            {
                _capasity *= 2;
                var temp = new Tuple<T, int>[_capasity];
                Array.Copy(_queues, temp, _count);
                _queues = temp;
            }
            _queues[_count++] = Tuple.Create(item, priority);
            _queues2 = new List<Tuple<T, int>>();
            _queues2.Add(Tuple.Create(item, priority));
        }

        public Tuple<T, int> Dequeue()
        {
            if (_count == 0) throw new InvalidOperationException("Queue is empty.");

            var temp = new Tuple<T, int>[_capasity];

            for (var i = 0; i < _count - 1; i++)
            {
                temp[i] = _queues[i + 1];
            }
            Tuple<T, int> returnValue = _queues[0];
            _queues = temp;
            return returnValue;
        }

        public Tuple<T, int> Dequeue(int priority)
        {
            if (_count ==   0) throw new InvalidOperationException("Queue is empty.");
            if (priority <= 0) throw new ArgumentOutOfRangeException($"{ nameof(priority) } must be natural number.");
            
            var temp = new Tuple<T, int>[_capasity];

            for (var i = 0; i < _count - 1; i++)
            {
                temp[i] = _queues[i + 1];
            }
            Tuple<T, int> returnValue = _queues[0];
            _queues = temp;

            return returnValue;
        }

        public Tuple<T, int> Peek(int priority = 1)
        {
            if (_count ==   0) throw new InvalidOperationException("Queue is empty.");
            if (priority <= 0) throw new ArgumentOutOfRangeException($"{ nameof(priority) } must be natural number.");
            
            return _queues[0];
        }

        public IEnumerator<Tuple<T, int>> GetEnumerator()
        {
            for (var i = 0; i < _count; i++)
            {
                yield return _queues[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = 0; i < _count; i++)
            {
                yield return _queues[i];
            }
        }

        public QueueWithPriorities()
        {
            _capasity = 8;
            _queues = new Tuple<T, int>[_capasity];
        }

        public QueueWithPriorities(int capasity)
        {
            _queues = new Tuple<T, int>[_capasity];
        }

    }
}
