using System;
using System.Collections.Generic;
using System.Linq;

namespace Json5
{
  public class Json5Array : Json5Container, IList<Json5Value>
  {
    private List<Json5Value> list = new List<Json5Value>();

    public int IndexOf(Json5Value item)
    {
      return this.list.IndexOf(item);
    }

    public void Insert(int index, Json5Value item)
    {
      this.list.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
      this.list.RemoveAt(index);
    }

    public override Json5Value this[int index]
    {
      get { return this.list[index]; }
      set { this.list[index] = value; }
    }

    public void Add(Json5Value item)
    {
      this.list.Add(item);
    }

    public bool Contains(Json5Value item)
    {
      return this.list.Contains(item);
    }

    void ICollection<Json5Value>.CopyTo(Json5Value[] array, int arrayIndex)
    {
      this.list.CopyTo(array, arrayIndex);
    }

    bool ICollection<Json5Value>.IsReadOnly
    {
      get { return ((ICollection<Json5Value>)this.list).IsReadOnly; }
    }

    public bool Remove(Json5Value item)
    {
      return this.list.Remove(item);
    }

    public IEnumerator<Json5Value> GetEnumerator()
    {
      return this.list.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    public override void Add(string key, Json5Value value)
    {
      this[int.Parse(key)] = value;
    }

    public override bool ContainsKey(string key)
    {
      int index = int.Parse(key);
      return index >= 0 && index < this.Count;
    }

    public override ICollection<string> Keys
    {
      get { return new List<string>(Enumerable.Range(0, this.Count).Select(i => i.ToString())); }
    }

    public override bool Remove(string key)
    {
      this.RemoveAt(int.Parse(key));
      return true;
    }

    public override ICollection<Json5Value> Values
    {
      get { return new List<Json5Value>(this); }
    }

    public override Json5Value this[string key]
    {
      get { return this[int.Parse(key)]; }
      set { this[int.Parse(key)] = value; }
    }

    public override void Clear()
    {
      this.list.Clear();
    }

    public override int Count
    {
      get { return this.list.Count; }
    }

    public override Json5Type Type
    {
      get { return Json5Type.Array; }
    }

    internal override string ToJson5String(string space, string indent)
    {
      string newLine = string.IsNullOrEmpty(space) ? "" : "\n";

      string s = "[" + newLine;

      foreach(Json5Value value in this)
        s += (value ?? Null).ToJson5String(space, indent + space) + "," + newLine;

      s += indent + "]";

      return s;
    }
  }
}
