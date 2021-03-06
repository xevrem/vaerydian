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

using Microsoft.Xna.Framework;

using ECSFramework;

using Vaerydian.Characters;
using Vaerydian.Components;
using Vaerydian.Components.Items;
using Vaerydian.Components.Utils;
using Vaerydian.Utils;
using Vaerydian.Factories;
using Vaerydian.Components.Spatials;
using Vaerydian.Components.Actions;
using Vaerydian.Components.Characters;


namespace Vaerydian.Systems.Update
{
    class AttackSystem : EntityProcessingSystem
    {
        private ComponentMapper _AttackMapper;
        private ComponentMapper _PositionMapper;
        private ComponentMapper _SkillMapper;
        private ComponentMapper _AttributeMapper;
        private ComponentMapper _KnowledgeMapper;
        private ComponentMapper _EquipmentMapper;
        private ComponentMapper _ItemMapper;
        private ComponentMapper _AggroMapper;
        private ComponentMapper _InfoMapper;
        private ComponentMapper _InteractMapper;

        private Entity _CurrentEntity;

        private Random rand = new Random();

        public AttackSystem() { }
        
		protected override void initialize()
        {
            _AttackMapper = new ComponentMapper(new Attack(), ecs_instance);
            _PositionMapper = new ComponentMapper(new Position(), ecs_instance);
            _SkillMapper = new ComponentMapper(new Skills(), ecs_instance);
            _AttributeMapper = new ComponentMapper(new Statistics(), ecs_instance);
            _KnowledgeMapper = new ComponentMapper(new Knowledges(), ecs_instance);
            _EquipmentMapper = new ComponentMapper(new Equipment(), ecs_instance);
            _ItemMapper = new ComponentMapper(new Item(), ecs_instance);
            _AggroMapper = new ComponentMapper(new Aggrivation(), ecs_instance);
            _InfoMapper = new ComponentMapper(new Information(), ecs_instance);
            _InteractMapper = new ComponentMapper(new Interactable(), ecs_instance);

        }

        protected override void process(Entity entity)
        {
            //retrieve this attack
            _CurrentEntity = entity;
            Attack attack = (Attack)_AttackMapper.get(entity);

            //see if defender is aggroable
            Aggrivation aggro = (Aggrivation)_AggroMapper.get(attack.Defender);
            if (aggro != null)
            {
                //set aggro
                if (!aggro.HateList.Contains(attack.Attacker))
                    aggro.HateList.Add(attack.Attacker);
            }


            //determine type of attack and handle it
            switch (attack.AttackType)
            {
                case AttackType.Melee:
                    handleMelee(attack);
                    break;
                case AttackType.Projectile:
                    handleProjectile(attack);
                    break;
                case AttackType.Ability:
                    handleAbility(attack);
                    break;
                default:
                    ecs_instance.delete_entity(entity);
                    break;
            }
        }

