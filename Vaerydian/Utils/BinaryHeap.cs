/*
 Author:
      Erika V. Jonell <@xevrem>
 
 Copyright (c) 2013, 2014, 2015, 2016 Erika V. Jonell

 This program is free software: you can redistribute it and/or modify
 it under the terms of the GNU Lesser General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public License
 along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vaerydian.Utils
{

    public class BinaryHeap<T>
    {
        /// <summary>
        /// data structure
        /// </summary>
        HeapCell<T>[] _Data = new HeapCell<T>[0];

        private int _Length;

        private int _Size;

        public int Size
        {
            get { return _Size; }
            set { _Size = value; }
        }

        public BinaryHeap()
        {
            _Size = 0;
            _Length = 16 * 2 + 2;
            _Data = new HeapCell<T>[_Length];


            for (int i = 0; i < _Length; i++)
            {
                _Data[i] = new HeapCell<T>();
            }
        }

        public BinaryHeap(int length)
        {
            _Size = 0;
            _Length = length * 2 + 2;
            _Data = new HeapCell<T>[_Length];

            for (int i = 0; i < _Length; i++)
            {
                _Data[i] = new HeapCell<T>();
            }
        }

        public HeapCell<T> this[int index]
        {
            get { return _Data[index]; }
            set { _Data[index] = value; }
        }

        /// <summary>
        /// adds data to the heap using the given sort-value
        /// </summary>
        /// <param name="value">value used to determine proper sort</param>
        /// <param name="data">data package to store</param>
        public void add(int value, T data)
        {
            add(new HeapCell<T>(value, data));
            return;
        }

        /// <summary>
        /// adds heapcell to the heap using the given sort-value
        /// </summary>
        /// <param name="cell">heapcell to be used</param>
        private void add(HeapCell<T> cell)
        {
            _Size++;

            if ((_Size * 2 + 1) >= _Length)
                grow(_Size);

            _Data[_Size] = cell;

            int i = _Size;

            //do any needed swapping
            while (i != 1)
            {
                //compare cells
                if (_Data[i].Value <= _Data[i / 2].Value)
                {
                    //if i is less than i/2, swap
                    HeapCell<T> temp = _Data[i / 2];
                    _Data[i / 2] = _Data[i];
                    _Data[i] = temp;
                    i = i / 2;
                }
                else//otherwise break
                    break;
            }
        }

        public HeapCell<T> removeFirst()
        {
            HeapCell<T> retVal = _Data[1];

            //move last item to 1st position, reduce size by 1
            _Data[1] = _Data[_Size];
            _Data[_Size] = null;
            _Size--;

            int u, v;
            v = 1;

            //sort the heap
            while (true)
            {
                u = v;

                //if both children exist
                if ((2 * u + 1) <= _Size)
                {
                    //select lowest child
                    if (_Data[u].Value >= _Data[2 * u].Value)
                        v = 2 * u;
                    if (_Data[v].Value >= _Data[2 * u + 1].Value)
                        v = 2 * u + 1;
                }//if only one child exists
                else if (2 * u <= _Size)
                {
                    if (_Data[u].Value >= _Data[2 * u].Value)
                        v = 2 * u;
                }

                //do we need to swap or exit?
                if (u != v)
                {
                    HeapCell<T> temp = _Data[u];
                    _Data[u] = _Data[v];
                    _Data[v] = temp;
                }
                else
                {
                    break;//we've re-sorted the heap, so exit
                }
            }

            return retVal;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        private void grow(int size)
        {
            int length = size * 2 + 2;
            HeapCell<T>[] data = new HeapCell<T>[length];

            Array.Copy(_Data, data, _Length);

            _Data = data;
            _Length = length;

            return;
        }

        public void Clear()
        {
            for (int i = 0; i < _Size; i++)
            {
                _Data[i] = null;
            }

            _Size = 0;
        }

        public override String ToString()
        {
            String str = "";

            for (int i = 1; i < _Size + 1; i++)
            {
                str += _Data[i].Value + ", ";
            }

            return str;
        }
    }
}
