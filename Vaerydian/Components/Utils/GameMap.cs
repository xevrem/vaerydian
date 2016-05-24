﻿/*
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

namespace Vaerydian.Components.Utils
{
    public class GameMap : Component
    {
        private static int g_type_id;
        private int g_entity_id;

        private Map g_Map;

        public Map Map
        {
            get { return g_Map; }
            set { g_Map = value; }
        }

        private byte[,] g_ByteMap;

        public byte[,] ByteMap
        {
            get { return g_ByteMap; }
            set { g_ByteMap = value; }
        }

        private List<Cell> g_Path = new List<Cell>();

        public List<Cell> Path
        {
            get { return g_Path; }
            set { g_Path = value; }
        }

        private List<Cell> g_Blocking = new List<Cell>();

        public List<Cell> Blocking
        {
            get { return g_Blocking; }
            set { g_Blocking = value; }
        }

        public GameMap() { }

        public GameMap(Map map)
        {
            g_Map = map;
        }

        public int getEntityId()
        {
            return g_entity_id;
        }

        public int getTypeId()
        {
            return g_type_id;
        }

		public static int TypeID {
			get {
				return g_type_id;
			}
			set {
				g_type_id = value;
			}
		}

        public void setEntityId(int entityId)
        {
            g_entity_id = entityId;
        }

        public void setTypeId(int typeId)
        {
            g_type_id = typeId;
        }

        public Terrain getTerrain (int x, int y)
		{
			try {
				if ((x < g_Map.XSize) && (y < g_Map.YSize) && (x >= 0) && (y >= 0))
					return g_Map.Terrain [x, y];
				else
					return null;
			} catch (Exception) {
				Console.Error.WriteLine("ERROR IN RETRIEVING TERRAIN - x: {0} | y: {1} | mapX: {2} | mapY: {3}",x,y,g_Map.XSize, g_Map.YSize);
				return null;
			}
        }
    }
}