        /// <summary>
        /// handle melee attacks
        /// </summary>
        /// <param name="attack">attack to handle</param>
        private void handleMelee(Attack attack)
        {
            Position position = (Position)_PositionMapper.get(attack.Defender);

            //dont continue if this attack has no position
            if (position == null)
                return;

            //calculate position
            Vector2 pos = position.Pos;
            Position newPos = new Position(pos + new Vector2(rand.Next(16) + 8, 0), Vector2.Zero);

            //get equipment
            Equipment attEquip = (Equipment)_EquipmentMapper.get(attack.Attacker);
            Equipment defEquip = (Equipment)_EquipmentMapper.get(attack.Defender);

            //dont continue if we have no equipment to use
            if (attEquip == null || defEquip == null)
                return;

            //get weapon and armor
            Item weapon = (Item)_ItemMapper.get(attEquip.MeleeWeapon);
			Item armor = (Item)_ItemMapper.get(defEquip.Armor);

            //dont continue if either of these are null
            if (weapon == null || armor == null)
                return;

            //get attributes
            Statistics attAttr = (Statistics)_AttributeMapper.get(attack.Attacker);
            Statistics defAttr = (Statistics)_AttributeMapper.get(attack.Defender);

            //dont continue if either of these are null
            if (attAttr == null || defAttr == null)
                return;

            int perception = attAttr.Perception.Value;
            int muscle = attAttr.Muscle.Value;
            int quickness = defAttr.Quickness.Value;
            int endurance = defAttr.Endurance.Value;

            //get Experience
            Knowledges attKnw = (Knowledges)_KnowledgeMapper.get(attack.Attacker);
            Knowledges defKnw = (Knowledges)_KnowledgeMapper.get(attack.Defender);

            //dont continue if null
            if (attKnw == null || defKnw == null)
                return;

            //get Skills
            Skills attSkills = (Skills)_SkillMapper.get(attack.Attacker);
            Skills defSkills = (Skills)_SkillMapper.get(attack.Defender);

            //dont continue if either of these are null
            if (attSkills == null || defSkills == null)
                return;

            int atkSkill = attSkills.Melee.Value;
            int defSkill = defSkills.Avoidance.Value;

            Information infoDef = (Information)_InfoMapper.get(attack.Defender);
            Information infoAtk = (Information)_InfoMapper.get(attack.Attacker);

            //dont continue if you dont have info
            if (infoDef == null || infoAtk == null)
                return;

            float probHit = atkSkill / 4 + perception / 4 + attKnw.GeneralKnowledge[infoDef.GeneralGroup].Value + weapon.Speed;
            float probDef = defSkill / 4 + quickness / 4 + defKnw.GeneralKnowledge[infoAtk.GeneralGroup].Value + armor.Mobility;

            float hitProb = (probHit / (probHit + probDef)) * 1.75f + (probDef / (probHit + probDef)) * 0.15f;

            float toHit = (float)rand.NextDouble();

            int damage = 0;

            if (toHit < hitProb)
            {

                float overhit = 0f;

                if (hitProb > 1f)
                    overhit = hitProb - 1f;

                //int maxDmg = (int)((overhit + 1f) * ((atkSkill / 5 + muscle / 4) / (endurance / 10)) * (weapon.Lethality / armor.Mitigation));
                int maxDmg = (int)((overhit + 1f) * ((atkSkill / 5 + muscle / 4)) * (weapon.Lethality / armor.Mitigation)) - (endurance/10);

                damage = rand.Next(maxDmg / 2, maxDmg);

                if (damage < 0)
                    damage = 0;
            }

            UtilFactory.createDirectDamage(damage, weapon.DamageType, attack.Defender, newPos);

            if (damage == 0)
            {
                UIFactory.createFloatingText("MISS","DAMAGE", Color.White,500, new Position(newPos.Pos,newPos.Offset));
            }
            else
            {
                UIFactory.createFloatingText(""+damage, "DAMAGE", Color.Yellow, 500, new Position(newPos.Pos, newPos.Offset));
            }

            Interactable interactor = (Interactable)_InteractMapper.get(attack.Attacker);
            Interactable interactee = (Interactable)_InteractMapper.get(attack.Defender);

            //only do if interaction supported
            if (interactee != null && interactor != null)
            {
                //only skill-up if you can
                if (interactor.SupportedInteractions.MAY_ADVANCE &&
                    interactee.SupportedInteractions.CAUSES_ADVANCEMENT)
                {
                    //if still possible to skill-up
                    if (atkSkill < defSkill)
                    {
						if (rand.NextDouble() <= ((double)(defSkill - atkSkill) / (double)defSkill) * GameConfig.AwardDefs.SkillChance * GameConfig.AwardDefs.MeleeMod)
							UtilFactory.createSkillupAward(attack.Defender, attack.Attacker, SkillName.MELEE, GameConfig.AwardDefs.SkillMinimum);
                    }

                    if (perception < quickness)
                    {
						if (rand.NextDouble() <= ((double)(quickness - perception) / (double)quickness) * GameConfig.AwardDefs.StatChance)
							UtilFactory.createAttributeAward(attack.Defender, attack.Attacker, StatType.PERCEPTION, GameConfig.AwardDefs.StatMinimum);
                    }

                    if (muscle < endurance)
                    {
						if (rand.NextDouble() <= ((double)(endurance - muscle) / (double)endurance) * GameConfig.AwardDefs.StatChance)
							UtilFactory.createAttributeAward(attack.Defender, attack.Attacker, StatType.MUSCLE, GameConfig.AwardDefs.StatMinimum);
                    }
                }

                if (interactor.SupportedInteractions.CAUSES_ADVANCEMENT &&
                    interactee.SupportedInteractions.MAY_ADVANCE)
                {
                    //if still possible to skill-up
                    if (defSkill < atkSkill)
                    {
						if (rand.NextDouble() <= ((double)(atkSkill - defSkill) / (double)atkSkill) * GameConfig.AwardDefs.SkillChance)
							UtilFactory.createSkillupAward(attack.Attacker, attack.Defender, SkillName.AVOIDANCE, GameConfig.AwardDefs.SkillMinimum);
                    }

                    if (quickness < perception)
                    {
						if (rand.NextDouble() <= ((double)(perception - quickness) / (double)perception) * GameConfig.AwardDefs.StatChance)
							UtilFactory.createAttributeAward(attack.Attacker, attack.Defender, StatType.QUICKNESS, GameConfig.AwardDefs.StatMinimum);
                    }


                    if (endurance < muscle)
                    {
						if (rand.NextDouble() <= ((double)(muscle - endurance) / (double)muscle) * GameConfig.AwardDefs.StatChance)
							UtilFactory.createAttributeAward(attack.Attacker, attack.Defender, StatType.ENDURANCE, GameConfig.AwardDefs.StatMinimum);
                    }
                }
            }


            //remove attack
            ecs_instance.delete_entity(_CurrentEntity);
        }

