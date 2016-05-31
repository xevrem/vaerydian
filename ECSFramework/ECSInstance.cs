//
//  ECSInstance.cs
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
	public class ECSInstance
	{
		public EntityManager entity_manager;
		public ComponentManager component_manager;
		public SystemManager system_manager;
		public TagManager tag_manager;
		public GroupManager group_manager;

		public int TotalTime;
		public int ElapsedTime;

		private Queue<Entity> _updating_entities;
		private Queue<Entity> _deleting_entities;

		public ECSInstance ()
		{
			this.entity_manager = new EntityManager (this);
			this.component_manager = new ComponentManager (this);
			this.system_manager = new SystemManager (this);
			this.tag_manager = new TagManager ();
			this.group_manager = new GroupManager ();
			this._updating_entities = new Queue<Entity> ();
			this._deleting_entities = new Queue<Entity> ();
		}

		public Entity create(){
			return this.entity_manager.create();
		}

		public void add_component(Entity e, IComponent c){
			this.component_manager.add_component(e,c);
		}

		public void remove_component(IComponent c){
			this.component_manager.remove_component (c);
		}

		public bool has_component(Entity e, int type_id){
			return this.component_manager.has_component (e, type_id);
		}

		public void resolve(Entity e){
			//TODO: add entity to an update list
			if (e != null)
				this._updating_entities.Enqueue (e);
		}

		public void delete_entity(Entity e){
			//TODO: add entity to a delete list
			if (e != null)
				this._deleting_entities.Enqueue (e);
		}

		public void resolve_entities(){
			//TODO: process updates
			if (this._updating_entities.Count > 0) {
				int size = this._updating_entities.Count;
				for(int i =0; i < size; i++){
					this.system_manager.resolve (this._updating_entities.Dequeue ());
				}
			}

			if (this._deleting_entities.Count > 0) {
				int size = this._deleting_entities.Count;

				for(int i =0; i < size; i++){
					Entity e = this._deleting_entities.Dequeue ();
					//TODO: procee deletions
					this.system_manager.delete_entity (e);
					this.tag_manager.delete_entity (e);
					this.group_manager.delete_entity (e);
					this.component_manager.delete_entity (e);
					this.entity_manager.delete_entity (e);
				}
			}
		}

		public void clean_up(){
			this.entity_manager.clean_up();
			this.component_manager.clean_up();
			this.system_manager.clean_up ();
			this.group_manager.clean_up ();
			this.tag_manager.clean_up ();
		}
	}
}

