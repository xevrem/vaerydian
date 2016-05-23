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

namespace ECSFramework
{
	public abstract class EntitySystem
	{
		public ECSInstance ecs_instance;

		public EntitySystem ()
		{
		}

		public virtual void initialize(){}
		public abstract void preLoadContent(Bag<Entity> entities);
		public virtual void cleanUp(Bag<Entity> entities){}


		public void process(){
			begin();

			process_entities ();

			end ();
		}

		protected abstract void process_entities();
		protected virtual void added(Entity entity){}
		protected virtual void begin(){}
		protected virtual void end(){}
		protected virtual void removed (Entity entity){}


		//TODO
	}
}

