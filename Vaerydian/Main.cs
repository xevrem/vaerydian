﻿// #region Using Statements
// using System;
// using System.Collections.Generic;
// using System.Linq;

// using MonoMac.AppKit;
// using MonoMac.Foundation;

// using Vaerydian;

// #endregion

// namespace Vaerydian
// {
// 	static class _Program
// 	{
// 		/// <summary>
// 		/// The main entry point for the application.
// 		/// </summary>
// 		static void Main (string[] args)
// 		{
// 			NSApplication.Init ();

// 			using (var p = new NSAutoreleasePool ()) {
// 				NSApplication.SharedApplication.Delegate = new AppDelegate ();
// 				NSApplication.Main (args);
// 			}
// 		}
// 	}

// 	class AppDelegate : NSApplicationDelegate
// 	{
// 		private static VaerydianGame game;

// 		public override void FinishedLaunching (NSObject notification)
// 		{
// 			game = new VaerydianGame ();
// 			game.Run ();
// 		}

// 		public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
// 		{
// 			return true;
// 		}
// 	}

// }


