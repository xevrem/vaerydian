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


using Vaerydian.Components;
using Vaerydian.Components.Characters;
using Vaerydian.Components.Spatials;
using Vaerydian.Components.Graphical;


namespace Vaerydian.Systems.Draw
{
    class SpriteRenderSystem : EntityProcessingSystem
    {

        private Dictionary<String,Texture2D> s_Textures = new Dictionary<String,Texture2D>();
        private GameContainer s_Container;
        private SpriteBatch _sprite_batch;
        private ComponentMapper s_PositionMapper;
        private ComponentMapper s_ViewportMapper;
        private ComponentMapper s_SpriteMapper;
        private ComponentMapper s_GeometryMapper;
        private ComponentMapper s_TranformMapper;
        private ComponentMapper s_LifeMapper;
        private Entity s_Geometry;

        private Entity s_Camera;

        private Color s_Color = Color.White;

        public SpriteRenderSystem(GameContainer gameContainer) : base() 
        {
            this.s_Container = gameContainer;
            this._sprite_batch = gameContainer.SpriteBatch;
        }

		protected override void initialize()
        {
            s_PositionMapper = new ComponentMapper(new Position(), ecs_instance);
            s_ViewportMapper = new ComponentMapper(new ViewPort(), ecs_instance);
            s_SpriteMapper = new ComponentMapper(new Sprite(), ecs_instance);
            s_GeometryMapper = new ComponentMapper(new GeometryMap(), ecs_instance);
            s_TranformMapper = new ComponentMapper(new Transform(), ecs_instance);
            s_LifeMapper = new ComponentMapper(new Life(), ecs_instance);
        }

		protected override void pre_load_content(Bag<Entity> entities)
        {
            Sprite sprite;
            String texName;
            
            //pre-load all known textures
			for (int i = 0; i < entities.count; i++)
            {
                sprite = (Sprite)s_SpriteMapper.get(entities.get(i));
                texName = sprite.TextureName;
                if(!s_Textures.ContainsKey(texName))
                    s_Textures.Add(texName, s_Container.ContentManager.Load<Texture2D>(texName));

            }

            s_Textures.Add("projectile", s_Container.ContentManager.Load<Texture2D>("projectile2"));

            //pre-load camera entity reference
            s_Camera = ecs_instance.tag_manager.get_entity_by_tag("CAMERA");
            //s_Geometry = ecs_instance.tag_manager.get_entity_by_tag("GEOMETRY");
        }

        protected override void added(Entity entity)
        {
            base.added(entity);

            Sprite sprite = (Sprite)s_SpriteMapper.get(entity);
            if (!s_Textures.ContainsKey(sprite.TextureName))
                s_Textures.Add(sprite.TextureName, s_Container.ContentManager.Load<Texture2D>(sprite.TextureName));
        }

		protected override void begin ()
		{
			_sprite_batch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.PointClamp,DepthStencilState.Default,RasterizerState.CullNone);

			base.begin ();
		}

        protected override void process(Entity entity)
        {
            
			Sprite sprite = (Sprite) s_SpriteMapper.get(entity);

			if (!sprite.Visible)
				return;

			Position position = (Position) s_PositionMapper.get(entity);
            
            ViewPort viewport = (ViewPort) s_ViewportMapper.get(s_Camera);
            //GeometryMap geometry = (GeometryMap)s_GeometryMapper.get(s_Geometry);
            Transform transform = (Transform)s_TranformMapper.get(entity);
            Life life = (Life)s_LifeMapper.get(entity);

            Vector2 pos = position.Pos;
            Vector2 offset = position.Offset;
            Vector2 origin = viewport.getOrigin();
            Vector2 center = viewport.getDimensions() / 2;

            float fade = 1;
            s_Color = sprite.Color;

            if (life != null)
            {
                if (!life.IsAlive)
                {
                    fade = (1f - ((float)life.TimeSinceDeath / (float)life.DeathLongevity));
                    s_Color = Color.Red;
                }
            }

            if(sprite.ShouldSystemAnimate)
                sprite.Column = sprite.SpriteAnimation.updateFrame(ecs_instance.ElapsedTime);


            //s_SpriteBatch.Draw(s_Textures[sprite.getTextureName()], pos+center , null, Color.White, 0f, origin, new Vector2(1), SpriteEffects.None,0f);
            if (transform != null)
            {
                _sprite_batch.Draw(s_Textures[sprite.TextureName], pos - origin + transform.RotationOrigin, new Rectangle(sprite.Column * sprite.Width, sprite.Row * sprite.Height, sprite.Width, sprite.Height), s_Color * fade, transform.Rotation, transform.RotationOrigin, new Vector2(1), SpriteEffects.None, 0f);
            }
            else
            {
                _sprite_batch.Draw(s_Textures[sprite.TextureName], pos - origin, new Rectangle(sprite.Column * sprite.Width, sprite.Row * sprite.Height, sprite.Width, sprite.Height), s_Color * fade, 0f, new Vector2(0, 0), new Vector2(1), SpriteEffects.None, 0f);
            }
        }

		protected override void end ()
		{
			_sprite_batch.End ();
			base.end ();
		}
    }
}
