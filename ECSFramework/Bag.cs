//
//  Bag.cs
//
//  Author:
//       erika <>
//
//  Copyright (c) 2016 erika
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;

namespace ECSFramework
{

	public class Bag<T>
	{
		private T[] _data;


		public Bag(int capacity = 16)
		{
			this._data = new T[capacity];
			this.count = 0;
		}


		public int capacity
		{
			get
			{
				return this._data.Length;
			}
		}
			
		public bool is_empty
		{
			get
			{
				return this.count == 0;
			}
		}


		public int count { get; private set; }


		public T this[int index]
		{
			get
			{
				return this._data[index];
			}

			set
			{
				if (index >= this._data.Length)
				{
					this.grow(index * 2);
					this.count = index + 1;
				}
				else if (index >= this.count)
				{
					this.count = index + 1;
				}

				this._data[index] = value;
			}
		}


		public void add(T element)
		{
			// is size greater than capacity increase capacity
			if (this.count == this._data.Length)
			{
				this.grow();
			}

			this._data[this.count] = element;
			++this.count;
		}


		public void add_range(Bag<T> range_of_elements)
		{
			for (int index = 0, j = range_of_elements.count; j > index; ++index)
			{
				this.add(range_of_elements.get(index));
			}
		}


		public void clear()
		{
			// Null all elements so garbage collector can clean up.
			for (int index = this.count - 1; index >= 0; --index)
			{
				this._data[index] = default(T);
			}

			this.count = 0;
		}

		public bool contains(T element)
		{
			for (int index = this.count - 1; index >= 0; --index)
			{
				if (element.Equals(this._data[index]))
				{
					return true;
				}
			}

			return false;
		}


		public T get(int index)
		{
			return this._data[index];
		}


		public T remove(int index)
		{
			// Make copy of element to remove so it can be returned.
			T result = this._data[index];
			--this.count;

			// Overwrite item to remove with last element.
			this._data[index] = this._data[this.count];

			// Null last element, so garbage collector can do its work.
			this._data[this.count] = default(T);
			return result;
		}


		public bool remove(T element)
		{
			for (int index = this.count - 1; index >= 0; --index)
			{
				if (element.Equals(this._data[index]))
				{
					--this.count;

					// Overwrite item to remove with last element.
					this._data[index] = this._data[this.count];
					this._data[this.count] = default(T);

					return true;
				}
			}

			return false;
		}


		public bool remove_all(Bag<T> bag)
		{
			bool isResult = false;
			for (int index = bag.count - 1; index >= 0; --index)
			{
				if (this.remove(bag.get(index)))
				{
					isResult = true;
				}
			}

			return isResult;
		}


		public T remove_last()
		{
			if (this.count > 0)
			{
				--this.count;
				T result = this._data[this.count];

				// default(T) if class = null.
				this._data[this.count] = default(T);
				return result;
			}

			return default(T);
		}


		public void set(int index, T element)
		{
			if (index >= this._data.Length)
			{
				this.grow(index * 2);
				this.count = index + 1;
			}
			else if (index >= this.count)
			{
				this.count = index + 1;
			}

			this._data[index] = element;
		}


		private void grow()
		{
			this.grow((int)(this._data.Length * 1.5) + 1);
		}


		private void grow(int newCapacity)
		{
			T[] oldElements = this._data;
			this._data = new T[newCapacity];
			Array.Copy(oldElements, 0, this._data, 0, oldElements.Length);
		}
	}

}

