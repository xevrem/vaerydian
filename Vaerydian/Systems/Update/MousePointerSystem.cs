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

using ECSFramework;


using Vaerydian.Components;

using Glimpse.Input;
using Vaerydian.Components.Spatials;
using Vaerydian.Components.Graphical;

namespace Vaerydian.Systems.Update
{
    class MousePointerSystem : EntityProcessingSystem
    {

        private ComponentMapper m_PositionMapper;
        private ComponentMapper m_ViewPortMapper;

        private Entity m_Camera;

        public MousePointerSystem() : base() { }

        public override void initialize()
        {
            m_PositionMapper = new ComponentMapper(new Position(), ecs_instance);
            m_ViewPortMapper = new ComponentMapper(new ViewPort(), ecs_instance);
        }

        public override void preLoadContent(Bag<Entity> entities)
        {
            m_Camera = ecs_instance.tag_manager.get_entity_by_tag("CAMERA");
        }

        public override void cleanUp(Bag<Entity> entities) { }

        protected override void process(Entity entity)
        {
            Position pos = (Position) m_PositionMapper.get(entity);

            ViewPort viewPort = (ViewPort)m_ViewPortMapper.get(m_Camera);
            //Vector2 center = viewPort.getDimensions() / 2;
            Vector2 origin = viewPort.getOrigin();

            pos.Pos = InputManager.getMousePositionVector()+origin;
        }
    }
}
