using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public abstract class Lists<T>
    {
        protected List<T> list;

        public Lists()
        {
            this.list = new List<T>();
        }

        public virtual void Add(T obj)
        {
            this.list.Add(obj);
        }

        public virtual bool Find(T obj, out int index)
        {
            index = FindItem(obj);

            if (index > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool Find(T obj)
        {
            if (FindItem(obj) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected int FindItem(T obj)
        {
            for (int i = 0; i < this.list.Count; i++)
            {
                if (obj.Equals(this.list[i]))
                    return i;
            }
            
            return -1;
        }

        public virtual bool Remove(T obj)
        {
            return this.list.Remove(obj);
        }

        public virtual void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
        }

        public virtual bool Update(T obj, int index)
        {
            try
            {
                this.list[index] = obj;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual void Clear()
        {
            this.list = new List<T>();
        }

        public virtual T GetAt(int index)
        {
            return this.list[index];
        }

        public int Count { get { return this.list.Count; } }
    }
}
