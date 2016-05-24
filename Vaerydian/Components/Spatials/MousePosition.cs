﻿/*
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
    public class MousePosition : Component
    {
        private static int m_type_id;
        private int m_entity_id;

        public MousePosition() { }

        public int getEntityId()
        {
            return m_entity_id;
        }

        public int getTypeId()
        {
            return m_type_id;
        }

        public void setEntityId(int entityId)
        {
            m_entity_id = entityId;
        }

        public void setTypeId(int typeId)
        {
            m_type_id = typeId;
        }
    }
}
