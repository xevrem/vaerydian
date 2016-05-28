//
//  Control.cs
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
using Microsoft.Xna.Framework;
using Glimpse.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Glimpse.Controls
{
	public abstract class Control
	{
		public Entity owner;
		public Entity caller;
		public ECSInstance ecs_instance;

		//control attributes
		public Rectangle bounds;
		public string font_name;
		public string background_name;
		public Texture2D background;
		public int border = 0;
		public string text;
		public Color text_color = Color.Black;
		public bool center_text = false;
		public bool autosize = false;
		public Color background_color = Color.White;
		public float transparency = 1.0f;

		public event InterfaceHandler updating;
		public event InterfaceHandler drawing;
		public event InterfaceHandler mouse_click;
		public event InterfaceHandler mouse_hover;
		public event InterfaceHandler mouse_press;
		public event InterfaceHandler mouse_leave;

		public abstract void init();
		public abstract void load(ContentManager content);
		public abstract void update(int elapsed_time);
		public abstract void draw(SpriteBatch sprite_batch);
        public abstract void handle_events(InterfaceArgs args);
		public abstract void clean_up();
		public abstract void reload();
		public abstract void resize();
	}
}

