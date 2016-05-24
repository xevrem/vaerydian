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
using Vaerydian.Components.Graphical;

namespace Vaerydian.Systems.Draw
{
    class DeferredSystem : EntityProcessingSystem
    {
        private GameContainer _Container;
        private SpriteBatch _SpriteBatch;
        private Effect _CombinedEffect;
        private ComponentMapper _GeometryMapper;

        public DeferredSystem(GameContainer container) 
        {
            _Container = container;
            _SpriteBatch = _Container.SpriteBatch;
        }

        public override void initialize()
        {
            _GeometryMapper = new ComponentMapper(new GeometryMap(), ecs_instance);
        }

        public override void preLoadContent(Bag<Entity> entities)
        {
            _CombinedEffect = _Container.ContentManager.Load<Effect>("effects\\DiferredCombine");
        }

        public override void cleanUp(Bag<Entity> entities) { }

        protected override void process(Entity entity)
        {
            GeometryMap geometry = (GeometryMap)_GeometryMapper.get(entity);

            //setup effect parameters and techniques
            _CombinedEffect.CurrentTechnique = _CombinedEffect.Techniques["Combine"];
            _CombinedEffect.Parameters["ambient"].SetValue(1f);
            _CombinedEffect.Parameters["lightAmbient"].SetValue(4);
            _CombinedEffect.Parameters["ambientColor"].SetValue(geometry.AmbientColor);
            _CombinedEffect.Parameters["ColorMap"].SetValue(geometry.ColorMap);
            _CombinedEffect.Parameters["ShadingMap"].SetValue(geometry.ShadingMap);
            _CombinedEffect.Parameters["NormalMap"].SetValue(geometry.NormalMap);
            _CombinedEffect.CurrentTechnique.Passes[0].Apply();

            _SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, _CombinedEffect);
            _SpriteBatch.Draw(geometry.ColorMap, Vector2.Zero, Color.White);
            _SpriteBatch.End();
        }
    }
}
