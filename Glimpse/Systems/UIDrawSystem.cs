//
//  UIDrawSystem.cs
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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Glimpse.Systems
{
	public class UIDrawSystem : EntityProcessingSystem
	{
		public UIDrawSystem ()
		{
		}

		public UIDrawSystem(ContentManager content_manager, GraphicsDevice graphics_device){
		}

		#region implemented abstract members of EntityProcessingSystem


		protected override void process (Entity entity)
		{
			throw new NotImplementedException ();
		}


		#endregion
	}
}

