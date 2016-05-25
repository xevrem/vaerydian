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
using Vaerydian.Characters;

using ECSFramework;

namespace Vaerydian.Components.Characters
{
	public struct InfoDef{
		public string Name;
		public string GeneralGroup;
		public string VariationGroup;
		public string UniqueGroup;
	}


    class Information : Component
    {
		private static int _type_id;
        private int i_entity_id;

        public Information() { }

		public override int type_id{ 
			get{ return this.type_id;} 
			set{ _type_id = value;}
		}

        public int getEntityId()
        {
            return i_entity_id;
        }

        public int getTypeId()
        {
            return _type_id;
        }

        public void setEntityId(int entityId)
        {
            i_entity_id = entityId;
        }

        public void setTypeId(int typeId)
        {
            _type_id = typeId;
        }

        /// <summary>
        /// name of creature
        /// </summary>
		public string Name;

        /// <summary>
        /// type of creature
        /// </summary>
		public string GeneralGroup;
        /// <summary>
        /// type of creature variation
        /// </summary>
		public string VariationGroup;
        /// <summary>
        /// type of unique creature
        /// </summary>
		public string UniqueGroup;
    }
}
