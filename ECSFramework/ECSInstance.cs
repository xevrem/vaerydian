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

		public ECSInstance ()
		{

		}

		public Entity create(){
			//TODO
			return new Entity();
		}

		public void refresh(Entity e){
			//TODO
		}

		public void delete_entity(Entity e){
			//TODO
		}

		public void resolve_entities(){
		}

		public void resolve_entities(Entity e){
			//TODO
		}

		public void clean_up(){
			//TODO
		}
	}
}

