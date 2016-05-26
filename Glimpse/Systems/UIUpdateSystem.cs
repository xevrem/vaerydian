//
//  UIUpdateSystem.cs
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
using ECSFramework;
using Glimpse.Components;

namespace Glimpse.Systems
{
	public class UIUpdateSystem : EntityProcessingSystem
	{
		private ComponentMapper _ui_mapper;

		public UIUpdateSystem ()
		{
		}

		#region implemented abstract members of EntityProcessingSystem


		protected override void initialize(){
			_ui_mapper = new ComponentMapper(new UserInterface(), ecs_instance);
		}

		protected override void process (Entity entity)
		{
			UserInterface ui = (UserInterface) this._ui_mapper.get (entity);
			ui.update ();
		}


		#endregion
	}
}

