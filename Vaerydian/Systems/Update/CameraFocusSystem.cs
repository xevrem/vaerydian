﻿/*
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

using ECSFramework;


using Vaerydian.Components;
using Vaerydian.Components.Spatials;
using Vaerydian.Components.Utils;
using Vaerydian.Components.Graphical;

namespace Vaerydian.Systems.Update
{
    class CameraFocusSystem : EntityProcessingSystem
    {

        private ComponentMapper _PositionMapper;
        private ComponentMapper _CameraFocusMapper;
        private ComponentMapper _ViewportMapper;

        private Entity _Camera;

        public CameraFocusSystem() : base() { }

		protected override void initialize()
        {
            _PositionMapper = new ComponentMapper(new Position(), ecs_instance);
            _CameraFocusMapper = new ComponentMapper(new CameraFocus(), ecs_instance);
            _ViewportMapper = new ComponentMapper(new ViewPort(), ecs_instance);
        }

		protected override void pre_load_content(Bag<Entity> entities)
        {
            _Camera = ecs_instance.tag_manager.get_entity_by_tag("CAMERA");
        }

        protected override void process(Entity entity)
        {
            Position focusPosition = (Position) _PositionMapper.get(entity);
            CameraFocus focus = (CameraFocus) _CameraFocusMapper.get(entity);
            ViewPort cameraView = (ViewPort)_ViewportMapper.get(_Camera);

            Vector2 cPos = cameraView.getOrigin();
            Vector2 center = cPos + cameraView.getDimensions() / 2;
            Vector2 fPos = focusPosition.Pos;
            float dist,radius;
            dist = Vector2.Distance(fPos,center);
            radius = focus.getFocusRadius();

            if (dist > radius)
            {
                Vector2 vec = Vector2.Subtract(fPos, center);
                vec.Normalize();

                cPos += Vector2.Multiply(vec, dist - radius);

                cameraView.setOrigin(cPos);
            }
        }
    }
}
