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

using Vaerydian.Characters;

namespace Vaerydian.Components.Characters
{
    public struct FactionsDef{
		public string Name;
		public Faction OwnerFaction;
		public List<Faction> Factions;
	}

	class Factions : Component
    {
		private static int _type_id;
        private int f_entity_id;

        public Factions() { }

		public override int type_id{ 
			get{ return _type_id;} 
			set{ _type_id = value;}
		}

        public int getEntityId()
        {
            return f_entity_id;
        }

        public int getTypeId()
        {
            return _type_id;
        }

        public void setEntityId(int entityId)
        {
            f_entity_id = entityId;
        }

        public void setTypeId(int typeId)
        {
            _type_id = typeId;
        }

        private Faction f_OwnerFaction;

        public Faction OwnerFaction
        {
            get { return f_OwnerFaction; }
            set { f_OwnerFaction = value; }
        }
		/// <summary>
		/// factions known to entity
		/// </summary>
		public Dictionary<string, Faction> KnownFactions = new Dictionary<string, Faction>();
    }
}
