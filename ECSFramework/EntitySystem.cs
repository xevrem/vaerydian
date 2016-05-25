//
//  EntitySystem.cs
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

namespace ECSFramework
{
	public abstract class EntitySystem
	{
		public ECSInstance ecs_instance;
		public List<int> component_types;

		protected Bag<Entity> _entities;


		public EntitySystem ()
		{
			this.component_types = new List<int> ();
			this._entities = new Bag<Entity> ();
		}

		public void initialize_system(){
			initialize ();
		}

		public void load_content(){
			pre_load_content (this._entities);
		}

		public void remove_entity(Entity e){
			this._entities.remove(e);
			removed (e);
		}

		public void add_entity(Entity e){
			this._entities.add(e);
			added (e);
		}

		public void process(){
			begin();

			process_entities (this._entities);

			end ();
		}


		protected abstract void process_entities(Bag<Entity> entities);

		protected virtual bool should_process(){
			return true;
		}

		protected virtual void pre_load_content(Bag<Entity> entities){}
		protected virtual void cleanUp(Bag<Entity> entities){}
		protected virtual void initialize(){}
		protected virtual void added(Entity entity){}
		protected virtual void removed (Entity entity){}
		protected virtual void begin(){}
		protected virtual void end(){}




		//TODO
	}
}

