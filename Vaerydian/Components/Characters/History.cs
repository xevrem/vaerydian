/*
 Author:
      Thomas H. Jonell <@Net_Gnome>
 
 Copyright (c) 2013 Thomas H. Jonell

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
    /// maintains a history of the entity
    /// </summary>
    class History : Component
    {

        private static int h_TypeID;
        private int h_EntityID;

        public History() { }

        public int getEntityId()
        {
            return h_EntityID;
        }

        public int getTypeId()
        {
            return h_TypeID;
        }

		public static int TypeId{ get { return h_TypeID; } set { h_TypeID = value; } }

        public void setEntityId(int entityId)
        {
            h_EntityID = entityId;
        }

        public void setTypeId(int typeId)
        {
            h_TypeID = typeId;
        }

    }
}
