//
//  SystemManager.cs
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
	public class SystemManager
	{
		private List<EntitySystem> _systems;

		public SystemManager ()
		{
			this._systems = new List<EntitySystem> ();
		}

		public EntitySystem set_system(EntitySystem system, params Component[] components){
			//TODO add the system and assign its components.
			return system;
		}

		public void initialize_systems(){
			//TODO
		}

		public void systems_load_content(){
			//TODO
		}

		public void resolve(Entity e){
			//TODO: assign the entity to the appropriate systems.
		}

		public void delete_entity(Entity e){
			//TODO
		}
	}
}

