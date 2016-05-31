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
//
//  Target.cs
//
//  Author:
//       tom <>
//
//  Copyright (c) 2013 tom
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using ECSFramework;

namespace Vaerydian.Components.Utils
{
	public class Target : IComponent
	{
		private static int _type_id;
		private int t_entity_id;

		public Target() { }

		public int id { get; set;}

		public int owner_id { get; set;}

		public int type_id{
			get{ return _type_id;} 
			set{ _type_id = value;}
		}

		public int getEntityId()
		{
			return t_entity_id;
		}

		public int getTypeId()
		{
			return _type_id;
		}

		public static int TypeId{ get { return _type_id; } set { _type_id = value; } }

		public void setEntityId(int entityId)
		{
			t_entity_id = entityId;
		}

		public void setTypeId(int typeId)
		{
			_type_id = typeId;
		}

		public Entity TargetEntity;

		public bool Active = false;
	}
}

