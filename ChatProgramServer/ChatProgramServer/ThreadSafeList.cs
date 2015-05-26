using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProgramServer
{
    class ThreadSafeList<T> : IList<T>
    {
        private List<T> _list;
        private readonly object lockList = new object();

        public ThreadSafeList()
        {
            _list = new List<T>();
        }

        public ThreadSafeList(List<T> list) : this()
        {
            AddRange(list);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            lock(lockList)
            {
                _list.AddRange(collection);
            }
        }

        public int IndexOf(T item)
        {
            lock(lockList)
            {
                return _list.IndexOf(item);
            }
        }

        public void Insert(int index, T item)
        {
            lock(lockList)
            {
                _list.Insert(index, item);
            }
        }

        public void RemoveAt(int index)
        {
            lock(lockList)
            {
                _list.RemoveAt(index);
            }
        }

        public T this[int index]
        {
            get
            {
                lock(lockList)
                {
                    return _list[index];
                }
            }
            set
            {
                lock(lockList)
                {
                    _list[index] = value;
                }
            }
        }

        public void Add(T item)
        {
            lock(lockList)
            {
                _list.Add(item);
            }
        }

        public void Clear()
        {
            lock(lockList)
            {
                _list.Clear();
            }
        }

        public bool Contains(T item)
        {
            lock(lockList)
            {
                return _list.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock(lockList)
            {
                _list.CopyTo(array, arrayIndex);
            }
        }

        public int Count
        {
            get
            {
                lock (lockList)
                {
                    return _list.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            lock(lockList)
            {
                return _list.Remove(item);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Clone().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<T> Clone()
        {
            lock (lockList)
            {
                return new List<T>(_list);
            }
        }
    }
}
