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
using Vaerydian.Components;
using Vaerydian.Components.Audio;
using Glimpse.Controls;
using Microsoft.Xna.Framework;
using Vaerydian.Components.Spatials;
using Vaerydian.Components.Actions;
using Vaerydian.Components.Graphical;
using Vaerydian.Components.Utils;
using Vaerydian.Components.Characters;
using Vaerydian.Characters;
using Glimpse.Input;


namespace Vaerydian.Factories
{
    public static class UtilFactory
    {
        public static ECSInstance ecs_instance;
        public static GameContainer Container;
        private static Random rand = new Random();

        /// <summary>
        /// create an attack
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <param name="attackType"></param>
        public static void createAttack(Entity attacker, Entity defender, AttackType attackType)
        {
            Entity e = ecs_instance.create();

            ecs_instance.add_component(e, new Attack(attacker, defender, attackType));

            ecs_instance.resolve(e);
        }


        /// <summary>
        /// creates direct damage
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="type"></param>
        /// <param name="target"></param>
        /// <param name="pos"></param>
        public static void createDirectDamage(int amount, DamageType type, Entity target,Position pos)
        {
            Entity e = ecs_instance.create();

            Damage damage = new Damage();

            damage.DamageAmount = amount;
            damage.Target = target;
            damage.DamageClass = DamageClass.DIRECT;
            damage.DamageType = type;
            damage.Lifespan = 500;//.5 second

            ecs_instance.add_component(e, damage);
            ecs_instance.add_component(e, pos);

            ecs_instance.resolve(e);
        }

        public static void createSound(String name, bool play, float volume)
        {
            Entity e = ecs_instance.create();

            Audio audio = new Audio(name,play,volume);

            ecs_instance.add_component(e, audio);

            ecs_instance.resolve(e);

        }

        public static void createFireSound(Control sender, InterfaceArgs args)
        {
            Entity e = ecs_instance.create();

            Audio audio = new Audio("audio\\effects\\fire", true, 1f);

            ecs_instance.add_component(e, audio);

            ecs_instance.resolve(e);
        }

        public static void createSound(String name, bool play, float volume, float pitch)
        {
            Entity e = ecs_instance.create();

            Audio audio = new Audio(name, play, volume, pitch);

            ecs_instance.add_component(e, audio);

            ecs_instance.resolve(e);

        }

        /// <summary>
        /// 
        /// </summary>
        public static void createMeleeAction(Vector2 position, Vector2 heading, Transform transform, Entity owner)
        {
            Entity e = ecs_instance.create();

            Sprite sprite = new Sprite("sword", "swordnormal", 32, 32, 0, 0);

            MeleeAction action = new MeleeAction();
            action.Animation = new SpriteAnimation(9, 20);
            action.ArcDegrees = 180;
            action.Owner = owner;
            action.Lifetime = 250;
            action.Range = 32;

            ecs_instance.add_component(e, new Position(position,new Vector2(16)));
            ecs_instance.add_component(e, new Heading(heading));
            ecs_instance.add_component(e, transform);
            ecs_instance.add_component(e, sprite);
            ecs_instance.add_component(e, action);

            ecs_instance.resolve(e);
        }

        /// <summary>
        /// creates a victory entity component
        /// </summary>
        public static void createVictoryAward(Entity awarder, Entity receiver, int maxAwardable)
        {
            Entity e = ecs_instance.create();

            Award victory = new Award();
            victory.AwardType = AwardType.Victory;
            victory.Awarder = awarder;
            victory.Receiver = receiver;
            victory.MaxAwardable = maxAwardable;

            ecs_instance.add_component(e, victory);

            ecs_instance.resolve(e);
        }

        public static void createSkillupAward(Entity awarder, Entity receiver, SkillName skill, int maxAwardable)
        {
            Entity e = ecs_instance.create();

            Award award = new Award();
            award.AwardType = AwardType.SkillUp;
            award.Awarder = awarder;
            award.Receiver = receiver;
            award.MaxAwardable = maxAwardable;
            award.SkillName = skill;

            ecs_instance.add_component(e, award);

            ecs_instance.resolve(e);
        }

        public static void createAttributeAward(Entity awarder, Entity receiver, StatType attribute, int maxAwardable)
        {
            Entity e = ecs_instance.create();

            Award award = new Award();
            award.AwardType = AwardType.Attribute;
            award.Awarder = awarder;
            award.Receiver = receiver;
            award.MaxAwardable = maxAwardable;
            award.StatType = attribute;

            ecs_instance.add_component(e, award);

            ecs_instance.resolve(e);
        }

        public static void createHealthAward(Entity receiver, int maxAwardable)
        {
            Entity e = ecs_instance.create();

            Award award = new Award();
            award.AwardType = AwardType.Health;
            award.Receiver = receiver;
            award.MaxAwardable = maxAwardable;

            ecs_instance.add_component(e, award);

            ecs_instance.resolve(e);
        }

		public static void createTarget(){

			Entity e = ecs_instance.create ();
			Target target = new Target ();
			target.TargetEntity = null;

			ecs_instance.add_component (e, target);
			ecs_instance.add_component (e, new Position(new Vector2(0),new Vector2(24)));

			Sprite sprite = new Sprite ("reticle", "reticle_normal", 48, 48, 0, 0);
			sprite.Visible = false;
			ecs_instance.add_component (e, sprite);

			ecs_instance.tag_manager.tag_entity("TARGET",e);

			ecs_instance.resolve (e);
		}
    }
}
