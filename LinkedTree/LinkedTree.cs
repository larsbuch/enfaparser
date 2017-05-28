using System.Collections;
using System.Collections.Generic;


namespace LinkedTree
{
    public class LinkedTree<T> : IEnumerable
    {
        private Node<T> _head = null;

        public Node<T> Add(T value)
        {
            var node = new Node<T> { Value = value };

            if (_head == null)
            {
                _head = node;
            }
            else
            {
                var current = _head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = node; //new head
            }

            return node;
        }

        public T Remove(Node<T> node)
        {
            if (_head == null)
                return node.Value;

            if (_head == node)
            {
                _head = _head.Next;
                node.Next = null;
                return node.Value;
            }

            var current = _head;
            while (current.Next != null)
            {
                if (current.Next == node)
                {
                    current.Next = node.Next;
                    return node.Value;
                }

                current = current.Next;
            }

            return node.Value;
        }

        public IEnumerator<T> Enumerator()
        {
            var current = _head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return Enumerator();
        }
    }

    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }
    }
}