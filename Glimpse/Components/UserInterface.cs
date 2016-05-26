//
//  UserInterface.cs
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
using Glimpse.Controls;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glimpse.Components
{
	public class UserInterface : Component
	{
		public UserInterface(){}

		public UserInterface (GForm form)
		{
			this.form = form;
		}

		private static int _type_id = 0;
		public override int type_id{ 
			get{ return _type_id;} 
			set{ _type_id = value;}
		}

		public GForm form;

		public void update(int elapsed_time){
			form.update (elapsed_time);
		}
			
		public void draw(SpriteBatch sprite_batch){
			form.draw (sprite_batch);
		}

		public void load(ContentManager content){
			form.load (content);
		}
	}
}

