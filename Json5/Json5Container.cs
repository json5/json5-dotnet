using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Json5
{
    public abstract class Json5Container : Json5Value, IDictionary<string, Json5Value>
    {
        public abstract void Add(string key, Json5Value value);

        public abstract bool ContainsKey(string key);

        public abstract ICollection<string> Keys { get; }

        public abstract bool Remove(string key);

        bool IDictionary<string, Json5Value>.TryGetValue(string key, out Json5Value value)
        {
            value = null;
            if (!this.ContainsKey(key))
                return false;

            value = this[key];
            return true;
        }

        public abstract ICollection<Json5Value> Values { get; }

        void ICollection<KeyValuePair<string, Json5Value>>.Add(KeyValuePair<string, Json5Value> item)
        {
            this.Add(item.Key, item.Value);
        }

        public abstract void Clear();

        bool ICollection<KeyValuePair<string, Json5Value>>.Contains(KeyValuePair<string, Json5Value> item)
        {
            return this.ContainsKey(item.Key) && this[item.Key] == item.Value;
        }

        void ICollection<KeyValuePair<string, Json5Value>>.CopyTo(KeyValuePair<string, Json5Value>[] array, int arrayIndex)
        {
            this.ToArray().CopyTo(array, arrayIndex);
        }

        public abstract int Count { get; }

        bool ICollection<KeyValuePair<string, Json5Value>>.IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<KeyValuePair<string, Json5Value>>.Remove(KeyValuePair<string, Json5Value> item)
        {
            if (((ICollection<KeyValuePair<string, Json5Value>>)this).Contains(item))
                return this.Remove(item.Key);

            return false;
        }

        IEnumerator<KeyValuePair<string, Json5Value>> IEnumerable<KeyValuePair<string, Json5Value>>.GetEnumerator()
        {
            foreach (string key in this.Keys)
                yield return new KeyValuePair<string, Json5Value>(key, this[key]);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (string key in this.Keys)
                yield return new KeyValuePair<string, Json5Value>(key, this[key]);
        }
    }
}
