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

namespace Vaerydian.Components.Utils
{
    class MapCollidable : Component
    {
		private static int _type_id;
        private int m_entity_id;

        public MapCollidable() { }

		public override int type_id{ 
			get{ return this.type_id;} 
			set{ _type_id = value;}
		}

        public int getEntityId()
        {
            return m_entity_id;
        }

        public int getTypeId()
        {
            return _type_id;
        }

        public void setEntityId(int entityId)
        {
            m_entity_id = entityId;
        }

        public void setTypeId(int typeId)
        {
            _type_id = typeId;
        }

        private bool m_Collided = false;

        public bool Collided
        {
            get { return m_Collided; }
            set { m_Collided = value; }
        }

        private Vector2 m_ResponseVector;

        public Vector2 ResponseVector
        {
            get { return m_ResponseVector; }
            set { m_ResponseVector = value; }
        }


    }
}
