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

namespace Vaerydian.Components.Spatials
{
    public class Velocity : Component
    {
        private static int v_type_id;
        private int v_entity_id;

        private float v_Velocity;

        public float Vel
        {
            get { return v_Velocity; }
            set { v_Velocity = value; }
        }

        public Velocity() { }

        public Velocity(float velocity)
        {
            v_Velocity = velocity;
        }

        public int getEntityId()
        {
            return v_entity_id;
        }

        public int getTypeId()
        {
            return v_type_id;
        }

        public void setEntityId(int entityId)
        {
            v_entity_id = entityId;
        }

        public void setTypeId(int typeId)
        {
            v_type_id = typeId;
        }
    }
}
