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
using Microsoft.Xna.Framework.Graphics;

using ECSFramework;

using Vaerydian;
using Vaerydian.Components;
using Vaerydian.Components.Spatials;
using Vaerydian.Components.Characters;
using Vaerydian.Components.Graphical;


namespace Vaerydian.Systems.Draw
{
    class HealthBarRenderSystem : EntityProcessingSystem
    {
        private GameContainer h_Container;

        private SpriteBatch _sprite_batch;

        private ComponentMapper h_PositionMapper;
        private ComponentMapper h_ViewportMapper;
        private ComponentMapper h_HealthMapper;

        private Entity h_Camera;

        private Texture2D h_Texture;


        public HealthBarRenderSystem(GameContainer container) 
        {
            h_Container = container;
            _sprite_batch = container.SpriteBatch;
        }

		protected override void initialize()
        {
            h_PositionMapper = new ComponentMapper(new Position(), ecs_instance);
            h_ViewportMapper = new ComponentMapper(new ViewPort(), ecs_instance);
            h_HealthMapper = new ComponentMapper(new Health(), ecs_instance);
        }
	
		protected override void pre_load_content(Bag<Entity> entities)
        {
            h_Texture = h_Container.ContentManager.Load<Texture2D>("export");

            h_Camera = ecs_instance.tag_manager.get_entity_by_tag("CAMERA");
        }

		protected override void begin ()
		{
			_sprite_batch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.PointClamp,DepthStencilState.Default,RasterizerState.CullNone);

			base.begin ();
		}

        protected override void process(Entity entity)
        {
            //grab components
            Position position = (Position)h_PositionMapper.get(entity);
            ViewPort camera = (ViewPort)h_ViewportMapper.get(h_Camera);
            Health health = (Health)h_HealthMapper.get(entity);

            //get vectors for easy working
            Vector2 pos = position.Pos;
            Vector2 origin = camera.getOrigin();

            //calculate current HP percentage
            float percentage = (float)health.CurrentHealth / (float)health.MaxHealth;

            //set the maximum x-distance that we have to draw, up to 32 pixels
            int max = (int)(32 * percentage);
            
            //construct the drawing region rectangle
            Rectangle rect = new Rectangle((int)(pos.X-origin.X),(int)(pos.Y - 10 -origin.Y),max,5);


            //draw the health bar
            _sprite_batch.Draw(h_Texture, rect, Color.Red);
        }

		protected override void end ()
		{
			_sprite_batch.End ();
			base.end ();
		}

    }
}
