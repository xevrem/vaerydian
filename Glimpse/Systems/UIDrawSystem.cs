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
using Glimpse.Components;

namespace Glimpse.Systems
{
	public class UIDrawSystem : EntityProcessingSystem
	{
		private ComponentMapper _ui_mapper;
		private ContentManager _content_manager;
		private SpriteBatch _sprite_batch;

		public UIDrawSystem ()
		{
		}

		public UIDrawSystem(ContentManager content_manager, SpriteBatch sprite_batch){
			this._content_manager = content_manager;
			this._sprite_batch = sprite_batch;
		}

		#region implemented abstract members of EntityProcessingSystem

		protected override void initialize(){
			_ui_mapper = new ComponentMapper(new UserInterface(), ecs_instance);
		}

		protected override void pre_load_content(Bag<Entity> entities){
			for (int i = 0; i < entities.count; i++) {
				//UserInterface ui = (UserInterface) this._ui_mapper.get (entities.get(i));
				UserInterface ui = ComponentMapper.get<UserInterface> (entities[i]);
				ui.load (this._content_manager);
			}
		}

		protected override void begin(){
			this._sprite_batch.Begin ();
		}

		protected override void process (Entity entity)
		{
			UserInterface ui = (UserInterface) this._ui_mapper.get (entity);
			ui.draw (this._sprite_batch);
		}

		protected override void end(){
			this._sprite_batch.End ();
		}

		protected override void added (Entity entity)
		{
			UserInterface ui = ComponentMapper.get<UserInterface> (entity);
			ui.init ();
			ui.load (this._content_manager);
		}

		protected override void removed (Entity entity)
		{
			UserInterface ui = ComponentMapper.get<UserInterface> (entity);
			ui.clean_up ();
		}

		#endregion
	}
}

