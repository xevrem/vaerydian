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
using Microsoft.Xna.Framework.Graphics;

namespace Vaerydian.Utils
{
    class Bone
    {
        private int _ElapsedTime;

        public int ElapsedTime
        {
            get { return _ElapsedTime; }
            set { _ElapsedTime = value; }
        }

        private int _AnimationTime;

        public int AnimationTime
        {
            get { return _AnimationTime; }
            set { _AnimationTime = value; }
        }

        private Dictionary<String, List<KeyFrame>> _Animations = new Dictionary<string, List<KeyFrame>>();

        public Dictionary<String, List<KeyFrame>> Animations
        {
            get { return _Animations; }
            set { _Animations = value; }
        }

        private String _TextureName;

        public String TextureName
        {
            get { return _TextureName; }
            set { _TextureName = value; }
        }

        private Vector2 _Origin;

        public Vector2 Origin
        {
            get { return _Origin; }
            set { _Origin = value; }
        }

        private Vector2 _RotationOrigin;

        public Vector2 RotationOrigin
        {
            get { return _RotationOrigin; }
            set { _RotationOrigin = value; }
        }

        private float _Rotation;

        public float Rotation
        {
            get { return _Rotation; }
            set { _Rotation = value; }
        }

        
    }
}
