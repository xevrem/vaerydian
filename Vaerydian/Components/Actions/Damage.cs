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

namespace Vaerydian.Components.Actions
{
    class Damage : Component
    {
        private static int _type_id;
        private int _entity_id;

        public Damage() { }

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

        private bool _IsActive = true;
        /// <summary>
        /// is damage component still active
        /// </summary>
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        private DamageClass _DamageClass;
        /// <summary>
        /// class of damage
        /// </summary>
        public DamageClass DamageClass
        {
            get { return _DamageClass; }
            set { _DamageClass = value; }
        }

        private DamageType _DamageType;
        /// <summary>
        /// type of damage
        /// </summary>
        public DamageType DamageType
        {
            get { return _DamageType; }
            set { _DamageType = value; }
        }

        private int _DamageAmount = 0;
        /// <summary>
        /// amount of damage
        /// </summary>
        public int DamageAmount
        {
            get { return _DamageAmount; }
            set { _DamageAmount = value; }
        }

        private int _DamageRate = 0;
        
        /// <summary>
        /// rate of damage
        /// </summary>
        public int DamageRate
        {
            get { return _DamageRate; }
            set { _DamageRate = value; }
        }

        private int _TimeSinceLastDamage = 0;
        /// <summary>
        /// time since target was last damaged (only applicable for over-time damage classes)
        /// </summary>
        public int TimeSinceLastDamage
        {
            get { return _TimeSinceLastDamage; }
            set { _TimeSinceLastDamage = value; }
        }

        private int _Lifetime = 0;
        /// <summary>
        /// time damage component has been alive
        /// </summary>
        public int Lifetime
        {
            get { return _Lifetime; }
            set { _Lifetime = value; }
        }

        private int _Lifespan = 0;
        /// <summary>
        /// total time damage component may live
        /// </summary>
        public int Lifespan
        {
            get { return _Lifespan; }
            set { _Lifespan = value; }
        }

        private Entity _Target;
        /// <summary>
        /// target of the damage
        /// </summary>
        public Entity Target
        {
            get { return _Target; }
            set { _Target = value; }
        }

    }
}
