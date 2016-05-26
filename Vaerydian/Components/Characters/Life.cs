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

namespace Vaerydian.Components.Characters
{
	public struct LifeDef{
		public int DeathLongevity;
	}

    class Life : Component
    {

		private static int _type_id;
        private int l_entity_id;

        public Life() { }

		public override int type_id{ 
			get{ return _type_id;} 
			set{ _type_id = value;}
		}

        public int getEntityId()
        {
            return l_entity_id;
        }

        public int getTypeId()
        {
            return _type_id;
        }

        public void setEntityId(int entityId)
        {
            l_entity_id = entityId;
        }

        public void setTypeId(int typeId)
        {
            _type_id = typeId;
        }

        private bool _IsAlive = true;
        /// <summary>
        /// is the entity currently alive
        /// </summary>
        public bool IsAlive
        {
            get { return _IsAlive; }
            set { _IsAlive = value; }
        }

        private int _TimeSinceDeath = 0;

        /// <summary>
        /// total time spent "dead"
        /// </summary>
        public int TimeSinceDeath
        {
            get { return _TimeSinceDeath; }
            set { _TimeSinceDeath = value; }
        }

        private int _DeathLongevity;
        /// <summary>
        /// total time allowed to be "dead"
        /// </summary>
        public int DeathLongevity
        {
            get { return _DeathLongevity; }
            set { _DeathLongevity = value; }
        }


    }
}
