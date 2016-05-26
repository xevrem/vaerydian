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

using ECSFramework;

namespace Vaerydian.Components.Audio
{
    class Audio : Component
    {

        private static int _type_id;
        private int _entity_id;

        public Audio() { }

        public Audio(String soundEffectName, bool playNow, float volume)
        {
            _SoundEffectName = soundEffectName;
            _Play = playNow;
            _Volume = volume;   
        }

        public Audio(String soundEffectName, bool playNow, float volume, float pitch)
        {
            _SoundEffectName = soundEffectName;
            _Play = playNow;
            _Volume = volume;
            _Pitch = pitch;
        }

		public override int type_id{ 
			get{ return _type_id;} 
			set{ _type_id = value;}
		}


        public int getEntityId()
        {
            return _entity_id;
        }

        public int getTypeId()
        {
            return _type_id;
        }

        public void setEntityId(int entityId)
        {
            _entity_id = entityId;
        }

        public void setTypeId(int typeId)
        {
            _type_id = typeId;
        }

        private String _SoundEffectName;

        public String SoundEffectName
        {
            get { return _SoundEffectName; }
            set { _SoundEffectName = value; }
        }

        private bool _Play = false;

        public bool Play
        {
            get { return _Play; }
            set { _Play = value; }
        }

        private bool _Loop = false;

        public bool Loop
        {
            get { return _Loop; }
            set { _Loop = value; }
        }

        private float _Volume = 1f;

        public float Volume
        {
            get { return _Volume; }
            set { _Volume = value; }
        }

        private float _Pitch = 0f;

        public float Pitch
        {
            get { return _Pitch; }
            set { _Pitch = value; }
        }


    }
}
