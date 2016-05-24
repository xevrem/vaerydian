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
using Microsoft.Xna.Framework.Graphics;

using ECSFramework;


using Vaerydian.Components;
using Vaerydian.Components.Spatials;
using Vaerydian.Components.Graphical;

namespace Vaerydian.Systems.Draw
{
    class SpriteDepthSystem : EntityProcessingSystem
    {

        private Texture2D s_DepthTexture;
        private GameContainer s_Container;
        private SpriteBatch s_SpriteBatch;
        private ComponentMapper s_PositionMapper;
        private ComponentMapper s_ViewportMapper;
        private ComponentMapper s_SpriteMapper;
        private ComponentMapper s_GeometryMapper;
        private Entity s_Geometry;

        private Entity s_Camera;

        public SpriteDepthSystem(GameContainer gameContainer)
            : base() 
        {
            this.s_Container = gameContainer;
            this.s_SpriteBatch = gameContainer.SpriteBatch;
        }

        public override void initialize()
        {
            s_PositionMapper = new ComponentMapper(new Position(), ecs_instance);
            s_ViewportMapper = new ComponentMapper(new ViewPort(), ecs_instance);
            s_SpriteMapper = new ComponentMapper(new Sprite(), ecs_instance);
            s_GeometryMapper = new ComponentMapper(new GeometryMap(), ecs_instance);
        }

        public override void preLoadContent(Bag<Entity> entities)
        {
            s_DepthTexture = s_Container.ContentManager.Load<Texture2D>("depth");

            //pre-load camera entity reference
            s_Camera = ecs_instance.tag_manager.get_entity_by_tag("CAMERA");
            s_Geometry = ecs_instance.tag_manager.get_entity_by_tag("GEOMETRY");
        }

        public override void cleanUp(Bag<Entity> entities) { }

        protected override void process(Entity entity)
        {
            Position position = (Position) s_PositionMapper.get(entity);
            Sprite sprite = (Sprite) s_SpriteMapper.get(entity);
            ViewPort viewport = (ViewPort) s_ViewportMapper.get(s_Camera);
            GeometryMap geometry = (GeometryMap)s_GeometryMapper.get(s_Geometry);

            Vector2 pos = position.Pos;
            Vector2 origin = viewport.getOrigin();
            Vector2 center = viewport.getDimensions() / 2;

            s_SpriteBatch.Begin();

            s_SpriteBatch.Draw(s_DepthTexture, pos + center, null, Color.White, 0f, origin, new Vector2(1), SpriteEffects.None, 0f);

            s_SpriteBatch.End();
        }
    }
}
