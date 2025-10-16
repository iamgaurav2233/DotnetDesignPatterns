using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Cache
{
    public class Node<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Node<TKey, TValue>? Next { get; set; } = null;
        public Node<TKey, TValue>? Prev { get; set; } = null;

    }
    public class LinkedList<TKey, TValue>
    {
        private Node<TKey, TValue>? _head;
        private Node<TKey, TValue>? _tail;

        public LinkedList()
        {
            _head = null;
            _tail = null;
        }
        public void AddNode(Node<TKey, TValue> val)
        {
            if (_head == null)
            {
                _head = val;
                _tail = val;
            }
            else
            {
                _head.Prev = val;
                val.Next = _head;
                _head = val;
            }
        }
        public void RemoveNode(Node<TKey, TValue> val)
        {

            if (val.Prev == null)
            {
                _head = null;
                _tail = null;
            }
            else if (val.Next == null)
            {
                val = val.Prev;
                val.Next = null;
            }
            else
            {
                val.Prev.Next = val.Next;
                val.Next.Prev = val.Prev;
                val.Prev = null;
                val.Next = null;
            }
        }
        public void RemoveLast(Dictionary<TKey, Node<TKey, TValue>> hashMap)
        {
            hashMap.Remove(_tail.Key);
            RemoveNode(_tail);
        }
        public void MoveAhead(Node<TKey, TValue> val)
        {
            RemoveNode(val);
            AddNode(val);
        }

    }
    public class LRU
    {
        private readonly int _capacity;
        private int CurrentCapacity { get; set; } = 0;
        private readonly Dictionary<int, Node<int, int>> _lruHashMap = new();
        private readonly LinkedList<int, int> _lruList;
        public LRU(int capacity)
        {
            _lruList = new LinkedList<int, int>();
            _capacity = capacity;
        }
        public int GetKey(int key)
        {
            if (_lruHashMap.ContainsKey(key))
            {
                _lruList.MoveAhead(_lruHashMap[key]);
                return _lruHashMap[key].Value;
            }
            return -1;
        }
        public void Put(int key, int value)
        {
            if (_lruHashMap.ContainsKey(key))
            {
                _lruHashMap[key].Value = value;
                _lruList.MoveAhead(_lruHashMap[key]);
            }
            else
            {
                _lruHashMap[key] = new Node<int, int> { Key = key, Value = value };
                _lruList.AddNode(_lruHashMap[key]);

                if (CurrentCapacity == _capacity)
                {
                    _lruList.RemoveLast(_lruHashMap);
                }
                CurrentCapacity++;
            }
        }
    }
    public class MyClass
    {
        class Background
        {
            public static void CleanLRU()
            {
                Console.WriteLine("This is callback");
            }
        }
        public static void Main(String[] args)
        {
            object obj = 10;
            var timer = new Timer(
            obj =>
            {
                Console.WriteLine(obj);
            },
            obj,
            TimeSpan.FromSeconds(1),
            TimeSpan.FromSeconds(5)
            );
            Console.ReadLine();
        }
    }
}