﻿//
//  GCanvas.cs
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
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Glimpse.Input;

namespace Glimpse.Controls
{
	public class GCanvas : Control
	{
		public List<Control> controls = new List<Control>();

		public GCanvas ()
		{
		}

		#region implemented abstract members of Control

		public override void init(){
			foreach (Control control in this.controls)
				control.init();
		}

		public override void load(ContentManager content){
			foreach (Control control in this.controls)
				control.load (content);
		}

		public override void update (int elapsed_time)
		{
			foreach(Control control in this.controls)
				control.update (elapsed_time);
		}
				

		public override void draw (SpriteBatch sprite_batch)
		{
			foreach(Control control in this.controls)
				control.draw (sprite_batch);
		}

		public override void clean_up ()
		{
			foreach (Control control in this.controls)
				control.clean_up ();

			this.controls.Clear ();

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
			foreach (Control control in this.controls) {
				if(control.bounds.Contains (args.current_mouse_state.Position))
					control.handle_events (args);
			}
        }
        #endregion
    }
}

