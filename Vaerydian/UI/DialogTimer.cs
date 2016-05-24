/*
 Author:
      Erika V. Jonell <@xevrem>
 
 Copyright (c) 2013 Erika V. Jonell

 This program is free software: you can redistribute it and/or modify
 it under the terms of the GNU Lesser General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public License
 along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Glimpse.Controls;

using ECSFramework;

using Vaerydian.Components.Spatials;
using Vaerydian.Components.Graphical;
using Glimpse.Input;

namespace Vaerydian.UI
{
    class DialogTimer
    {
        private int d_Duration;

        private int d_ElapsedTime;

        private ComponentMapper d_PositionMapper;
        private ComponentMapper d_ViewPortMapper;

        public DialogTimer(int duration, ECSInstance ecsInstance)
        {
            d_Duration = duration;
            d_PositionMapper = new ComponentMapper(new Position(), ecsInstance);
            d_ViewPortMapper = new ComponentMapper(new ViewPort(), ecsInstance);
        }

        /// <summary>
        /// updates the location of the control according to the location of the caller.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="args"></param>
        public void updateHandler(Control control, InterfaceArgs args)
        {
            d_ElapsedTime += control.ecs_instance.ElapsedTime;

            if (d_ElapsedTime >= d_Duration)
                control.ecs_instance.delete_entity(control.owner);

            Position pos = (Position)d_PositionMapper.get(control.caller);
            ViewPort camera = (ViewPort)d_ViewPortMapper.get(control.ecs_instance.tag_manager.get_entity_by_tag("CAMERA"));

            

            if (pos != null)
            {
                Vector2 pt = pos.Pos - camera.getOrigin();
                control.bounds = new Rectangle((int)pt.X, (int)pt.Y - control.bounds.Height - 16, control.bounds.Width, control.bounds.Height);
            }
        }

    }
}
