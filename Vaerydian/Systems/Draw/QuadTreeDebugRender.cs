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

using Vaerydian;
using Vaerydian.Components;
using Vaerydian.Utils;
using Vaerydian.Components.Spatials;
using Vaerydian.Components.Graphical;

namespace Vaerydian.Systems.Draw
{
    class QuadTreeDebugRenderSystem : EntityProcessingSystem
    {
        private GameContainer q_Contaner;
        private SpriteBatch _sprite_batch;

        private ComponentMapper q_SpatialMapper;
        private ComponentMapper q_PositionMapper;
        private ComponentMapper q_ViewPortMapper;

        private Entity q_Player;
        private Entity q_Camera;
        private Entity q_Spatial;

        private Texture2D q_Texture;
        
        public QuadTreeDebugRenderSystem(GameContainer container) 
        {
            q_Contaner = container;
            _sprite_batch = container.SpriteBatch;
        }

		protected override void initialize()
        {
            q_SpatialMapper = new ComponentMapper(new SpatialPartition(), ecs_instance);
            q_PositionMapper = new ComponentMapper(new Position(), ecs_instance);
            q_ViewPortMapper = new ComponentMapper(new ViewPort(), ecs_instance);
        }

		protected override void pre_load_content(Bag<Entity> entities)
        {
            q_Player = ecs_instance.tag_manager.get_entity_by_tag("PLAYER");
            q_Camera = ecs_instance.tag_manager.get_entity_by_tag("CAMERA");
            q_Spatial = ecs_instance.tag_manager.get_entity_by_tag("SPATIAL");

            q_Texture = q_Contaner.ContentManager.Load<Texture2D>("export");
        }

		protected override void begin ()
		{
			_sprite_batch.Begin ();
			base.begin ();
		}

        protected override void process(Entity entity)
        {
            Position position = (Position)q_PositionMapper.get(entity);
            ViewPort camera = (ViewPort)q_ViewPortMapper.get(q_Camera);
            SpatialPartition spatial = (SpatialPartition)q_SpatialMapper.get(q_Spatial);

            Vector2 pos = position.Pos + position.Offset;
            Vector2 origin = camera.getOrigin();
            QuadNode<Entity> node = spatial.QuadTree.locateNode(pos);

            int width = (int)(node.LRCorner.X - node.ULCorner.X);
            int height = (int)(node.LRCorner.Y - node.ULCorner.Y);

            Rectangle rec = new Rectangle((int)(node.ULCorner.X - origin.X), (int)(node.ULCorner.Y - origin.Y), width, height);

            _sprite_batch.Draw(q_Texture, rec, new Color(1f,0f,0f,0f));
        }

		protected override void end ()
		{
			_sprite_batch.End ();
			base.end ();
		}

    }
}
