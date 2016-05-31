//
//  EntityManager.cs
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
	public class EntityManager
	{
		private Bag<Entity> _entities;
		private Queue<int> _old_ids;
		private ECSInstance _ecs_instance;

		private int _next_id = 0;

		public EntityManager (ECSInstance instance)
		{
			this._ecs_instance = instance;
			this._entities = new Bag<Entity> ();
			this._old_ids = new Queue<int> ();
		}

		public Entity create(){
			Entity entity = new Entity();

			//re-use old IDs first
			if (this._old_ids.Count > 0) {
				entity.id = this._old_ids.Dequeue ();
			} else {
				entity.id = this._next_id++;
			}

			this._entities.set (entity.id, entity);

			return entity;
		}

		public int get_entity_count(){
			return _entities.count;
		}

		public void delete_entity(Entity e){
			this._old_ids.Enqueue(e.id);
			this._entities.set (e.id, null);
		}

		public void clean_up(){
			this._old_ids.Clear ();
			this._entities.clear ();
		}
	}
}

