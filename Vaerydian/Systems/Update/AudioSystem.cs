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

//using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Audio;

using ECSFramework;



using Vaerydian;
using Vaerydian.Components.Audio;


namespace Vaerydian.Systems.Update
{
    class AudioSystem : EntityProcessingSystem
    {
        private GameContainer _Container;

        private ComponentMapper _AudioMapper;

        private Dictionary<String, SoundEffect> _SoundEffects = new Dictionary<String, SoundEffect>();

        public AudioSystem(GameContainer container) : base() 
        {
            _Container = container;
        }

		protected override void initialize()
        {
            _AudioMapper = new ComponentMapper(new Audio(), ecs_instance);
        }

		protected override void pre_load_content(Bag<Entity> entities)
        {
            for (int i = 0; i < entities.count; i++)
            {
				Audio audio = (Audio) _AudioMapper.get(entities.get(i));
                if(!_SoundEffects.ContainsKey(audio.SoundEffectName))
                    _SoundEffects.Add(audio.SoundEffectName, _Container.ContentManager.Load<SoundEffect>(audio.SoundEffectName));
            }
        }			

        protected override void added(Entity entity)
        {
            Audio audio = (Audio)_AudioMapper.get(entity);
            if (!_SoundEffects.ContainsKey(audio.SoundEffectName))
                _SoundEffects.Add(audio.SoundEffectName, _Container.ContentManager.Load<SoundEffect>(audio.SoundEffectName));
        }

        protected override void process(Entity entity)
        {
            Audio audio = (Audio)_AudioMapper.get(entity);

            if (!audio.Play)
                return;

            _SoundEffects[audio.SoundEffectName].Play(audio.Volume, audio.Pitch, 0f);

            audio.Play = false;
            ecs_instance.delete_entity(entity);
        }


    }
}
