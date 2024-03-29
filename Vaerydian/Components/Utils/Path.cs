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

using Microsoft.Xna.Framework;

using ECSFramework;

using Vaerydian.Utils;

namespace Vaerydian.Components.Utils
{

    enum PathState
    {
        Idle,
        DoPathing,
        PathFound,
        PathFailed
    }

    class APath : IComponent
    {
		private static int _type_id;

        public static int TypeID
        {
            get { return APath._type_id; }
            set { APath._type_id = value; }
        }

        private int p_entity_id;

        public APath() { }

		public int id { get; set;}

		public int owner_id { get; set;}

		public int type_id{
			get{ return _type_id;} 
			set{ _type_id = value;}
		}

        public int getEntityId()
        {
            return p_entity_id;
        }

        public int getTypeId()
        {
            return _type_id;
        }

        public void setEntityId(int entityId)
        {
            p_entity_id = entityId;
        }

        public void setTypeId(int typeId)
        {
            _type_id = typeId;
        }

        private Vector2 p_Start;

        public Vector2 Start
        {
            get { return p_Start; }
            set { p_Start = value; }
        }

        private Vector2 p_Finish;

        public Vector2 Finish
        {
            get { return p_Finish; }
            set { p_Finish = value; }
        }

        private GameMap p_Map;

        public GameMap Map
        {
            get { return p_Map; }
            set { p_Map = value; }
        }

        private bool p_Failed = false;

        public bool Failed
        {
            get { return p_Failed; }
            set { p_Failed = value; }
        }

        private bool p_HasRun = false;

        public bool HasRun
        {
            get { return p_HasRun; }
            set { p_HasRun = value; }
        }

        private PathState p_PathState = PathState.Idle;

        internal PathState PathState
        {
            get { return p_PathState; }
            set { p_PathState = value; }
        }

        private List<Cell> p_FoundPath;

        public List<Cell> FoundPath
        {
            get { return p_FoundPath; }
            set { p_FoundPath = value; }
        }

        private AStarPathing p_Pathing;

        public AStarPathing Pathing
        {
            get { return p_Pathing; }
            set { p_Pathing = value; }
        }

    }
}
