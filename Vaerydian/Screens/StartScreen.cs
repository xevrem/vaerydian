/*
      Erika V. Jonell <@xevrem>
 Author:
 
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

using Vaerydian.Factories;
using Vaerydian.UI;
using Vaerydian.Utils;

using Glimpse.Controls;
using Glimpse.Components;
using Glimpse.Systems;
using Glimpse.Input;
using Microsoft.Xna.Framework.Input;


namespace Vaerydian.Screens
{
	public struct StartDefs{
		public int Seed;
		public int SkillLevel;
		public bool Returning;
		public MapType MapType;
	}

    class StartScreen : Screen
    {
		private ECSInstance ecs_instance;
        private GameContainer s_Container;

        private EntitySystem s_UiUpdateSystem;
        private EntitySystem s_UiDrawSystem;

        private ButtonMenu s_ButtonMenu;
        private GFrame s_Frame;

		private JsonManager s_JsonManager;
		private string s_json;

        public StartScreen() { }

        public override void Initialize()
        {
            base.Initialize();

            //setup instnace and container
            ecs_instance = new ECSInstance();
            s_Container = ScreenManager.GameContainer;

			UIFactory.Container = ScreenManager.GameContainer;
			UIFactory.ecs_instance = ecs_instance;

            //define and init systems
            s_UiUpdateSystem = ecs_instance.system_manager.set_system(new UIUpdateSystem(), new UserInterface());
			s_UiDrawSystem = ecs_instance.system_manager.set_system(new UIDrawSystem(s_Container.ContentManager, s_Container.GraphicsDevice), new UserInterface());
            ecs_instance.system_manager.initialize_systems();

			//setup json manager
			s_JsonManager = new JsonManager();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            //create backdrop
            UIFactory.createFrame(null, new Point(0, 0), s_Container.GraphicsDevice.Viewport.Height, s_Container.GraphicsDevice.Viewport.Width, "Title");

            
            //create menu
            Entity e = ecs_instance.create();

            int border = 10;
            int spacing = 5;
            int height = 38;
            int width = 100;
            Point screen = new Point(s_Container.GraphicsDevice.Viewport.Width,s_Container.GraphicsDevice.Viewport.Height);
            Point location = new Point(screen.X / 2 - (width + 2 * border) / 2, screen.Y / 2);

            s_ButtonMenu = new ButtonMenu(e, null, ecs_instance, 3, location, height, width, border, spacing);


            s_ButtonMenu.init();

			s_ButtonMenu.Frame.background_name = "frame";
            s_ButtonMenu.Frame.background_color = Color.Black;
			s_ButtonMenu.Frame.transparency = 0.75f;            
           
			s_ButtonMenu.Buttons[0].background_name = "test_dialog";
            s_ButtonMenu.Buttons[0].background_color = Color.Gray;
            s_ButtonMenu.Buttons[0].transparency = 1f; //0.75f;
            s_ButtonMenu.Buttons[0].border = 10;
            s_ButtonMenu.Buttons[0].font_name = "General";
            s_ButtonMenu.Buttons[0].autosize = false;
            s_ButtonMenu.Buttons[0].center_text = true;
			s_ButtonMenu.Buttons[0].text = "New Game";
            s_ButtonMenu.Buttons[0].text_color = Color.White;
			s_ButtonMenu.Buttons[0].mouse_hover += change_button_on_hover;
			s_ButtonMenu.Buttons[0].mouse_press += change_button_on_press;
			s_ButtonMenu.Buttons[0].mouse_leave += change_button_on_leave;
			s_ButtonMenu.Buttons[0].mouse_click += OnMouseClickNewGame;

			s_ButtonMenu.Buttons[1].background_name = "test_dialog";           
            s_ButtonMenu.Buttons[1].background_color = Color.Gray;
            s_ButtonMenu.Buttons[1].transparency = 1f;
			s_ButtonMenu.Buttons[1].border = 10;
            s_ButtonMenu.Buttons[1].font_name = "General";
            s_ButtonMenu.Buttons[1].autosize = false;
            s_ButtonMenu.Buttons[1].center_text = true;
			s_ButtonMenu.Buttons[1].text = "World Gen";
			s_ButtonMenu.Buttons[1].text_color = Color.White;
			s_ButtonMenu.Buttons[1].mouse_hover += change_button_on_hover;
			s_ButtonMenu.Buttons[1].mouse_press += change_button_on_press;
			s_ButtonMenu.Buttons[1].mouse_leave += change_button_on_leave;
			s_ButtonMenu.Buttons[1].mouse_click += OnMouseClickNewGame;

			s_ButtonMenu.Buttons[2].background_name = "test_dialog";
			s_ButtonMenu.Buttons[2].background_color = Color.Gray;
            s_ButtonMenu.Buttons[2].transparency = 1f;
            s_ButtonMenu.Buttons[2].border = 10;
            s_ButtonMenu.Buttons[2].font_name = "General";
            s_ButtonMenu.Buttons[2].autosize = false;
            s_ButtonMenu.Buttons[2].center_text = true;
			s_ButtonMenu.Buttons[2].text = "Exit Game";
			s_ButtonMenu.Buttons[2].text_color = Color.White;
			s_ButtonMenu.Buttons[2].mouse_hover += change_button_on_hover;
			s_ButtonMenu.Buttons[2].mouse_press += change_button_on_press;
			s_ButtonMenu.Buttons[2].mouse_leave += change_button_on_leave;
			s_ButtonMenu.Buttons[2].mouse_click += OnMouseClickNewGame;            


            s_ButtonMenu.assemble();

            UserInterface ui = new UserInterface(s_ButtonMenu.Form);

            ecs_instance.add_component(e, ui);

            ecs_instance.resolve(e);

			//load the json start screen file
			s_json = s_JsonManager.loadJSON(GameConfig.root_dir + "/Content/json/start_screen.v");

            //create mouse pointer
			s_Frame = UIFactory.createMousePointer(InputManager.getMousePosition(), 10, 10, "pointer", OnMousePointerUpdate);

            //early entity reslove
            ecs_instance.resolve_entities();

            //load system content
            ecs_instance.system_manager.systems_load_content();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            ecs_instance.clean_up();

            GC.Collect();
        }

        public override void hasFocusUpdate(GameTime gameTime)
        {
            base.hasFocusUpdate(gameTime);

            //check to see if escape was recently pressed
            if (InputManager.isKeyToggled(Keys.Escape))
            {
                InputManager.YesExit = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            s_UiUpdateSystem.process();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

			ScreenManager.GameContainer.GraphicsDevice.Clear(Color.Black);

			s_UiDrawSystem.process();
        }

        private void OnMouseClickNewGame(Control control, InterfaceArgs args) 
        {
            //dispose of this screen
            this.ScreenManager.removeScreen(this);

            //setup new game parameters
            object[] parameters = new object[GameScreen.GAMESCREEN_PARAM_SIZE];
			parameters [GameScreen.GAMESCREEN_SEED] = GameConfig.StartDefs.Seed;
			parameters [GameScreen.GAMESCREEN_SKILLLEVEL] = GameConfig.StartDefs.SkillLevel;
			parameters [GameScreen.GAMESCREEN_RETURNING] = false;
			parameters [GameScreen.GAMESCREEN_LAST_PLAYER_POSITION] = null;

			//load the world screen
			NewLoadingScreen.Load(this.ScreenManager, true, new GameScreen(true,GameConfig.StartDefs.MapType,parameters));
        }

        private void OnMouseClickWorldGen(Control control, InterfaceArgs args)
        {
            //dispose of this screen
            this.ScreenManager.removeScreen(this);


            //load the world screen
            //LoadingScreen.Load(this.ScreenManager, true, new WorldScreen());
			NewLoadingScreen.Load(this.ScreenManager,true,new WorldScreen());
        }

        private void OnMouseClickExit(Control control, InterfaceArgs args)
        {
            //tell the input manager that the player wants to quit
            InputManager.YesExit = true;
        }

        private void OnMousePointerUpdate(Control control, InterfaceArgs args)
        {
			control.bounds = new Rectangle(args.state_container.current_mouse_state.Position.X, args.state_container.current_mouse_state.Position.Y, 10, 10);
        }

		private void change_button_on_press(Control sender, InterfaceArgs args){
			sender.background_name = "test_dialog2";
			sender.text_color = Color.Red;
		}

		private void change_button_on_hover(Control sender, InterfaceArgs args){
			sender.background_name = "test_dialog2";
			sender.text_color = Color.Yellow;
		}

		private void change_button_on_leave(Control sender, InterfaceArgs args){
			sender.background_name = "test_dialog1";
			sender.text_color = Color.White;
		}

    }
}
