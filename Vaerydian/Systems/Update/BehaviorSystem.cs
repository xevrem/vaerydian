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


using BehaviorLib;

using Vaerydian.Components;
using Vaerydian.Components.Characters;


namespace Vaerydian.Systems.Update
{
    class BehaviorSystem : EntityProcessingSystem
    {
        private ComponentMapper _BehaviorMapper;
        private ComponentMapper _LifeMapper;

        public BehaviorSystem() {}//: base(intervals) { }

		protected override void initialize()
        {
            _BehaviorMapper = new ComponentMapper(new AiBehavior(), ecs_instance);
            _LifeMapper = new ComponentMapper(new Life(), ecs_instance);
        }

        protected override void process(Entity entity)
        {
            AiBehavior aiBehavior = (AiBehavior)_BehaviorMapper.get(entity);
            Life life = (Life)_LifeMapper.get(entity);
            if (life.IsAlive)
                aiBehavior.Behavior.Behave();
            else
            {
                if (!aiBehavior.Behavior.IsClean)
                    aiBehavior.Behavior.deathCleanup();
            }
        }

        
    }
}
