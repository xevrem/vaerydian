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

using Glimpse.Input;
using Glimpse.Managers;
using Vaerydian.Components.Spatials;
using Vaerydian.Components.Actions;
using Vaerydian.Components.Graphical;

namespace Vaerydian.Systems.Draw
{
    public class FloatingTextDisplaySystem : EntityProcessingSystem
    {
        private GameContainer _Container;
        private SpriteBatch _SpriteBatch;

        private ComponentMapper _DamageMapper;
        private ComponentMapper _PositionMapper;
        private ComponentMapper _ViewPortMapper;
        private ComponentMapper _FloatMapper;

        private SpriteFont _Font;

        private Entity _Camera;

        public FloatingTextDisplaySystem(GameContainer container)
        {
            _Container = container;
            _SpriteBatch = container.SpriteBatch;
        }


        public override void initialize()   
        {
            _DamageMapper = new ComponentMapper(new Damage(), ecs_instance);
            _PositionMapper = new ComponentMapper(new Position(), ecs_instance);
            _ViewPortMapper = new ComponentMapper(new ViewPort(), ecs_instance);
            _FloatMapper = new ComponentMapper(new FloatingText(), ecs_instance);
        }
        
        public override void preLoadContent(Bag<Entity> entities)
        {
            _Font = FontManager.fonts["Damage"];
            _Camera = ecs_instance.tag_manager.get_entity_by_tag("CAMERA");
        }

        public override void cleanUp(Bag<Entity> entities) { }

        protected override void process(Entity entity)
        {
            //Damage damage = (Damage)_DamageMapper.get(entity);
            FloatingText text = (FloatingText)_FloatMapper.get(entity);

            if (text == null)
                return;
            else
            {
                //see if we should destroy this
                text.ElapsedTime += ecs_instance.ElapsedTime;
                if (text.ElapsedTime >= text.Lifetime)
                {
                    ecs_instance.delete_entity(entity);
                    return;
                }
            }

            
            Position position = (Position)_PositionMapper.get(entity);//damage.Target);

            if (position == null || text == null)
                return;

            ViewPort camera = (ViewPort)_ViewPortMapper.get(_Camera);
            Vector2 origin = camera.getOrigin();
            Vector2 pos = position.Pos + new Vector2(0, -text.ElapsedTime / 7);

            //String dmg;
            //Color color = Color.Yellow;
            
            /*
            if (damage.DamageAmount == 0)
            {
                dmg = "miss";
                color = Color.White;
            }
            else
                dmg = "" + damage.DamageAmount;
            */

            float fade = 1f;
            float half = (float)text.Lifetime / 2f;

            if (text.ElapsedTime > half)
                fade = (1f - (text.ElapsedTime - half) / half);
            

            _SpriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.PointClamp,DepthStencilState.Default,RasterizerState.CullNone);
            
            //background
            _SpriteBatch.DrawString(_Font, text.Text, pos - origin + new Vector2(1, 0), Color.Black * fade);
            _SpriteBatch.DrawString(_Font, text.Text, pos - origin + new Vector2(-1, 0), Color.Black * fade);
            _SpriteBatch.DrawString(_Font, text.Text, pos - origin + new Vector2(0, 1), Color.Black * fade);
            _SpriteBatch.DrawString(_Font, text.Text, pos - origin + new Vector2(0, -1), Color.Black * fade);
            //foreground
            _SpriteBatch.DrawString(_Font, text.Text, pos - origin, text.Color * fade);

            _SpriteBatch.End();
        }


    }
}
