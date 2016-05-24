/*
 Author:
      Erika V. Jonell <@xevrem>
 
 Copyright (c) 2013, 2014, 2015, 2016 Erika V. Jonell

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
        private int _Duration;

        private int _ElapsedTime;

        private ComponentMapper _PositionMapper;
        private ComponentMapper _ViewPortMapper;

        public DialogTimer(int duration, ECSInstance ecsInstance)
        {
            _Duration = duration;
            _PositionMapper = new ComponentMapper(new Position(), ecsInstance);
            _ViewPortMapper = new ComponentMapper(new ViewPort(), ecsInstance);
        }

        /// <summary>
        /// updates the location of the control according to the location of the caller.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="args"></param>
        public void updateHandler(Control control, InterfaceArgs args)
        {
            _ElapsedTime += control.ecs_instance.ElapsedTime;

            if (_ElapsedTime >= _Duration)
                control.ecs_instance.delete_entity(control.owner);

            Position pos = (Position)_PositionMapper.get(control.caller);
            ViewPort camera = (ViewPort)_ViewPortMapper.get(control.ecs_instance.tag_manager.get_entity_by_tag("CAMERA"));

            

            if (pos != null)
            {
                Vector2 pt = pos.Pos - camera.getOrigin();
                control.bounds = new Rectangle((int)pt.X, (int)pt.Y - control.bounds.Height - 16, control.bounds.Width, control.bounds.Height);
            }
        }

    }
}
