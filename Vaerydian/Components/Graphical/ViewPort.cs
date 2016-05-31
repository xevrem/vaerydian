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
    public class ViewPort : IComponent
    {

		private static int _type_id;
        private int v_entity_id;

        private Vector2 v_Origin;
        private Vector2 v_Dimensions;

        public ViewPort() { }

		public int id { get; set;}

		public int owner_id { get; set;}

		public int type_id{
			get{ return _type_id;} 
			set{ _type_id = value;}
		}

        public ViewPort(Vector2 origin, Vector2 dimensions) 
        {
            v_Origin = origin;
            v_Dimensions = dimensions;
        }

        public int getEntityId()
        {
            return v_entity_id;
        }

        public int getTypeId()
        {
            return _type_id;
        }

        public Vector2 getOrigin()
        {
            return v_Origin;
        }

        public Vector2 getDimensions()
        {
            return v_Dimensions;
        }

        public void setEntityId(int entityId)
        {
            v_entity_id = entityId;
        }

        public void setTypeId(int typeId)
        {
            _type_id = typeId;
        }

        public void setOrigin(Vector2 origin)
        {
            v_Origin = origin;
        }

        public void setDimensions(Vector2 dimensions)
        {
            v_Dimensions = dimensions;
        }
    }
}
