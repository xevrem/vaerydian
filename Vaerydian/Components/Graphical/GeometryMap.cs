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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Vaerydian.Components.Graphical
{
    public class GeometryMap : Component
    {
        private static int _type_id;
        private int _entity_id;

        public GeometryMap() { }

		public override int type_id{ 
			get{ return _type_id;} 
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

        private RenderTarget2D _ColorMap;

        public RenderTarget2D ColorMap
        {
            get { return _ColorMap; }
            set { _ColorMap = value; }
        }

        private RenderTarget2D _DepthMap;

        public RenderTarget2D DepthMap
        {
            get { return _DepthMap; }
            set { _DepthMap = value; }
        }

        private RenderTarget2D _NormalMap;

        public RenderTarget2D NormalMap
        {
            get { return _NormalMap; }
            set { _NormalMap = value; }
        }

        private RenderTarget2D _ShadingMap;

        public RenderTarget2D ShadingMap
        {
            get { return _ShadingMap; }
            set { _ShadingMap = value; }
        }

        private Texture2D _ColorMapTexture;

        public Texture2D ColorMapTexture
        {
            get { return _ColorMapTexture; }
            set { _ColorMapTexture = value; }
        }
        private Texture2D _DepthMapTexture;

        public Texture2D DepthMapTexture
        {
            get { return _DepthMapTexture; }
            set { _DepthMapTexture = value; }
        }
        private Texture2D _NormalMapTexture;

        public Texture2D NormalMapTexture
        {
            get { return _NormalMapTexture; }
            set { _NormalMapTexture = value; }
        }
        private Texture2D _ShadowMapTexture;

        public Texture2D ShadowMapTexture
        {
            get { return _ShadowMapTexture; }
            set { _ShadowMapTexture = value; }
        }

        private Vector4 _AmbientColor;

        public Vector4 AmbientColor
        {
            get { return _AmbientColor; }
            set { _AmbientColor = value; }
        }
    }
}
