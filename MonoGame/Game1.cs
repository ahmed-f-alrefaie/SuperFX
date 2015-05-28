#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using SuperEFEX.Renderer;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using SuperEFEX.Renderer.ModelContent;
using SuperFXContent;
using SuperEFEX.Core;
using SuperEFEX.Core.Components;
using StarfoxClone.Components.Test;
using SuperEFEX.Kernal;
using SuperEFEX.Core.Content;
using SuperEFEX.Core.Content.Graphics;
using SuperEFEX.GameSystem;
#endregion

namespace MonoGame
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{

		GameScene gameScene;
		XmlSerializer xmlsr;
		StreamWriter sw;
		GraphicsDeviceManager graphics;
		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			graphics.PreferredBackBufferWidth = 256*3;
			graphics.PreferredBackBufferHeight = 244*3;
			graphics.IsFullScreen = false;	
			graphics.PreferredBackBufferFormat = SurfaceFormat.Bgra32;//SurfaceFormat.Bgr565;
			Logger.Instance.Init("test.txt","");
			ProfileSampler.outputer = new ProfilerLogger();

			//gameScene = new GameScene ();
		//	gameScene.Name = "TEST";
		//	gameScene.GameObjects.Add ("Test.XML");
		//	sw = new StreamWriter ("Content/Scenes/GameScene.xml");
			//xmlsr = new XmlSerializer (typeof(GameScene));
			//xmlsr.Serialize (sw,gameScene);
		//	sw.Close ();

			gameScene = GameScene.CreateGameScene ("Content/Scenes/GameScene.xml");


			gameScene.Setup (this,null);



		}




		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			gameScene.Initialize ();
			// TODO: Add your initialization logic here
			base.Initialize ();
				
		}


		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			gameScene.LoadContent (GraphicsDevice);
			base.LoadContent ();
		}



		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			ProfileSampler.StartTimer ("Update");

			gameScene.Update (gameTime);
				base.Update (gameTime);
			ProfileSampler.StopTimer ("Update");

		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			ProfileSampler.StartTimer ("Draw");
			graphics.GraphicsDevice.Clear (Color.Black);
			gameScene.Draw (gameTime);

				
				base.Draw (gameTime);
			ProfileSampler.StopTimer ("Draw");
			
		}
			
	}
}

