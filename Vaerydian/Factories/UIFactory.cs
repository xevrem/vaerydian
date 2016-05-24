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


using Vaerydian.Components;
using Vaerydian.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Glimpse.Components;
using Glimpse.Controls;
using Glimpse.Input;
using Vaerydian.Characters;
using Vaerydian.Components.Spatials;
using Vaerydian.Components.Graphical;


namespace Vaerydian.Factories
{
    public static class UIFactory
    {
        public static ECSInstance ecs_instance;
        public static GameContainer Container;
        private static Random rand = new Random();


        /// <summary>
        /// creates a timed dialog
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="dialog"></param>
        /// <param name="origin"></param>
        /// <param name="name"></param>
        /// <param name="duration"></param>
        public static void createTimedDialogWindow(Entity caller, String dialog, Vector2 origin, String name, int duration)
        {
            Entity e = ecs_instance.create();

            DialogTimer timer = new DialogTimer(duration,ecs_instance);

            GForm form = new GForm();
			form.caller = caller;
            form.owner = e;
            form.ecs_instance = ecs_instance;
            form.bounds = new Rectangle((int)origin.X, (int)origin.Y, 100, 50);

            GCanvas canvas = new GCanvas();
            canvas.caller = caller;
            canvas.owner = e;
            canvas.ecs_instance = ecs_instance;
            canvas.bounds = new Rectangle((int)origin.X, (int)origin.Y, 200, 200);

            GTextBox textBox = new GTextBox();
			textBox.caller = caller;
            textBox.owner = e;
            textBox.ecs_instance = ecs_instance;
            textBox.bounds = new Rectangle((int)origin.X, (int)origin.Y, 100, 10);
            textBox.font_name = "StartScreen";
			textBox.background_name = "dialog_bubble";
            textBox.border = 10;
			textBox.text = dialog;
			textBox.text_color = Color.White;
            textBox.center_text = true;
            textBox.background_color = Color.Black;
            textBox.transparency = 0.75f;

            textBox.updating += timer.updateHandler;

            canvas.controls.Add(textBox);

			form.canvas_controls.Add(canvas);

            UserInterface ui = new UserInterface(form);

            //assign component and issue refresh
            ecs_instance.add_component(e, ui);
            ecs_instance.resolve(e);
        }

        public static void createFrame(Entity caller, Point Position, int height, int width, string textureName)
        {
            Entity e = ecs_instance.create();

            GForm form = new GForm();
            form.caller = caller;
            form.owner = e;
            form.ecs_instance = ecs_instance;
            form.bounds = new Rectangle(Position.X, Position.Y, width, height);

            GCanvas canvas = new GCanvas();
            canvas.caller = caller;
            canvas.owner = e;
            canvas.ecs_instance = ecs_instance;
            canvas.bounds = new Rectangle(Position.X, Position.Y, width, height);

            GFrame frame = new GFrame();
            frame.caller = caller;
            frame.owner = e;
            frame.ecs_instance = ecs_instance;
            frame.bounds = new Rectangle(Position.X, Position.Y, width, height);
			frame.background_name = textureName;

            canvas.controls.Add(frame);

            form.canvas_controls.Add(canvas);

            UserInterface ui = new UserInterface(form);

            ecs_instance.component_manager.add_component(e, ui);

            ecs_instance.resolve(e);
        }

        public static GFrame createMousePointer(Point position, int width, int height, string textureName, InterfaceHandler handler)
        {
            Entity e = ecs_instance.create();

            GForm form = new GForm();
            form.owner = e;
            form.ecs_instance = ecs_instance;
            form.bounds = new Rectangle(position.X, position.Y, width, height);

            GCanvas canvas = new GCanvas();
            canvas.owner = e;
            canvas.ecs_instance = ecs_instance;
            canvas.bounds = new Rectangle(position.X, position.Y, width, height);

            GFrame frame = new GFrame();
            frame.owner = e;
			frame.ecs_instance = ecs_instance;
            frame.bounds = new Rectangle(position.X, position.Y, width, height);
            frame.background_name = textureName;

            frame.updating += handler;

            canvas.controls.Add(frame);

            form.canvas_controls.Add(canvas);

            UserInterface ui = new UserInterface(form);

            ecs_instance.component_manager.add_component(e, ui);

            ecs_instance.resolve(e);

            return frame;
        }

