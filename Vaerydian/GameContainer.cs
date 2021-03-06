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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Vaerydian
{
    public class GameContainer
    {

        private ContentManager g_ContentManager;

        public ContentManager ContentManager
        {
            get { return g_ContentManager; }
            set { g_ContentManager = value; }
        }

        private SpriteBatch g_SpriteBatch;

        public SpriteBatch SpriteBatch
        {
            get { return g_SpriteBatch; }
            set { g_SpriteBatch = value; }
        }

        private GraphicsDevice g_GraphicsDevice;

        public GraphicsDevice GraphicsDevice
        {
            get { return g_GraphicsDevice; }
            set { g_GraphicsDevice = value; }
        }


    }
}
