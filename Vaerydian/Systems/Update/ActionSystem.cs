/*
 Author:
      Erika V. Jonell <@xevrem>
 
 Copyright (c) 2013 Erika V. Jonell

 This program is free software: you can redistribute it and/or modify
 it under the terms of the GNU Lesser General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public License
 along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;

using ECSFramework;

using Vaerydian.Components.Actions;
using Vaerydian.Utils;

namespace Vaerydian.Systems.Update
{
	public class ActionSystem : EntityProcessingSystem
	{
		private ComponentMapper a_ActionMapper;

		public ActionSystem ()
		{
		}

		public override void initialize ()
		{
			a_ActionMapper = new ComponentMapper (new VAction (), ecs_instance);
		}

		public override void preLoadContent (Bag<Entity> entities)
		{
			//throw new System.NotImplementedException ();
		}

		protected override void process (Entity entity)
		{
			VAction action = (VAction) a_ActionMapper.get(entity);

			action.doAction();

			ecs_instance.delete_entity (entity);
		}

		protected override void removed (Entity entity)
		{
			base.removed (entity);
		}

		public override void cleanUp (Bag<Entity> entities)
		{
			//do nothing for now
		}

		private void foo(){}
	}
}

