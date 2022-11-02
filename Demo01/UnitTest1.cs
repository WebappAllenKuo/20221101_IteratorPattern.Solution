using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Demo01
{
	public class Tests
	{
		[Test]
		public void Test1()
		{
			List<int> items = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
			IIterator<int> iterator = new CycleListIterator<int>(items);

			while (iterator.Count>0)
			{
				iterator.GetNext(5);
				int item = iterator.Pop();
				Console.Write(item + "\t");
			}
		}
	}

	
	public interface IIterator<T>
	{
		// T Current { get; }
		T GetNext();
		T GetNext(int offset);
		bool HasNext { get; }

		T Pop();
		int Count { get; }
	}

	public class CycleListIterator<T> : IIterator<T>
	{
		private readonly List<T> _data;
		private int index;
		
		public CycleListIterator(List<T> data)
		{
			_data = data;
			index = -1;
		}

		// public T Current => _data[index];

		public T GetNext()
			=> GetNext(1);

		public T GetNext(int offset=1)
		{
			//for (int i = 0; i < offset; i++)
			//{
			//	index++;
			//	if (index >= _data.Count) index = 0;
			//}

			index = (index + offset) % _data.Count;
			return _data[index];
		}

		public bool HasNext => true;
		public T Pop()
		{
			T result = _data[index];
			_data.RemoveAt(index);

			index = (index > 0) ? index - 1 : _data.Count - 1;

			return result;
		}

		public int Count => _data.Count;
	}
}