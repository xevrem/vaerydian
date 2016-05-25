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

namespace Vaerydian.Components.Characters
{
    class Aggrivation : Component
    {
        private static int _type_id;

        public static int TypeID
        {
            get { return Aggrivation._type_id; }
            set { Aggrivation._type_id = value; }
        }
        private int _entity_id;

        public Aggrivation() { }

		public override int type_id{ 
			get{ return this.type_id;} 
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

        private List<Entity> _HateList = new List<Entity>();

        public List<Entity> HateList
        {
            get { return _HateList; }
            set { _HateList = value; }
        }

        private Entity _Target;

        public Entity Target
        {
            get { return _Target; }
            set { _Target = value; }
        }
    }
}
