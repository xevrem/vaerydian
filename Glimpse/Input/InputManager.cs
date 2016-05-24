//
//  InputManager.cs
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

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Glimpse.Input
{

	public struct InputStateContainer{
		public MouseState current_mouse_state;
		public MouseState previous_mouse_state;
		public KeyboardState current_kb_state;
		public KeyboardState previous_kb_state;
	}

	public static class InputManager
	{
		public static bool YesScreenshot = false;
		public static bool YesExit = false;

		private static KeyboardState _previous_kb_state;
		private static KeyboardState _current_kb_state;
		private static MouseState _previous_m_state;
		private static MouseState _current_m_state;

		public static bool isKeyToggled(Keys key){
			return _previous_kb_state.IsKeyDown (key) && _current_kb_state.IsKeyUp (key);
		}

		public static bool isKeyPressed(Keys key){
			return _previous_kb_state.IsKeyDown (key) && _current_kb_state.IsKeyDown (key);
		}

		public static void Update(){
			_previous_kb_state = _current_kb_state;
			_current_kb_state = Keyboard.GetState ();
			_previous_m_state = _current_m_state;
			_current_m_state = Mouse.GetState ();

//			if (isKeyToggled (Keys.Escape)) {
//				InputManager.YesExit = true;
//			}
		}

		public static bool isLeftButtonDown(){
			return Mouse.GetState ().LeftButton == ButtonState.Pressed;
		}

		public static bool isRightButtonDown(){
			return Mouse.GetState ().RightButton == ButtonState.Pressed;
		}

		public static Vector2 getMousePositionVector(){
			return Mouse.GetState ().Position.ToVector2 ();
		}

		public static Point getMousePosition(){
			return Mouse.GetState ().Position;
		}

	}
}

