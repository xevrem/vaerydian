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

using Glimpse.Controls;

using ECSFramework;

using Microsoft.Xna.Framework;

namespace Vaerydian.UI
{
    class ButtonMenu
    {
        private Entity _Owner;
        private Entity _Caller;
        private ECSInstance _ECSInstance;
        private List<GButton> _Buttons = new List<GButton>();
        private int _ButtonCount;
        private int _border;
        private int _Height;
        private int _Width;
        private int _Spacing;
        private Point _Position;
        private GForm _Form = new GForm();
        private GCanvas _Canvas = new GCanvas();
        private GFrame _Frame = new GFrame();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="caller"></param>
        /// <param name="ecsInstance"></param>
        /// <param name="buttonCount"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="border"></param>
        public ButtonMenu(Entity owner, Entity caller, ECSInstance ecsInstance, int buttonCount, Point position, int height, int width, int border, int spacing)
        {
            _Owner = owner;
            _Caller = caller;
            _ECSInstance = ecsInstance;
            _ButtonCount = buttonCount;
            _Position = position;
            _Height = height;
            _Width = width;
            _border = border;
            _Spacing = spacing;
        }

        /// <summary>
        /// Buttons in the menu
        /// </summary>
        public List<GButton> Buttons
        {
            get { return _Buttons; }
            set { _Buttons = value; }
        }

        /// <summary>
        /// form containing controlls
        /// </summary>
        public GForm Form
        {
            get { return _Form; }
            set { _Form = value; }
        }

        /// <summary>
        /// frame background
        /// </summary>
        public GFrame Frame
        {
            get { return _Frame; }
            set { _Frame = value; }
        }
        
        /// <summary>
        /// initialize menu
        /// </summary>
        public void init()
        {
            //setup form
            _Form = new GForm();
            _Form.owner = _Owner;
            _Form.caller = _Caller;
            _Form.ecs_instance = _ECSInstance;
            _Form.bounds = new Rectangle(_Position.X, _Position.Y, _Width + 2 * _border, (_Height + _Spacing) * _ButtonCount + 2 * _border);

            //setup canvas
            _Canvas = new GCanvas();
            _Canvas.owner = _Owner;
            _Canvas.caller = _Caller;
            _Canvas.ecs_instance = _ECSInstance;
            _Canvas.bounds = new Rectangle(_Position.X, _Position.Y, _Width + 2 * _border, (_Height + _Spacing) * _ButtonCount + 2 * _border);

            //setup frame
            _Frame = new GFrame();
            _Frame.owner = _Owner;
            _Frame.caller = _Caller;
            _Frame.ecs_instance = _ECSInstance;
            _Frame.bounds = new Rectangle(_Position.X, _Position.Y, _Width + 2 * _border, (_Height + _Spacing) * _ButtonCount + 2 * _border);

            //_Canvas.Controls.Add(_Frame);

            for (int i = 0; i < _ButtonCount; i++)
            {
                GButton button = new GButton();
                button.owner = _Owner;
                button.caller = _Caller;
                button.ecs_instance = _ECSInstance;
                button.bounds = new Rectangle(_Position.X + _border, _Position.Y + _border + i * (_Height + _Spacing), _Width, _Height);
				
                //_Canvas.Controls.Add(button);
                _Buttons.Add(button);
            }
            
            //_Form.CanvasControls.Add(_Canvas);
        }

        public void assemble()
        {
            _Canvas.controls.Add(_Frame);

            for (int i = 0; i < _Buttons.Count; i++)
            {
                _Canvas.controls.Add(_Buttons[i]);
            }

			_Form.canvas_controls.Add(_Canvas);
        }


    }
}
