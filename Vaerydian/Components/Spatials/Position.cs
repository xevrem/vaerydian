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

namespace Vaerydian.Components.Spatials
{
    public class Position : Component
    {
		private static int _type_id;

        public static int TypeID
        {
            get { return Position._type_id; }
            set { Position._type_id = value; }
        }

        private int p_entity_id;

        private Vector2 p_Position;

        public Vector2 Pos
        {
            get { return p_Position; }
            set { p_Position = value; }
        }

        private Vector2 p_Offset;

        public Vector2 Offset
        {
            get { return p_Offset; }
            set { p_Offset = value; }
        }

        public Position() { }

		public override int type_id{ 
			get{ return _type_id;} 
			set{ _type_id = value;}
		}

        public Position(Vector2 position, Vector2 offset)
        {
            p_Position = position;
            p_Offset = offset;
        }

        public int getEntityId()
        {
            return p_entity_id;
        }

        public int getTypeId()
        {
            return _type_id;
        }

        public void setEntityId(int entityId)
        {
            p_entity_id = entityId;
        }

        public void setTypeId(int typeId)
        {
            _type_id = typeId;
        }

    }
}