        public static void createFloatingText(string text, string font, Color color, int timeToLive, Position position)
        {
            Entity e = ecs_instance.create();

            FloatingText fText = new FloatingText();
            fText.Text = text;
            fText.FontName = font;
            fText.Color = color;
            fText.Lifetime = timeToLive;

            ecs_instance.add_component(e, fText);
            ecs_instance.add_component(e, position);

            ecs_instance.resolve(e);
        }

		public static void createHitPointLabel(Entity entity, int width, int height, Point position)
		{
			Entity e = ecs_instance.create();

			HpLabelUpdater updater = new HpLabelUpdater(ecs_instance);

			GForm form = new GForm();
            form.owner = e;
            form.ecs_instance = ecs_instance;
            form.bounds = new Rectangle(position.X, position.Y, width, height);

            GCanvas canvas = new GCanvas();
            canvas.owner = e;
			canvas.ecs_instance = ecs_instance;
            canvas.bounds = new Rectangle(position.X, position.Y, width, height);

			GLabel label = new GLabel();
			label.owner = e;
			label.caller = entity;
			label.ecs_instance = ecs_instance;
			label.bounds = new Rectangle(position.X, position.Y, width, height);
			label.font_name = "StartScreen";
			label.border = 10;
			label.background_name = "dialog_bubble";
			label.background_color = Color.Black;
			label.transparency = 0.5f;
			label.center_text = true;
			label.text = "XXX / XXX";
			label.text_color = Color.Red;
			label.updating += updater.updateHandler;


			canvas.controls.Add(label);
			form.canvas_controls.Add(canvas);

			UserInterface ui = new UserInterface(form);

            ecs_instance.component_manager.add_component(e, ui);

            ecs_instance.resolve(e);
		}

		public static Entity createStatWindow(Entity caller, Point position, Point dimensions, int buttonHeight)
		{
			Entity e = ecs_instance.create();

			BasicWindow window = new BasicWindow(e, caller, ecs_instance, position, dimensions, buttonHeight);

			//initialize the window
			window.init();

			//setup background frame
			window.Frame.background_color = Color.White;
			window.Frame.background_name = "whitebg";
			window.Frame.transparency = 0.5f;

			//setup close button
			window.Button.background_color = Color.Gray;
			window.Button.background_name = "test_dialog";
            window.Button.font_name = "General";
            window.Button.center_text = true;
			window.Button.text = "Player Stats";
			window.Button.text_color = Color.White;
            
            

			//setup mouse events
			window.Button.mouse_hover += change_button_on_hover;
			window.Button.mouse_press += change_button_on_press;
			window.Button.mouse_leave += change_button_on_leave;
			window.Button.mouse_click += destroyUI;

			//pre-assemble window
			window.preAssemble();

			//add any custom controls here to window.Canvas
			GLabel label = new GLabel();
			label.owner = e;
			label.caller = caller;
			label.ecs_instance = ecs_instance;
			label.bounds = new Rectangle(window.Form.bounds.Left + 20,
			                             window.Form.bounds.Top + 40,
			                             dimensions.X - 40,
			                             dimensions.Y-60);
			label.autosize = false;
			label.text = "stuffs";
			label.font_name = "General";
			label.border = 0;
			label.text_color = Color.White;
			label.background_name = "frame";
			label.background_color = Color.Black;
			label.transparency = 0.5f;
			label.updating += labelUpdate;

			//add controls to canvas
			window.Canvas.controls.Add(label);

			//final assemble
			window.assemble();

			//create the UI component and assign it to the entity
			UserInterface ui = new UserInterface(window.Form);

            ecs_instance.component_manager.add_component(e, ui);

            ecs_instance.resolve(e);

			return e;
		}

		private static void change_button_on_press(Control sender, InterfaceArgs args){
			sender.background_name = "test_dialog2";
			sender.text_color = Color.Red;
		}

