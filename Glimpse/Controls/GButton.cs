//
//  GButton.cs
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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Glimpse.Managers;
using Glimpse.Input;

namespace Glimpse.Controls
{
	public class GButton : Control
	{
		public GButton ()
		{
		}

		private bool _mouse_entered = false;

		#region implemented abstract members of Control

		public override event InterfaceHandler updating;
		public override event InterfaceHandler drawing;
		public override event InterfaceHandler mouse_click;
		public override event InterfaceHandler mouse_enter;
		public override event InterfaceHandler mouse_press;
		public override event InterfaceHandler mouse_leave;

		public override void init(){
		}

		public override void load(ContentManager content){
			this.background = content.Load<Texture2D>(this.background_name);
		}

		public override void update (int elapsed_time)
		{
			if (_mouse_entered) {
				if(!bounds.Contains (InputManager.get_interface_args ().current_mouse_state.Position)){
					_mouse_entered = false;
					mouse_leave (this, InputManager.get_interface_args ());
				}
			}

			if(updating != null)
				updating(this, InputManager.get_interface_args ());
		}

		public override void draw (SpriteBatch sprite_batch){
			
			sprite_batch.Draw (this.background, this.bounds, this.background_color * this.transparency);
			//TODO: fix positioning
			sprite_batch.DrawString (FontManager.fonts[this.font_name], this.text, this.bounds.Location.ToVector2 (), this.text_color);

			if(drawing != null)
				drawing(this, InputManager.get_interface_args ());
		}

		public override void clean_up ()
		{
			this.updating = null;
			this.drawing = null;
			this.mouse_click = null;
			this.mouse_enter = null;
			this.mouse_press = null;
			this.mouse_leave = null;
		}

		public override void reload ()
		{
			throw new NotImplementedException ();
		}

		public override void resize ()
		{
			throw new NotImplementedException ();
		}

        public override void handle_events(InterfaceArgs args)
        {
			if (bounds.Contains (args.current_mouse_state.Position) &&
			   !bounds.Contains (args.previous_mouse_state.Position)) {
				if (mouse_enter != null) {
					_mouse_entered = true;
					mouse_enter (this, args);
				}
			}

			if (args.current_mouse_state.LeftButton == ButtonState.Released &&
				args.previous_mouse_state.LeftButton == ButtonState.Pressed){
				if(mouse_click != null){
					mouse_click (this, args);
				}
			}

			if (args.current_mouse_state.LeftButton == ButtonState.Pressed &&
			   args.previous_mouse_state.LeftButton == ButtonState.Pressed) {
				if (mouse_press != null)
					mouse_press (this, args);
			}

        }

        #endregion
    }
}

