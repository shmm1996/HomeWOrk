using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Task1
{
    public class LList<T> : ICollection<T>, IEnumerable<T>, ISerializable, IDeserializationCallback
    {
        public int Count { get; private set; }
        public bool IsReadOnly => false;

        private LListNode _firstNode;
        private LListNode _lastNode;

        public void Add(T item)
        {
            if (Count == 0)
                AddFirst(item);
            else
                AddLast(item);
        }

        public void AddAfter(T after, T item)
        {
            LListNode node = FindFirstNode(after);
            if(node == null)
                return;
            LListNode nodeNext = node.Next;
            node.Next = new LListNode
            {
                Value = item,
                Next = nodeNext,
                Last = node
            };
            nodeNext.Last = node.Next;
        }

        public void AddBefore(T after, T item)
        {
            LListNode node = FindFirstNode(after);
            if (node == null)
                return;
            LListNode nodeLast = node.Last;
            node.Next = new LListNode
            {
                Value = item,
                Next = node,
                Last = nodeLast
            };
            nodeLast.Next = node.Last;
        }

        public T Find(Func<T, bool> condition)
        {
            LListNode currentNode = _firstNode;
            while (currentNode != null)
            {
                if (condition.Invoke(currentNode.Value))
                    return currentNode.Value;
                currentNode = currentNode.Next;
            }
            return default;
        }

        public T FindLast(Func<T, bool> condition)
        {
            LListNode currentNode = _lastNode;
            while (currentNode != null)
            {
                if (condition.Invoke(currentNode.Value))
                    return currentNode.Value;
                currentNode = currentNode.Last;
            }
            return default;
        }

        public void AddFirst(T item)
        {
            _firstNode.Last = new LListNode
            {
                Value = item,
                Next = _firstNode,
                Last = null
            };
            _firstNode.Next = _firstNode;
            Count++;
        }

        public void AddLast(T item)
        {
            _firstNode.Last = new LListNode
            {
                Value = item,
                Next = null,
                Last = _lastNode
            };
            _lastNode.Last = _lastNode;
            Count++;
        }

        public void Clear()
        {
            _firstNode = null;
            _lastNode = null;
            Count = 0;
        }

        public bool Contains(T item) => FindFirstNode(item) != null;

        public void CopyTo(T[] array, int arrayIndex)
        {
            if(Count == 0)
                return;
            LListNode currentNode = _firstNode;
            for (int i = arrayIndex; i < array.Length; i++)
            {
                array[i] = currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        public IEnumerator<T> GetEnumerator() => new LListEnum(this);

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public void OnDeserialization(object sender)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            if (Count == 0)
                return false;
            LListNode currentNode = FindFirstNode(item);
            if (currentNode != null)
            {
                if (currentNode.Last != null)
                    currentNode.Last.Next = currentNode.Next;
                if (currentNode.Next != null)
                    currentNode.Next.Last = currentNode.Last;
                Count--;
                return true;
            }
            return false;
        }

        public bool RemoveFirst()
        {
            if (Count == 0)
                return false;
            _firstNode = _firstNode.Next;
            _firstNode.Last = null;
            Count--;
            return true;
        }

        public bool RemoveLast()
        {
            if (Count == 0)
                return false;
            _lastNode = _lastNode.Last;
            _lastNode.Next = null;
            Count--;
            return true;
        }

        private LListNode FindFirstNode(T item)
        {
            LListNode currentNode = _firstNode;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(item))
                    break;
                currentNode = currentNode.Next;
            }
            return currentNode;
        }

        IEnumerator IEnumerable.GetEnumerator() => new LListEnum(this);

        private class LListNode
        {
            public T Value { get; set; }
            public LListNode Next { get; set; }
            public LListNode Last { get; set; }
        }

        private class LListEnum : IEnumerator<T>
        {
            public T Current => _currentNode.Value;
            private LListNode _currentNode;
            private readonly LList<T> _list;

            object IEnumerator.Current => Current;

            public LListEnum(LList<T> list)
            {
                list = _list;
                Reset();
            }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                _currentNode = _currentNode.Next;
                return _currentNode.Next != null;
            }

            public void Reset() => _currentNode = _list._firstNode;
        }

    }
}
