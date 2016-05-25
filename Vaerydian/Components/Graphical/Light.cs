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

namespace Vaerydian.Components.Graphical
{
    class Light : Component
    {
		private static int _type_id;
        private int l_entity_id;

        public Light() { }

		public override int type_id{ 
			get{ return this.type_id;} 
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

		public static int TypeID {
			get {
				return _type_id;
			}
			set {
				_type_id = value;
			}
		}

        public void setEntityId(int entityId)
        {
            l_entity_id = entityId;
        }

        public void setTypeId(int typeId)
        {
            _type_id = typeId;
        }

        private bool l_IsEnabled;

        public bool IsEnabled
        {
            get { return l_IsEnabled; }
            set { l_IsEnabled = value; }
        }

        private float l_ActualPower;

        public float ActualPower
        {
            get { return l_ActualPower; }
            set { l_ActualPower = value; }
        }
        private Vector3 l_Position;

        public Vector3 Position
        {
            get { return l_Position; }
            set { l_Position = value; }
        }
        private Vector4 l_Color;

        public Vector4 Color
        {
            get { return l_Color; }
            set { l_Color = value; }
        }
        private int l_LightRadius;

        public int LightRadius
        {
            get { return l_LightRadius; }
            set { l_LightRadius = value; }
        }
    }
}
