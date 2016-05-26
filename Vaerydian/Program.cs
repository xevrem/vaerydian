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

#if OSX
//using MonoMac.AppKit;
//using MonoMac.Foundation;
#endif

namespace Vaerydian
{

	#if WINDOWS || LINUX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {

			try{
				using (VaerydianGame game = new VaerydianGame())
            	{
					game.Run();
            	}
			}catch(Exception e){
				Console.Out.WriteLine (e.ToString ());
			}
        }
    }
	#endif

	#if OSX
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main (string[] args)
		{
			NSApplication.Init ();


			using (var p = new NSAutoreleasePool ()) {
				NSApplication.SharedApplication.Delegate = new AppDelegate ();
				NSApplication.Main (args);
			}
		}
	}

	class AppDelegate : NSApplicationDelegate
	{
		private static VaerydianGame game;

		public override void FinishedLaunching (NSObject notification)
		{
			try{
				game = new VaerydianGame();
				game.Run ();
			}catch(Exception e){
				Console.Out.WriteLine (e.ToString ());
			}
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
		{
			return true;
		}
	}
	#endif

}

