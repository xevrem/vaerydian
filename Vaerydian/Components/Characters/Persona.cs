/*
 Author:
      Erika V. Jonell <@xevrem>
 
 Copyright (c) 2013 Erika V. Jonell

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
using ECSFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vaerydian.Components.Characters
{
    /// <summary>
    /// contains the entitie's persona
    /// </summary>
    class Persona : Component
    {
        private static int p_TypeID;
        private int p_EntityID;

        public Persona() { }

        public int getEntityId()
        {
            return p_EntityID;
        }

        public int getTypeId()
        {
            return p_TypeID;
        }

		public static int TypeId{ get { return p_TypeID; } set { p_TypeID = value; } }

        public void setEntityId(int entityId)
        {
            p_EntityID = entityId;
        }

        public void setTypeId(int typeId)
        {
            p_TypeID = typeId;
        }

    }
}
