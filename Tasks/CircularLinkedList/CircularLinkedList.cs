using System.Collections;

namespace Tasks.CircularLinkedList
{
    public class CircularLinkedList<T> : IEnumerable<T>, ISort
        where T : IComparable
    {
        Node<T> _first;
        Node<T> _tail;
        int _count = 0;

        public bool IsEmpty
        {
            get
            {
                return _count == 0;
            }
        }

        public void Add(T value)
        {
            Node<T> node = new Node<T>(value, _first);

            if (_first == null)
            {
                _first = node;
                _first.Next = _first;
            }
            else _tail.Next = node;
            _tail = node;

            _count++;
        }

        public void AddRange(IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                Add(item);
            }
        }

        public bool Remove(T value)
        {
            Node<T> current = _first;
            Node<T> previous = null;
            int step = 0;

            while (step < _count)
            {
                if (current.Value.Equals(value))
                {
                    if (previous != null)
                        previous.Next = current.Next;
                    else
                    {
                        _first = _first.Next;
                        _tail.Next = _first;
                    }
                    _count--;
                    return true;
                }
                else
                {
                    previous = current;
                    current = current.Next;
                    step++;
                }
            }
            return false;
        }

        public void Clear()
        {
            _first = null;
            _tail = null;
            _count = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = _first;
            do
            {
                if (current != null)
                {
                    yield return current.Value;
                    current = current.Next;
                }
            }
            while (current != _first);
        }

        public void Order()
        {
            Node<T> current = _first;
            Node<T> previous = _tail;

            for (int j = 1; j < _count; j++)
            {
                for (int i = 0; i < _count - 1; i++)
                {
                    if (current.Value.CompareTo(current.Next.Value) > 0)
                    {
                        var temp = current.Next.Next;
                        current.Next.Next = current;
                        previous.Next = current.Next;
                        current.Next = temp;
                        previous = previous.Next;
                    }
                    else
                    {
                        previous = current;
                        current = current.Next;
                    }
                }
                previous = current;
                current = current.Next;
            }

            _tail = previous;
            _first = current;
        }

        public void OrderDescending()
        {
            throw new NotImplementedException();
        }

        public void Reverse()
        {
            throw new NotImplementedException();
        }
    }

    class Node<T>
    {
        public Node(T value, Node<T> next)
        {
            Value = value;
            Next = next;
        }

        public T Value { get; set; }
        public Node<T> Next { get; set; }
    }
}
