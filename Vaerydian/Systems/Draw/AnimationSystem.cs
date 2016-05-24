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

using ECSFramework;

using Vaerydian.Components.Utils;
using Vaerydian.Components.Graphical;
using Vaerydian.Components.Spatials;
using Vaerydian.Utils;

using Microsoft.Xna.Framework.Graphics;
using Vaerydian.Components.Characters;


namespace Vaerydian.Systems.Draw
{
    class AnimationSystem : EntityProcessingSystem
    {
        
        private ComponentMapper _CharacterMapper;
        private ComponentMapper _PositionMapper;
        private ComponentMapper _ViewportMapper;
        private ComponentMapper _LifeMapper;
        
        private GameContainer _Container;
        private SpriteBatch _SpriteBatch;

        private Dictionary<String, Texture2D> _Textures = new Dictionary<string, Texture2D>();

        private Entity _Camera;

        private Color _Color;

        public AnimationSystem(GameContainer container)
        {
            _Container = container;
        }
        
        public override void initialize()
        {
            _CharacterMapper = new ComponentMapper(new Character(), ecs_instance);
            _PositionMapper = new ComponentMapper(new Position(), ecs_instance);
            _ViewportMapper = new ComponentMapper(new ViewPort(), ecs_instance);
            _LifeMapper = new ComponentMapper(new Life(), ecs_instance);
        }

        public override void preLoadContent(Bag<Entity> entities)
        {
            _Camera = ecs_instance.tag_manager.get_entity_by_tag("CAMERA");
            _SpriteBatch = _Container.SpriteBatch;
        }

        protected override void added(Entity entity)
        {
            Character character = (Character)_CharacterMapper.get(entity);

               foreach (Bone bone in character.Skeletons[character.CurrentSkeleton].Bones)
            {
                if(!_Textures.ContainsKey(bone.TextureName))
                    _Textures.Add(bone.TextureName, _Container.ContentManager.Load<Texture2D>(bone.TextureName));
            }
        }

        protected override void process(Entity entity)
        {
            Character character = (Character)_CharacterMapper.get(entity);
            Position position = (Position)_PositionMapper.get(entity);
            ViewPort viewPort = (ViewPort)_ViewportMapper.get(_Camera);
            Life life = (Life)_LifeMapper.get(entity);

            _SpriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.PointClamp,DepthStencilState.Default,RasterizerState.CullNone);

            float fade = 1;
			
			//FIX this to come from a component, not calc'd here
            _Color = Color.White;

            if (life != null)
            {
                if (!life.IsAlive)
                {
                    fade = (1f - ((float)life.TimeSinceDeath / (float)life.DeathLongevity));
                    _Color = Color.Red;
                }
            }

            foreach (Bone bone in character.Skeletons[character.CurrentSkeleton].Bones)
            {
                updateTime(bone, ecs_instance.ElapsedTime);
                _SpriteBatch.Draw(_Textures[bone.TextureName], position.Pos + getKeyPosition(bone, character.CurrentAnimtaion) - viewPort.getOrigin() + bone.RotationOrigin,
                    null, _Color * fade, getKeyRotation(bone, character.CurrentAnimtaion), bone.RotationOrigin, 1f, SpriteEffects.None, 1f);
            }

            _SpriteBatch.End();

        }

        public override void cleanUp(Bag<Entity> entities)
        {
            
        }

        public void updateTime(Bone bone, int gameTime)
        {
            bone.ElapsedTime += gameTime;//.ElapsedGameTime.Milliseconds;

            if (bone.ElapsedTime >= bone.AnimationTime)
            {
                bone.ElapsedTime = 0;
            }
        }

        public Vector2 getKeyPosition(Bone bone, String animation)
        {
            for (int i = 0; i < bone.Animations[animation].Count; i++)
            {
                if (i > 0)
                {
                    if (bone.ElapsedTime <= bone.Animations[animation][i].KeyPercent * bone.AnimationTime && bone.ElapsedTime > bone.Animations[animation][i - 1].KeyPercent)
                        return bone.Origin + tweenKeyFramesPosition(bone, bone.Animations[animation][i - 1], bone.Animations[animation][i], bone.ElapsedTime);
                }
            }


            return bone.Origin + Vector2.Zero;
        }


        private Vector2 tweenKeyFramesPosition(Bone bone, KeyFrame a, KeyFrame b, int time)
        {
            float timeBetweenFrames = b.KeyPercent * bone.AnimationTime - a.KeyPercent * bone.AnimationTime;
            float timeAfterA = time - a.KeyPercent * bone.AnimationTime;
            float percentTween = timeAfterA / timeBetweenFrames;

            Vector2 aToB = b.KeyPosition - a.KeyPosition;

            return a.KeyPosition + (aToB * percentTween);
        }

        public float getKeyRotation(Bone bone, String animation)
        {
            for (int i = 0; i < bone.Animations[animation].Count; i++)
            {
                if (i > 0)
                {
                    if (bone.ElapsedTime <= bone.Animations[animation][i].KeyPercent * bone.AnimationTime && bone.ElapsedTime > bone.Animations[animation][i - 1].KeyPercent)
                        return tweenKeyFramesRotation(bone, bone.Animations[animation][i - 1], bone.Animations[animation][i], bone.ElapsedTime);
                }
            }
            return bone.Rotation;
        }


        private float tweenKeyFramesRotation(Bone bone, KeyFrame a, KeyFrame b, int time)
        {
            float timeBetweenFrames = b.KeyPercent * bone.AnimationTime - a.KeyPercent * bone.AnimationTime;
            float timeAfterA = time - a.KeyPercent * bone.AnimationTime;
            float percentTween = timeAfterA / timeBetweenFrames;

            float aToB = b.KeyRotation - a.KeyRotation;

            return a.KeyRotation + aToB * percentTween;
        }


    }
}
