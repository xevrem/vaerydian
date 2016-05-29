//
//  TagManager.cs
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
	public class TagManager
	{
		private Dictionary<string, Entity> tagged_entities;

		public TagManager ()
		{
			this.tagged_entities = new Dictionary<string, Entity> ();
		}

		public Entity get_entity_by_tag(string name){
			//TODO
			return this.tagged_entities[name];
		}

		public void tag_entity(string tag, Entity e){
			this.tagged_entities.Add (tag, e);
		}

		public void refresh(Entity e){
			//TODO
		}

		public void delete_entity(Entity e){
			//TODO

		}

	}
}

