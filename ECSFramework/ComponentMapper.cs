﻿//
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

namespace ECSFramework
{
	public class ComponentMapper
	{
		public static ECSInstance ecs_instance;

		public ComponentMapper ()
		{
		}

		public ComponentMapper(Component c, ECSInstance ecs_instance){
			//TODO
		}

		public object get(Entity e){
			//TODO
			return default(object);
		}

		public static T get<T>(Entity e){
			//TODO
			return default(T);
		}
	}
}

