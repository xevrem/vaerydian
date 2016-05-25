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
		private int _next_type_id = 0;
		private ECSInstance _ecs_instance;

		public ComponentManager (ECSInstance instance)
		{
			this._ecs_instance = instance;
			this._components = new Bag<Bag<Component>> ();
		}

		public void register_component_type(Component component){
			component.type_id = _next_type_id++;
			this._components [component.type_id] = new Bag<Component> ();
		}

		public Component get_component(Entity e, int component_type){
			//TODO
			return default(Component);
		}

		public void add_component(Entity e, Component c){
			c.owner_id = e.id;
			this._components[c.type_id].set (e.id, c);
		}

		public void remove_components(Entity e){
			for(int i=0; i < this._components.count;i++) {
				this._components[i].set (e.id, null);
			}
		}

		public void remove_component(Component c){
			this._components[c.type_id].set(c.owner_id, null);
		}

		public void delete_entity(Entity e){
			remove_components (e);
		}

		public bool has_component(Entity e, int type_id){
			if (this._components [type_id] [e.id] != null) {
				return true;
			} else {
				return false;
			}
		}
	}
}

