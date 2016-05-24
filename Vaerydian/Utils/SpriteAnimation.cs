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

namespace Vaerydian.Utils
{
    public class SpriteAnimation
    {
        private int _FrameRate;

        private int _Frames;

        private int _ElapsedTime = 0;

        private int _LastFrame = 0;

        public SpriteAnimation() { }

        public SpriteAnimation(int frames, int frameRate) 
        {
            _Frames = frames;
            _FrameRate = frameRate;
        }

        public void reset()
        {
            _ElapsedTime = 0;
            _LastFrame = 0;
        }

        public int updateFrame(GameTime gameTime)
        {
            _ElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (_ElapsedTime > _FrameRate)
            {
                //reset elapsed
                _ElapsedTime = 0;

                //update frame
                _LastFrame++;

                //make sure we didnt run over
                if (_LastFrame == _Frames)
                    _LastFrame = 0;

                //return frame
                return _LastFrame;
            }
            else
                return _LastFrame;
        }

        public int updateFrame(int gameTime)
        {
            _ElapsedTime += gameTime;

            if (_ElapsedTime > _FrameRate)
            {
                //reset elapsed
                _ElapsedTime = 0;

                //update frame
                _LastFrame++;

                //make sure we didnt run over
                if (_LastFrame == _Frames)
                    _LastFrame = 0;

                //return frame
                return _LastFrame;
            }
            else
                return _LastFrame;
        }

        /// <summary>
        /// number of frames in this animation
        /// </summary>
        public int Frames
        {
            get { return _Frames; }
            set { _Frames = value; }
        }
    }
}
