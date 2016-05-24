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

using ECSFramework;

using Glimpse.Controls;
using Microsoft.Xna.Framework;

namespace Vaerydian
{
	public class BasicWindow
	{
		private GForm _Form;
		private GCanvas _Canvas;
		private GFrame _Frame;
		private GButton _Button;
		private Entity _Owner;
		private Entity _Caller;
		private ECSInstance _ECSInstance;
		private Point _Position;
		private Point _Dimensions;
		private int _ButtonHeight = 10;

		/// <summary>
		/// creates a base window UI control
		/// </summary>
		/// <param name='owner'>
		/// Owning entity
		/// </param>
		/// <param name='caller'>
		/// Calling entity
		/// </param>
		/// <param name='ecsInstance'>
		/// ECSInstance entities are from
		/// </param>
		/// <param name='position'>
		/// Position to display the window
		/// </param>
		/// <param name='dimensions'>
		/// Dimensions of the window
		/// </param>
		/// <param name='buttonHeight'>
		/// Height of the top button
		/// </param>
		public BasicWindow (Entity owner, Entity caller, ECSInstance ecsInstance, Point position, Point dimensions, int buttonHeight)
		{
			_Owner = owner;
			_Caller = caller;
			_ECSInstance = ecsInstance;
			_Position = position;
			_Dimensions = dimensions;
			_ButtonHeight = buttonHeight;
		}

		/// <summary>
		/// underlying form of base window
		/// </summary>
		/// <value>
		/// GForm value
		/// </value>
		public GForm Form {
			get {
				return _Form;
			}
			set {
				_Form = value;
			}
		}

		/// <summary>
		/// underlying canvas of base window
		/// </summary>
		/// <value>
		/// GCanvas value
		/// </value>
		public GCanvas Canvas {
			get {
				return _Canvas;
			}
			set {
				_Canvas = value;
			}
		}

		/// <summary>
		/// underlying frame of base window
		/// </summary>
		/// <value>
		/// GFrame value
		/// </value>
		public GFrame Frame {
			get {
				return _Frame;
			}
			set {
				_Frame = value;
			}
		}

		/// <summary>
		/// underlying button of base window
		/// </summary>
		/// <value>
		/// GButton value
		/// </value>
		public GButton Button {
			get {
				return _Button;
			}
			set {
				_Button = value;
			}
		}

		/// <summary>
		/// Initialize the base controls
		/// </summary>
		public void init()
		{
			_Form = new GForm();
			_Form.owner = _Owner;
			_Form.caller = _Caller;
			_Form.ecs_instance = _ECSInstance;
			_Form.bounds = new Rectangle(_Position.X,_Position.Y,_Dimensions.X,_Dimensions.Y);			
			
			_Canvas = new GCanvas();
			_Canvas.owner = _Owner;
			_Canvas.caller = _Caller;
			_Canvas.ecs_instance = _ECSInstance;
			_Canvas.bounds = new Rectangle(_Position.X,_Position.Y,_Dimensions.X,_Dimensions.Y);

			_Frame = new GFrame();
			_Frame.owner = _Owner;
			_Frame.caller = _Caller;
			_Frame.ecs_instance = _ECSInstance;
			_Frame.bounds = new Rectangle(_Position.X,_Position.Y,_Dimensions.X,_Dimensions.Y);

			_Button = new GButton();
			_Button.owner = _Owner;
			_Button.caller = _Caller;
			_Button.ecs_instance = _ECSInstance;
			_Button.bounds = new Rectangle(_Position.X,_Position.Y,_Dimensions.X,_ButtonHeight);
		}

		/// <summary>
		/// Pre-assemble the controls (base frame and button)
		/// </summary>
		public void preAssemble ()
		{
			_Canvas.controls.Add(_Frame);
			_Canvas.controls.Add(_Button);
		}

		/// <summary>
		/// Assemble into the form.
		/// </summary>
		public void assemble()
		{
			_Form.canvas_controls.Add(_Canvas);
		}


	}
}

