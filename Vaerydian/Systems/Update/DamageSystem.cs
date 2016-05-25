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
using Vaerydian.Factories;
using Vaerydian.Components.Actions;
using Vaerydian.Components.Characters;
using Vaerydian.Characters;

namespace Vaerydian.Systems.Update
{
    class DamageSystem : EntityProcessingSystem
    {
        private ComponentMapper _DamageMapper;
        private ComponentMapper _HealthMapper;
        private ComponentMapper _AttributeMapper;
        private ComponentMapper _InteractMapper;

        private Random _Rand = new Random();

        public DamageSystem() { }

		protected override void initialize()
        {
            _DamageMapper = new ComponentMapper(new Damage(), ecs_instance);
            _HealthMapper = new ComponentMapper(new Health(), ecs_instance);
            _AttributeMapper = new ComponentMapper(new Statistics(), ecs_instance);
            _InteractMapper = new ComponentMapper(new Interactable(), ecs_instance);

        }
        
        protected override void process(Entity entity)
        {
            Damage damage = (Damage)_DamageMapper.get(entity);

            if (damage.IsActive)
            {
                switch (damage.DamageClass)
                {
                    case DamageClass.DIRECT:
                        handleDirectDamage(damage);
                        break;
                    case DamageClass.OVERTIME:
                        break;
                    case DamageClass.AREA:
                        break;
                    default:
                        break;
                }
            }

            damage.Lifetime += ecs_instance.ElapsedTime;

            if (damage.Lifetime > damage.Lifespan)
            {
                ecs_instance.delete_entity(entity);
                return;
            }
        }

        private void handleDirectDamage(Damage damage)
        {
            Health health = (Health)_HealthMapper.get(damage.Target);

            if (health != null)
            {
                //damage target
                health.CurrentHealth -= damage.DamageAmount;

                if (damage.DamageAmount > 0)
                {
                    if (((Interactable)_InteractMapper.get(damage.Target)).SupportedInteractions.MAY_ADVANCE)
                    {
                        int endurance = ((Statistics)_AttributeMapper.get(damage.Target)).Endurance.Value;

                        if (health.MaxHealth < (endurance * 5))
                        {

                            if (_Rand.NextDouble() <= ((double)(endurance*5) - (double)health.MaxHealth)/(double)(endurance*5))
                            {
                                UtilFactory.createHealthAward(damage.Target, 1);
                            }
                        }
                    }
                    

					//FIX
                    switch (_Rand.Next(0, 7))
                    {
                        case 1:
                            UtilFactory.createSound("audio\\effects\\hurt", true, 1f);
                            break;
                        case 3:
                            UtilFactory.createSound("audio\\effects\\hurt2", true, 1f);
                            break;
                        case 5:
                            UtilFactory.createSound("audio\\effects\\hurt3", true, 1f);
                            break;
                        case 7:
                            UtilFactory.createSound("audio\\effects\\hurt4", true, 1f);
                            break;
                        default:
                            break;
                    }
                    
                    
                }
            }

            damage.IsActive = false;
        }

        private void handleDamageOverTime(Damage damage)
        {
        }

        private void handleAreaOfEffect(Damage damage)
        {
        }


    }
}
