using System.Collections.Generic;
using System.Linq;

namespace GenericsHomework
{ 

	public class Circle<T> where T : class
	{
		public string Name { get; }
		private readonly HashSet<T> _items = new();

		public Circle(string name) => Name = name;
		public bool Add(T item) => _items.Add(item);
		public bool Contains(T item) => _items.Contains(item);
		public IEnumerable<T> Items => _items;
    }

	public class VennDiagram<T> where T: class
	{
		private readonly Dictionsary<string, Circle<T>> _circles = new();
		public IEnumerable<Circle<T>> Circles => _circles.Values;
	}
}