        /// <summary>
        /// handle projectile attacks
        /// </summary>
        /// <param name="attack">attack to handle</param>
        private void handleProjectile(Attack attack) 
        {
            Position position = (Position)_PositionMapper.get(attack.Defender);

            //dont continue if this attack has no position
            if (position == null)
                return;

            //calculate position
            Vector2 pos = position.Pos;
            Position newPos = new Position(pos + new Vector2(rand.Next(16)+8, 0), Vector2.Zero);

            //get equipment
            Equipment attEquip = (Equipment)_EquipmentMapper.get(attack.Attacker);
            Equipment defEquip = (Equipment)_EquipmentMapper.get(attack.Defender);

            //dont continue if we have no equipment to use
            if (attEquip == null || defEquip == null)
                return;

            //get weapon and armor
            Item weapon = (Item) _ItemMapper.get(attEquip.RangedWeapon);
            Item armor = (Item) _ItemMapper.get(defEquip.Armor);

            //dont continue if either of these are null
            if (weapon == null || armor == null)
                return;

            //get attributes
            Statistics attAttr = (Statistics)_AttributeMapper.get(attack.Attacker);
            Statistics defAttr = (Statistics)_AttributeMapper.get(attack.Defender);

            //dont continue if either of these are null
            if (attAttr == null || defAttr == null)
                return;

            int perception = attAttr.Perception.Value;
            int quickness = defAttr.Quickness.Value;
            int focus = attAttr.Focus.Value;
			int endurance = defAttr.Endurance.Value;

            //get Experience
            Knowledges attKnw = (Knowledges)_KnowledgeMapper.get(attack.Attacker);
            Knowledges defKnw = (Knowledges)_KnowledgeMapper.get(attack.Defender);

            //dont continue if null
            if (attKnw == null || defKnw == null)
                return;

            //get Skills
            Skills attSkills = (Skills)_SkillMapper.get(attack.Attacker);
            Skills defSkills = (Skills)_SkillMapper.get(attack.Defender);

            //dont continue if either of these are null
            if (attSkills == null || defSkills == null)
                return;

            int atkSkill = attSkills.Ranged.Value;
            int defSkill = defSkills.Avoidance.Value;


            Information infoDef = (Information)_InfoMapper.get(attack.Defender);
            Information infoAtk = (Information)_InfoMapper.get(attack.Attacker);

            //dont continue if you dont have info
            if (infoDef == null || infoAtk == null)
                return;

            float probHit = atkSkill / 4 + perception / 4 + attKnw.GeneralKnowledge[infoDef.GeneralGroup].Value + weapon.Speed;
            float probDef = defSkill / 4 + quickness / 4 + defKnw.GeneralKnowledge[infoAtk.GeneralGroup].Value + armor.Mobility;

            float hitProb = (probHit / (probHit + probDef)) * 1.75f + (probDef / (probHit + probDef)) * 0.15f;

            float toHit = (float)rand.NextDouble();

            int damage = 0;

            if (toHit < hitProb)
            {

                float overhit = 0f;

                if (hitProb > 1f)
                    overhit = hitProb - 1f;

                //int maxDmg = (int)((overhit + 1f) * ((atkSkill / 5 + focus / 4) / (endurance / 10)) * (weapon.Lethality / armor.Mitigation));
                int maxDmg = (int)((overhit + 1f) * ((atkSkill / 5 + focus / 4)) * (weapon.Lethality / armor.Mitigation)) - (endurance / 10);

                damage = rand.Next(maxDmg / 2, maxDmg);

                if (damage < 0)
                    damage = 0;
            }

            UtilFactory.createDirectDamage(damage, weapon.DamageType, attack.Defender, newPos);

            //create the floating dmg
            if (damage == 0)
            {
                UIFactory.createFloatingText("MISS", "DAMAGE", Color.White, 500, new Position(newPos.Pos, newPos.Offset));
            }
            else
            {
                UIFactory.createFloatingText("" + damage, "DAMAGE", Color.Yellow, 500, new Position(newPos.Pos, newPos.Offset));
            }


            Interactable interactor = (Interactable)_InteractMapper.get(attack.Attacker);
            Interactable interactee = (Interactable)_InteractMapper.get(attack.Defender);

            //only do if interaction supported
            if (interactor != null && interactor != null)
            {
                //only skill-up if you can
                if (interactor.SupportedInteractions.MAY_ADVANCE && 
                    interactee.SupportedInteractions.CAUSES_ADVANCEMENT)
                {
                    //if still possible to skill-up
                    if (atkSkill < defSkill)
                    {
						if (rand.NextDouble() <= ((double)(defSkill - atkSkill) / (double)defSkill) * GameConfig.AwardDefs.SkillChance)
							UtilFactory.createSkillupAward(attack.Defender, attack.Attacker, SkillName.RANGED, GameConfig.AwardDefs.SkillMinimum);
                    }

                    if (perception < quickness)
                    {
						if (rand.NextDouble() <= ((double)(quickness - perception) / (double)quickness) * GameConfig.AwardDefs.StatChance)
							UtilFactory.createAttributeAward(attack.Defender, attack.Attacker, StatType.PERCEPTION, GameConfig.AwardDefs.StatMinimum);
                    }

                    if (focus < endurance)
                    {
						if(rand.NextDouble() <= ((double)(endurance - focus) / (double) endurance) * GameConfig.AwardDefs.StatChance)
							UtilFactory.createAttributeAward(attack.Defender, attack.Attacker, StatType.FOCUS, GameConfig.AwardDefs.StatMinimum);
                    }
                }

                if (interactor.SupportedInteractions.CAUSES_ADVANCEMENT &&
                    interactee.SupportedInteractions.MAY_ADVANCE)
                {
                    //if still possible to skill-up
                    if (defSkill < atkSkill)
                    {
						if (rand.NextDouble() <= ((double)(atkSkill - defSkill) / (double)atkSkill) * GameConfig.AwardDefs.SkillChance)
							UtilFactory.createSkillupAward(attack.Attacker, attack.Defender, SkillName.AVOIDANCE, GameConfig.AwardDefs.SkillMinimum);
                    }

                    if (quickness < perception)
                    {
						if (rand.NextDouble() <= ((double)(perception - quickness) / (double)perception) * GameConfig.AwardDefs.StatChance)
							UtilFactory.createAttributeAward(attack.Attacker, attack.Defender, StatType.QUICKNESS, GameConfig.AwardDefs.StatMinimum);
                    }


                    if (endurance < focus)
                    {
						if (rand.NextDouble() <= ((double)(focus - endurance) / (double)focus) * GameConfig.AwardDefs.StatChance)
							UtilFactory.createAttributeAward(attack.Attacker, attack.Defender, StatType.ENDURANCE, GameConfig.AwardDefs.StatMinimum);
                    }
                }
            }

            //remove attack
            ecs_instance.delete_entity(_CurrentEntity);
        }

        /// <summary>
        /// handle ability attacks
        /// </summary>
        /// <param name="attack">attack to handle</param>
        private void handleAbility(Attack attack)
        {
            //remove attack
            ecs_instance.delete_entity(_CurrentEntity);
        }
    }
}
