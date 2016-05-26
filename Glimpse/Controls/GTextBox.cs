//
//  GTextBox.cs
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

namespace Glimpse.Controls
{
	public class GTextBox : Control
	{

		public GTextBox ()
		{
		}

		#region implemented abstract members of Control

		public override void init(){
		}

		public override void load(ContentManager content){
		}

		public override void update (int elapsed_time)
		{
			throw new NotImplementedException ();
		}

		public override void draw (SpriteBatch sprite_batch)
		{
			throw new NotImplementedException ();
		}

		public override void clean_up ()
		{
			throw new NotImplementedException ();
		}

		public override void reload ()
		{
			throw new NotImplementedException ();
		}

		public override void resize ()
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

