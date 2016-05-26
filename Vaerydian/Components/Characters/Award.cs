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
using Vaerydian.Characters;

namespace Vaerydian.Components.Characters
{

	public struct AwardDef{
		public float HealthChance;
		public float SkillChance;
		public float StatChance;
		public float VictoryChance;
		public int HealthMinimum;
		public int SkillMinimum;
		public int StatMinimum;
		public int VictoryMinimum;
		public int MeleeMod;
	}

    public enum AwardType
    {
        Victory,
        SkillUp,
        Attribute,
        Health
    }


    class Award : Component
    {
		private static int _type_id;
        private int v_entity_id;

        public Award() { }

		public override int type_id{ 
			get{ return _type_id;} 
			set{ _type_id = value;}
		}

        public int getEntityId()
        {
            return v_entity_id;
        }

        public int getTypeId()
        {
            return _type_id;
        }

        public void setEntityId(int entityId)
        {
            v_entity_id = entityId;
        }

        public void setTypeId(int typeId)
        {
            _type_id = typeId;
        }

        private AwardType v_AwardType;
        /// <summary>
        /// type of award
        /// </summary>
        public AwardType AwardType
        {
            get { return v_AwardType; }
            set { v_AwardType = value; }
        }

        private Entity v_Owner;
        /// <summary>
        /// entity awarding victory (defeated enemy)
        /// </summary>
        public Entity Awarder
        {
            get { return v_Owner; }
            set { v_Owner = value; }
        }

        private Entity v_Receiver;
        /// <summary>
        /// entity receiving victory
        /// </summary>
        public Entity Receiver
        {
            get { return v_Receiver; }
            set { v_Receiver = value; }
        }

        private int v_MaxAwardable;
        /// <summary>
        /// maximum knowledge awardable
        /// </summary>
        public int MaxAwardable
        {
            get { return v_MaxAwardable; }
            set { v_MaxAwardable = value; }
        }

        private int v_MinAwardable = 1;
        /// <summary>
        /// minimum knowledge awardable
        /// </summary>
        public int MinAwardable
        {
            get { return v_MinAwardable; }
            set { v_MinAwardable = value; }
        }

        private SkillName v_SkillName;
        /// <summary>
        /// name of what is to be awarded
        /// </summary>
        public SkillName SkillName
        {
            get { return v_SkillName; }
            set { v_SkillName = value; }
        }

        private StatType v_StatType;

        public StatType StatType
        {
            get { return v_StatType; }
            set { v_StatType = value; }
        }
    }
}
