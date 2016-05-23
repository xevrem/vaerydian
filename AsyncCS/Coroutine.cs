//
//  Coroutine.cs
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
using System.Collections.Generic;

namespace AsyncCS
{
	public abstract class Coroutine{

		private IEnumerator<object> _enumerator;
		private bool _do_sub = false;
		private Coroutine _sub_coroutine;
		private object _input = null;
		private object _sub_input = null;
		public bool is_complete = false;
		public bool can_move_next = true;


		public Coroutine(){
			this._enumerator = this.process().GetEnumerator();
		}    

		public object YieldFrom(Coroutine coroutine, object sub_input=null){
			this._do_sub = true;
			this._sub_coroutine = coroutine;
			this._sub_input = sub_input;
			return this._sub_coroutine.next();
		}

		public object YieldComplete(object return_value=null){
			this.is_complete = true;
			return return_value;
		}

		public object next(object in_value=null){
			if (this._do_sub) {
				if (this._sub_coroutine.can_move_next && !this._sub_coroutine.is_complete)
					return this._sub_coroutine.next (this._sub_input);
				else {
					this._do_sub = false;
					this._input = in_value;
					this.can_move_next = this._enumerator.MoveNext ();
					return this._enumerator.Current;
				}
			} else {
				this._input = in_value;
				this.can_move_next = this._enumerator.MoveNext ();
				return this._enumerator.Current;
			}
		}

		public void initialize(object input){
			this._input = input;
		}

		public abstract IEnumerable<object> process();
	}
}

