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

using Vaerydian.Components;
using Vaerydian.Components.Items;
using Vaerydian.Utils;

namespace Vaerydian.Factories
{
    class ItemFactory
    {
		private ECSInstance ecs_instance;
        private static GameContainer i_Container;
        private Random rand = new Random();

        public ItemFactory(ECSInstance ecsInstance, GameContainer container)
        {
            ecs_instance = ecsInstance;
            i_Container = container;
        }

        public ItemFactory(ECSInstance ecsInstance) 
        {
            ecs_instance = ecsInstance;
        }

        public Entity createTestMeleeWeapon()
        {
            Entity e = ecs_instance.create();

            Item item = new Item("TestMeleeWeapon", 0, 100);
			item.Lethality = 10;
			item.Speed = 5;
			item.MinRange = 0;
			item.MaxRange = 48;
			item.ItemType = ItemType.WEAPON;
			item.WeaponType = WeaponType.MELEE;
			item.DamageType = DamageType.SLASHING;


            //Weapon weapon = new Weapon(10, 5, 0, 48, WeaponType.MELEE, DamageType.SLASHING);
            //weapon.MeleeWeaponType = MeleeWeaponType.Sword;

            ecs_instance.add_component(e, item);
            //i_ECSInstance.entity_manager.add_component(e, weapon);

            ecs_instance.resolve(e);
            return e;
        }

        public Entity createTestRangedWeapon()
        {
            Entity e = ecs_instance.create();

            Item item = new Item("TestRangedWeapon", 0, 100);
			item.Lethality = 5;
			item.Speed = 5;
			item.MinRange = 100;
			item.MaxRange = 300;
			item.ItemType = ItemType.WEAPON;
			item.WeaponType = WeaponType.RANGED;
			item.DamageType = DamageType.PIERCING;

            //Weapon weapon = new Weapon(5, 5, 100, 300, WeaponType.RANGED, DamageType.PIERCING);
            //weapon.RangedWeaponType = RangedWeaponType.Blaster;

            ecs_instance.add_component(e, item);
            //i_ECSInstance.entity_manager.add_component(e, weapon);

            ecs_instance.resolve(e);

            return e;
        }

        public Entity createTestArmor()
        {
            Entity e = ecs_instance.create();

            Item item = new Item("TestArmor", 0, 100);
			item.Mobility = 5;
			item.Mitigation = 5;

            //Armor armor = new Armor(5, 5);

            ecs_instance.add_component(e, item);
            //i_ECSInstance.entity_manager.add_component(e, armor);

            ecs_instance.resolve(e);

            return e;
        }

        public Equipment createTestEquipment()
        {
            Equipment equipment = new Equipment();

            equipment.MeleeWeapon = createTestMeleeWeapon();
            equipment.RangedWeapon = createTestRangedWeapon();
            equipment.Armor = createTestArmor();

            return equipment;
        }

        public void destoryEquipment(Entity entity)
        {
            ComponentMapper equipMapper = new ComponentMapper(new Equipment(),ecs_instance);
            ComponentMapper itemMapper = new ComponentMapper(new Item(),ecs_instance);

            Equipment equip = (Equipment)equipMapper.get(entity);

            if (equip == null)
                return;

            //remove melee weapon
            Item meleeWeapon = (Item)itemMapper.get(equip.MeleeWeapon);
            if (meleeWeapon != null)
                ecs_instance.delete_entity(equip.MeleeWeapon);
			

            //remove ranged weapon
            Item rangedWeapon = (Item)itemMapper.get(equip.RangedWeapon);
            if (rangedWeapon != null)
                ecs_instance.delete_entity(equip.RangedWeapon);

            //remove armor
            Item armor = (Item)itemMapper.get(equip.Armor);
            if (armor != null)
                ecs_instance.delete_entity(equip.Armor);


            return;
        }

    }
}
