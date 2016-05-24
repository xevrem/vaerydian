//
//  ComponentManager.cs
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
	public class ComponentManager
	{
		private Bag<Bag<Component>> _components;

		public ComponentManager ()
		{
			this._components = new Bag<Bag<Component>> ();
		}

		public void register_component_type(Component component){
			//TODO
		}

		public Component get_component(Entity e, int component_type){
			//TODO
			return default(Component);
		}

		public void add_component(Entity e, Component c){
			//TODO
		}

		public void remove_components(Entity e){
			//TODO
		}
	}
}

