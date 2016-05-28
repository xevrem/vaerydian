//
//  GForm.cs
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
	public class GForm : Control
	{
		public List<GCanvas> canvas_controls = new List<GCanvas>();

		public GForm ()
		{
		}

		#region implemented abstract members of Control

		public override void init(){
		}

		public override void load(ContentManager content){
			foreach (GCanvas canvas in this.canvas_controls)
				canvas.load (content);
		}

		public override void update (int elapsed_time)
		{
            if (this.bounds.Contains(InputManager.getMousePosition())){
                this.handle_events(InputManager.get_interface_args());
            }

            foreach (GCanvas canvas in this.canvas_controls)
				canvas.update (elapsed_time);
		}

		public override void draw (SpriteBatch sprite_batch)
		{
			foreach (GCanvas canvas in this.canvas_controls)
				canvas.draw (sprite_batch);		
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

        public override void handle_events(InterfaceArgs args)
        {
            foreach (GCanvas canvas in this.canvas_controls)
            {
                if (canvas.bounds.Contains(args.state_container.current_mouse_state.Position)){
                    canvas.handle_events(args);
                }
            }
        }

        #endregion
    }
}