		private static void change_button_on_hover(Control sender, InterfaceArgs args){
			sender.background_name = "test_dialog2";
			sender.text_color = Color.Yellow;
		}

		private static void change_button_on_leave(Control sender, InterfaceArgs args){
			sender.background_name = "test_dialog1";
			sender.text_color = Color.White;
		}

		private static void destroyUI(Control sender, InterfaceArgs args)
		{
			sender.ecs_instance.delete_entity(sender.owner);
		}

		private static void labelUpdate(Control sender, InterfaceArgs args)
		{
			Vaerydian.Components.Characters.Skills skills = ComponentMapper.get<Vaerydian.Components.Characters.Skills>(sender.caller);
			Vaerydian.Components.Characters.Statistics attributes = ComponentMapper.get<Vaerydian.Components.Characters.Statistics>(sender.caller);

			GLabel label = (GLabel) sender;
			label.text = "  Skills" + "\n" +
				"    Range: " + skills.Ranged.Value + "\n" +
				"    Melee: " + skills.Melee.Value + "\n" +
				"    Avoidance: " + skills.Avoidance.Value + "\n" +
				"\n" +
				"  Attributes" + "\n" +
				"    Endurance: " + attributes.Endurance.Value + "\n" +
				"    Focus: " + attributes.Focus.Value + "\n" +
				"    Mind: " + attributes.Mind.Value + "\n" +
				"    Muscle: " + attributes.Muscle.Value + "\n" +
				"    Perception: " + attributes.Perception.Value + "\n" +
				"    Personality: " + attributes.Personality.Value + "\n" +
				"    Quickness: " + attributes.Quickness.Value;
		}

		public static Entity createInventoryWindow(Entity caller, 
		                                     Point position, Point dimensions, 
		                                     int buttonHeight, int rows, int cols){
			Entity e = ecs_instance.create ();
			
			BasicWindow window = new BasicWindow (e, caller, ecs_instance, position, dimensions, buttonHeight);
		
			//initialize the window
			window.init();

			//setup background frame
			window.Frame.background_color = Color.White;
			window.Frame.background_name = "whitebg";
			window.Frame.transparency = 0.5f;

			//setup close button
			window.Button.background_name = "test_dialog";
			window.Button.background_color = Color.Gray;
			window.Button.transparency = 1f;
			window.Button.border = 0;
			window.Button.font_name = "General";

			window.Button.center_text = true;
			window.Button.text = "Inventory";
			window.Button.text_color = Color.White;


			//setup button mouse events
			window.Button.mouse_hover += change_button_on_hover;
			window.Button.mouse_press += change_button_on_press;
			window.Button.mouse_leave += change_button_on_leave;
			window.Button.mouse_click += destroyUI;

			//pre-assemble window
			window.preAssemble();

			//add what i need here
			int x = window.Frame.bounds.X;
			int y = window.Frame.bounds.Y + buttonHeight;

			int xSize = window.Frame.bounds.Width / cols;
			int ySize = (window.Frame.bounds.Height - buttonHeight) / rows;

			for (int i = 0; i < cols; i++) {
				for(int j = 0; j < rows; j++){
					GFrame slot = new GFrame ();
					slot.owner = e;
					slot.caller = caller;
					slot.ecs_instance = ecs_instance;
					slot.bounds = new Rectangle (x + i*xSize, y + j*ySize, xSize-1, ySize-1);

					slot.background_color = Color.White;
					slot.background_name = "frame";
					slot.transparency = 0.5f;

					slot.mouse_click += getItem;

					window.Canvas.controls.Add (slot);
				}
			}

			//final assemble
			window.assemble();

			//create the UI component and assign it to the entity
			UserInterface ui = new UserInterface(window.Form);

			ecs_instance.component_manager.add_component(e, ui);

			ecs_instance.resolve (e);

			return e;
		}

		public static void getItem(Control sender, InterfaceArgs args){
			//TODO
		}

		public static void createConsole(){
			Entity e = UIFactory.ecs_instance.create ();

			GForm form = new GForm ();

			GCanvas canvas = new GCanvas ();

			GTextBox textBox = new GTextBox ();
		}
    }
}
