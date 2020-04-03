using System.Collections.Generic;

namespace Task4
{
    public class Queue
    {

        private readonly List<Goblin> _queue;

        public Queue() => _queue = new List<Goblin>();

        public void Add(Goblin goblin)
        {
            if(goblin.Range == 0)
                _queue.Add(goblin);
            else
                _queue.Insert(_queue.Count / 2, goblin);
        }

        public Goblin Peek()
        {
            if (_queue.Count == 0)
                return null;
            Goblin goblin = _queue[0];
            _queue.RemoveAt(0);
            return goblin;
        }
    }
}