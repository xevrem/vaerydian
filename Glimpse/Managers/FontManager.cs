//
//  FontManager.cs
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
using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Glimpse.Managers
{
	public static class FontManager
	{
		public static List<string> fonts_to_load = new List<string>();
		public static ContentManager content_manager;
		public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

		public static void LoadContent(){
			foreach(string fontname in fonts_to_load){
				fonts[fontname] = content_manager.Load<SpriteFont> (fontname);
			}
		}
	}
}

