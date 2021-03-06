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

using Glimpse.Controls;

using ECSFramework;

using Vaerydian.Components.Characters;
using Vaerydian.Components.Spatials;
using Glimpse.Input;

namespace Vaerydian
{
	public class HpLabelUpdater
	{
		private ECSInstance h_ECSInstance;
		private ComponentMapper h_HealthMapper;
		//private ComponentMapper h_PositionMapper;

		public HpLabelUpdater (ECSInstance ecsInstnace)
		{
			h_ECSInstance = ecsInstnace;
			h_HealthMapper = new ComponentMapper(new Health(), h_ECSInstance);
		}

		public void updateHandler (Control control, InterfaceArgs args)
		{

			Health health = (Health) h_HealthMapper.get(control.caller);

			GLabel label = (GLabel) control;
			label.text = health.CurrentHealth + " / " + health.MaxHealth;
			label.resize();
		}
	}
}

