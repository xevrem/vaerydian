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
using Vaerydian.Utils;


namespace Vaerydian.Components.Items
{
	public struct ItemDef{
		public string Name;
		public int Value;
		public int Durability;
		public string Description;
		public ItemType ItemType;
		public int Lethality;
		public int Speed;
		public int MinRange;
		public int MaxRange;
		public WeaponType WeaponType;
		public DamageType DamageType;
		public int Mitigation;
		public int Mobility;
		public ActionDef SpecialEffect;
	}

	public enum ItemType{
		CONSUMEABLE,
		WEAPON,
		ARMOR,
		MATERIAL,
	}

	public enum WeaponType
	{
		MELEE,
		RANGED
	}

	public enum MeleeWeaponType
	{
		AXE,
		DAGGER,
		MACE,
		SWORD,
	}

	public enum RangedWeaponType
	{
		BOW,
		CROSSBOW,
		BLASTER
	}

    class Item : IComponent
    {
		private static int _type_id;
        private int i_entity_id;

        public Item() { }

		public int id { get; set;}

		public int owner_id { get; set;}

		public int type_id{
			get{ return _type_id;} 
			set{ _type_id = value;}
		}

        public Item(String name, int Value, int durability)
        {
            this.Name = name;
			this.Value = Value;
			this.CurrentDurability = durability;
			this.MaxDurability = durability;
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
        /// name of item
        /// </summary>
		public String Name;
        /// <summary>
        /// Value of the item
        /// </summary>
        public int Value;
        /// <summary>
        /// the item's current durability
        /// </summary>
		public int CurrentDurability;
        /// <summary>
        /// the item's maximum durability
        /// </summary>
		public int MaxDurability;
		/// <summary>
		/// The description.
		/// </summary>
		public string Description;
		/// <summary>
		/// The type of the item.
		/// </summary>
		public ItemType ItemType;
		/// <summary>
		/// How damaging the item is
		/// </summary>
		public int Lethality;
		/// <summary>
		/// how fast the item allows the user to use it
		/// </summary>
		public int Speed;
		/// <summary>
		/// The minimum damage.
		/// </summary>
		public int MinRange;
		/// <summary>
		/// The max damage.
		/// </summary>
		public int MaxRange;
		/// <summary>
		/// The type of the weapon.
		/// </summary>
		public WeaponType WeaponType;
		/// <summary>
		/// The type of the damage.
		/// </summary>
		public DamageType DamageType;
		/// <summary>
		/// how much damage is absorbed by the item
		/// </summary>
		public int Mitigation;
		/// <summary>
		/// how mobile the item allows the user to be
		/// </summary>
		public int Mobility;
		/// <summary>
		/// any special effects triggered when the item is used
		/// </summary>
		public ActionDef SpecialEffect;
    }
}
