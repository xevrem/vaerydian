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

namespace Vaerydian.Components.Characters
{
    class Health: IComponent
    {
		private static int _type_id;
        private int h_entity_id;

        public Health() { }

        public Health(int max)
        {
            h_MaxHealth = max;
            h_CurrentHealth = max;
        }

		public int id { get; set;}

		public int owner_id { get; set;}

		public int type_id{
			get{ return _type_id;} 
			set{ _type_id = value;}
		}

        public int getEntityId()
        {
            return h_entity_id;
        }

        public int getTypeId()
        {
            return _type_id;
        }

        public void setEntityId(int entityId)
        {
            h_entity_id = entityId;
        }

        public void setTypeId(int typeId)
        {
            _type_id = typeId;
        }

        private int h_MaxHealth;

        public int MaxHealth
        {
            get { return h_MaxHealth; }
            set { h_MaxHealth = value; }
        }

        private int h_CurrentHealth;

        public int CurrentHealth
        {
            get { return h_CurrentHealth; }
            set { h_CurrentHealth = value; }
        }

        private int h_RecoveryRate;

        public int RecoveryRate
        {
            get { return h_RecoveryRate; }
            set { h_RecoveryRate = value; }
        }

        private int h_RecoveryAmmount;

        public int RecoveryAmmount
        {
            get { return h_RecoveryAmmount; }
            set { h_RecoveryAmmount = value; }
        }

        private int h_TimeSinceLastRecover;

        public int TimeSinceLastRecover
        {
            get { return h_TimeSinceLastRecover; }
            set { h_TimeSinceLastRecover = value; }
        }
    }
}
