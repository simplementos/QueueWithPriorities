using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Queue
{
    class QueueWithPriorities<T> : IEnumerable<Tuple<T, int>>
    {
        private Tuple<T, int>[] _queues;
        private int[] _queuesCounts;
        private int _count;
        private int _capasity;

        public Tuple<T, int> this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                {
                    throw new ArgumentException($" '{ nameof(index) }' must be positive and less then the queue length.");
                }
                return _queues[index];
            }
        }

        public int Count => _count;

        public bool Empty => (_count == 0) ? true : false;

        public int GetMaxPriority() => _queues.Where(q => q != null).Select(q => q.Item2).Max();

        public bool IsSuchPriorityInQueue(int priority) => (_queues.Where(q => (q != null) && (q.Item2 == priority)).Count() > 0) ? true : false;
        


        public void Enqueue(T item, int priority)
        {
            if (priority <= 0)
            throw new ArgumentException($" '{ nameof(priority) }' must be more than zero.");
           
            if (_count == _capasity)
            {
                _capasity *= 2;
                var temp = new Tuple<T, int>[_capasity];
                Array.Copy(_queues, temp, _count);
                _queues = temp;
            }
            int nextPosition = _queuesCounts.Take(priority).Sum();

            if (_queues[nextPosition] != null)
            {
                var temp = new Tuple<T, int>[_capasity];
                Array.Copy(_queues, temp, _count);
                for (var i = nextPosition; i < _capasity - 1; i++)
                {
                    temp[i + 1] = _queues[i];
                }

                _queues = temp;
            }
            _queues[nextPosition] = Tuple.Create(item, priority);       
            
            if (priority > _queuesCounts.Length)
            {
                var pTemp = new int[priority * 2];
                Array.Copy(_queuesCounts, pTemp, _queuesCounts.Length);
                _queuesCounts = pTemp;
            }

            _queuesCounts[priority - 1]++;
            _count++;
        }

        public void Enqueue(T item)
        {            
            if (_count == _capasity)
            {
                _capasity *= 2;
                var temp = new Tuple<T, int>[_capasity];
                Array.Copy(_queues, temp, _count);
                _queues = temp;
            }
            int priority = (_count == 0) ? 1 : GetMaxPriority();

            int nextPosition = _queuesCounts.Take(priority).Sum();

            if (_queues[nextPosition] != null)
            {
                var temp = new Tuple<T, int>[_capasity];
                Array.Copy(_queues, temp, _count);
                for (var i = nextPosition; i < _capasity - 1; i++)
                {
                    temp[i + 1] = _queues[i];
                }

                _queues = temp;
            }
            _queues[nextPosition] = Tuple.Create(item, priority);

            if (priority > _queuesCounts.Length)
            {
                var pTemp = new int[priority * 2];
                Array.Copy(_queuesCounts, pTemp, _queuesCounts.Length);
                _queuesCounts = pTemp;
            }

            _queuesCounts[priority - 1]++;
            _count++;
        }


        public Tuple<T, int> Dequeue(int priority)
        {
            if (_count == 0) throw new InvalidOperationException("Queue is empty.");
            if ((priority <= 0) || !IsSuchPriorityInQueue(priority))
                throw new ArgumentException($" '{ nameof(priority) }' must be more than zero. Also the item with such priority must exist."); 

            var temp = new Tuple<T, int>[_capasity];

            int positionFromWhichDequeue = _queuesCounts.Take(priority - 1).Sum();
            for (var i = 0; i < _count - 1; i++)
            {
                temp[i] = (i < positionFromWhichDequeue) ? _queues[i] : _queues[i + 1];
            }
            Tuple<T, int> returnValue = _queues[_queuesCounts.Take(priority - 1).Sum()];
            _queues = temp;

            _queuesCounts[priority - 1]--;
            _count--;

            return returnValue;
        }

        public Tuple<T, int> Dequeue()
        {
            if (_count == 0) throw new InvalidOperationException("Queue is empty.");

            _queuesCounts[_queues[0].Item2 - 1]--;

            var temp = new Tuple<T, int>[_capasity];         

            for (var i = 0; i < _count - 1; i++)
            {
                temp[i] = _queues[i + 1];
            }
            Tuple<T, int> returnValue = _queues[0];
            _queues = temp;
          
            _count--;

            return returnValue;
        }

        public Tuple<T, int> Peek(int priority = 1)
        {
            if (_count == 0) throw new InvalidOperationException("Queue is empty.");
            if ((priority <= 0) || !IsSuchPriorityInQueue(priority))
                throw new ArgumentException($" '{ nameof(priority) }' must be more than zero. Also the item with such priority must exist.");

            return _queues[_queuesCounts.Take(priority - 1).Sum()];
        }

        public Tuple<T, int> Peek()
        {
            if (_count == 0) throw new InvalidOperationException("Queue is empty.");        
               
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
            _queuesCounts = new int[8];
        }

        public QueueWithPriorities(int capasity)
        {
            _queues = new Tuple<T, int>[_capasity];
            _queuesCounts = new int[8];
        }

    }
}
