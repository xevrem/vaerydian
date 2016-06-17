//
//  GLabel.cs
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Glimpse.Input;
using Glimpse.Managers;

namespace Glimpse.Controls
{
	public class GLabel : Control
	{
		private int _spacing;
		private Vector2 _text_lengths;
		private Vector2 _text_position;

		public GLabel ()
		{
		}

		#region implemented abstract members of Control

		public override event InterfaceHandler updating;

		public override void init(){
			_spacing = FontManager.fonts[this.font_name].LineSpacing;
			_text_lengths = FontManager.fonts [this.font_name].MeasureString (this.text);

			if(autosize)
				this.bounds = new Rectangle(this.bounds.Location, new Point ((int) _text_lengths.X+2*this.border, (int)_text_lengths.Y+2*border));

			if (center_text) {
				_text_position.Y = this.bounds.Center.ToVector2 ().Y - (_text_lengths.Y / 2f);
				_text_position.X = this.bounds.Center.ToVector2 ().X - (_text_lengths.X / 2f);
			} else {
				_text_position = this.bounds.Location.ToVector2 ();
			}
		}

		public override void load(ContentManager content){
			this.background = content.Load<Texture2D>(this.background_name);
		}

		public override void update (int elapsed_time)
		{
			if (center_text) {
				_text_lengths = FontManager.fonts [this.font_name].MeasureString (this.text);
				_text_position.Y = this.bounds.Center.ToVector2 ().Y - (_text_lengths.Y / 2f);
				_text_position.X = this.bounds.Center.ToVector2 ().X - (_text_lengths.X / 2f);
			}

			if(updating != null)
				updating(this, InputManager.get_interface_args ());
		}

		public override void draw (SpriteBatch sprite_batch)
		{
			sprite_batch.Draw (this.background, this.bounds, this.background_color * this.transparency);

			sprite_batch.DrawString (FontManager.fonts[this.font_name], this.text, this._text_position, this.text_color);
		}

		public override void clean_up ()
		{
			this.updating = null;
		}

		public override void reload ()
		{
			throw new NotImplementedException ();
		}

		public override void resize ()
		{
			//throw new NotImplementedException ();
		}

        public override void handle_events(InterfaceArgs args)
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}

