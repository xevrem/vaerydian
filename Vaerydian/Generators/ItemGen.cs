//
//  ItemGen.cs
//
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
using Vaerydian.Components.Items;

namespace Vaerydian.Generators
{
	public static class ItemGen
	{
		public static ItemDef createItem(int skillLevel, ItemType itemType){

		ItemDef item;

			switch (itemType) {
			case ItemType.ARMOR:
				item = createArmor (skillLevel);
				break;
			case ItemType.CONSUMEABLE:
				item = createConsumeable (skillLevel);
				break;
			case ItemType.MATERIAL:
				item = createMaterial (skillLevel);
				break;
			case ItemType.WEAPON:
				item = createWeapon (skillLevel);
				break;
			default:
				item = default(ItemDef);
				break;
			}

			return item;
		}

		private static ItemDef createArmor(int skillLevel){
			return default(ItemDef);
		}

		private static ItemDef createConsumeable(int skillLevel){
			return default(ItemDef);
		}

		private static ItemDef createMaterial(int skillLevel){
			return default(ItemDef);
		}

		private static ItemDef createWeapon(int skillLevel){
			ItemDef item = default(ItemDef);
			item.ItemType = ItemType.WEAPON;

			item.Lethality = skillLevel;
			item.MinRange = 0;
			item.MaxRange = 400;
			item.Speed = 5;



			item.Name = "";

			return default(ItemDef);
		}


	}
}

