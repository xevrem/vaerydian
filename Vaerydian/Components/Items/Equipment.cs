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

namespace Vaerydian.Components.Items
{

	public struct EquipmentDef{
		public string Name;
		public ItemDef ArmorDef;
		public ItemDef WeaponDef;
	}

    class Equipment : Component
    {
        private static int e_type_id;
        private int e_entity_id;

        public Equipment() { }

        public int getEntityId()
        {
            return e_entity_id;
        }

        public int getTypeId()
        {
            return e_type_id;
        }

        public void setEntityId(int entityId)
        {
            e_entity_id = entityId;
        }

        public void setTypeId(int typeId)
        {
            e_type_id = typeId;
        }

        private Entity _RangedWeapon;
        /// <summary>
        /// the given entity's ranged weapon entity reference
        /// </summary>
        public Entity RangedWeapon
        {
            get { return _RangedWeapon; }
            set { _RangedWeapon = value; }
        }

        private Entity _MeleeWeapon;

        /// <summary>
        /// the given entity's melee weapon entity reference
        /// </summary>
        public Entity MeleeWeapon
        {
            get { return _MeleeWeapon; }
            set { _MeleeWeapon = value; }
        }

        private Entity _Armor;
        /// <summary>
        /// the given entities armor entity reference
        /// </summary>
        public Entity Armor
        {
            get { return _Armor; }
            set { _Armor = value; }
        }
    }
}
