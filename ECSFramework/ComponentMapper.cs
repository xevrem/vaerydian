//
//  ComponentMapper.cs
//
//  Author:
//       erika <>
//
//  Copyright (c) 2016 erika
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
using System.Reflection;

namespace ECSFramework
{
	public class ComponentMapper
	{
		private static ECSInstance _ecs_instance;

		private int _type_id;

		public ComponentMapper ()
		{
		}

		public ComponentMapper(Component c, ECSInstance ecs_instance){
			this._type_id = c.type_id;
			_ecs_instance = ecs_instance;
		}

		public Component get(Entity e){
			return _ecs_instance.component_manager.components[_type_id][e.id];
		}

		public static T get<T>(Entity e) where T: Component {
			//TODO
			T t = Activator.CreateInstance<T>();

			return (T) _ecs_instance.component_manager.components[t.type_id][e.id];
		}
	}
}

