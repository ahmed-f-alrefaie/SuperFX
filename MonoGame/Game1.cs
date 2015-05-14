﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Renderer;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using Renderer.ModelContent;
using SuperFXContent;
using Core;
using Kernal;
#endregion

namespace MonoGame
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		PixelPlotter plotter;
		PixelModel arwing;
		PixelModel arwing2;
		Vector3 position = Vector3.Zero;
		Vector3 rotation = Vector3.Zero;
		Vector3 scale = Vector3.One;
		Camera cam;
		PixelModelData arwingData;
		Rasterizer3D raster;
		Background titania;
		BackgroundRenderer bkgRenderer;
		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			graphics.PreferredBackBufferWidth = 256*3;
			graphics.PreferredBackBufferHeight = 244*3;
			graphics.IsFullScreen = false;	
			graphics.PreferredBackBufferFormat = SurfaceFormat.Bgra32;//SurfaceFormat.Bgr565;
			Logger.Instance.Init();
			ProfileSampler.outputer = new ProfilerLogger();
			GameObject go = new GameObject ();
			XmlSerializer xml = new XmlSerializer (go.GetType ());
			StreamWriter sw = new StreamWriter("../../../../../Test2.xml");
			xml.Serialize (sw, go);



		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			cam = new Camera (256,244);
			// TODO: Add your initialization logic here
			base.Initialize ();
				
		}

		float t1 = 0;
		float t = 0.0f;
		float incr = 7.0f;
		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);
			plotter = new PixelPlotter (GraphicsDevice,256, 224);
			plotter.SetClearColor (Color.Black);

			raster = new Rasterizer3D (spriteBatch,plotter);
			bkgRenderer = new BackgroundRenderer (spriteBatch,256,224);
			//TODO: use this.Content to load your game content here 
			//arwing = new PixelModel(verticies,faces,true);
			//Console.Write(typeof(PixelModelDataReader).AssemblyQualifiedName);
			arwingData = Content.Load<PixelModelData>("arwing_color");
			arwing = new PixelModel (arwingData);
			raster.AddPolygons (arwing.GET);
			//raster.AddPolygons (arwing2.GET);
			position.X = 00.0f;
			position.Y = 0.0f;
			position.Z = 80.0f + ((float)Math.Sin ((float)t1) * 2.0f);

			scale.X = 2.0f;
			scale.Y = 2.0f;
			scale.Z = 2.0f;
			arwing.SetLighting (3, false);
			arwing.SetLighting (37, false);
			arwing.SetLighting (24, false);
			titania = new Background ();
			titania.Filename = "62819";
			titania.LoadContent (Content);
	
			//arwing2.SetPosition (0.0f, 5.0f, 50.0f);
			//arwing2.SetRotation (0.0f, 5.0f, 700.0f);
			//arwing2.SetScale (3.0f, 3.0f, 3.0f);

		}
		float rotationSpeed = 2.0f;
		float forwardSpeed = 50.0f;
		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			ProfileSampler.StartTimer ("Update");
				// For Mobile devices, this logic will close the Game when the Back button is pressed
				// Exit() is obsolete on iOS
				#if !__IOS__
				if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				   Keyboard.GetState ().IsKeyDown (Keys.Escape)) {

					Exit ();
				}
			if(Keyboard.GetState ().IsKeyDown (Keys.A))
				raster.SetRenderMode(RasterType.WIREFRAME);
			else
				raster.SetRenderMode(RasterType.FLAT_SHADED);
				//plotter.DrawLine(0,0,256,256,Color.Red);

				#endif
				cam.Update ();
				position.Z = 80.0f + ((float)Math.Sin ((float)t1) * 30.0f);
				t1 += 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
				rotation.Z = MathHelper.Pi + 1.0f * ((float)Math.Sin ((float)t1));
				//	position.Y -= 1.0f;
				rotation.X -= 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
				//rotation.Y -= 0.2f*(float)gameTime.ElapsedGameTime.TotalSeconds;
				// TODO: Add your update logic here			
			arwing.SetColor(3,Color.Lerp (Color.Red, Color.Yellow,t));
			arwing.SetColor(37,Color.Lerp (Color.Blue, Color.LightBlue,t));
			arwing.SetColor(24,Color.Lerp (Color.Blue, Color.LightBlue,t));
			arwing.SetPosition (position.X, position.Y, position.Z);
			arwing.SetRotation (rotation.X, rotation.Y, rotation.Z);
			arwing.SetScale (scale.X, scale.Y, scale.Z);

			t += incr* (float)gameTime.ElapsedGameTime.TotalSeconds;;
			if (t > 1.0f || t < 0) {
				incr *= -1.0f;
			}


			KeyboardState keyboardState = Keyboard.GetState();
			GamePadState currentState = GamePad.GetState( PlayerIndex.One );

			if (keyboardState.IsKeyDown( Keys.Left ) || (currentState.DPad.Left == ButtonState.Pressed))
			{
				// Rotate left.
				cam.Rotation = cam.Rotation + new Vector3(0, rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds,0);
			}
			if (keyboardState.IsKeyDown( Keys.Right ) || (currentState.DPad.Right == ButtonState.Pressed))
			{
				// Rotate right.
				cam.Rotation = cam.Rotation - new Vector3(0, rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds,0);
			}
			if (keyboardState.IsKeyDown( Keys.Up ) || (currentState.DPad.Up == ButtonState.Pressed))
			{
				Matrix forwardMovement = Matrix.CreateRotationY( cam.Rotation.Y );
				Vector3 v = new Vector3( 0, 0, forwardSpeed );
				v = Vector3.Transform( v, forwardMovement );
				cam.Position = cam.Position + new Vector3(v.X* (float)gameTime.ElapsedGameTime.TotalSeconds,0,v.Z* (float)gameTime.ElapsedGameTime.TotalSeconds);//* (float)gameTime.ElapsedGameTime.TotalSeconds;
				//cam.Position.X += v.X;
			}
			if (keyboardState.IsKeyDown( Keys.Down ) || (currentState.DPad.Down == ButtonState.Pressed))
			{
				Matrix forwardMovement = Matrix.CreateRotationY( cam.Rotation.Y );
				Vector3 v = new Vector3( 0, 0, -forwardSpeed );
				v = Vector3.Transform( v, forwardMovement );
				cam.Position = cam.Position + new Vector3 (v.X* (float)gameTime.ElapsedGameTime.TotalSeconds, 0, v.Z* (float)gameTime.ElapsedGameTime.TotalSeconds);//;
				//cam.Position.X += v.X;
			}
		
			if (keyboardState.IsKeyDown (Keys.Q)) {
				
				cam.Rotation = new Vector3 (cam.Rotation.X, cam.Rotation.Y, MathHelper.Lerp (cam.Rotation.Z, MathHelper.ToRadians (-30.0f), 0.05f));
			} else if (keyboardState.IsKeyDown (Keys.W)) {
				cam.Rotation = new Vector3 (cam.Rotation.X, cam.Rotation.Y, MathHelper.Lerp (cam.Rotation.Z, MathHelper.ToRadians (30.0f), 0.05f));

			} else {
				cam.Rotation = new Vector3 (cam.Rotation.X, cam.Rotation.Y, MathHelper.Lerp (cam.Rotation.Z, MathHelper.ToRadians (0.0f), 0.05f));

			}
				





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
		

				//TODO: Add your drawing code here
			spriteBatch.Begin (SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, null, null, Matrix.CreateScale ((float)graphics.GraphicsDevice.Viewport.Width/256.0f,(float)graphics.GraphicsDevice.Viewport.Height/224.0f,1.0f));
		
				//arwing.Draw (cam, plotter);
				//raster.Draw (gameTime);
			    RendererBase.Draw(gameTime);
				spriteBatch.End ();
				base.Draw (gameTime);
			ProfileSampler.StopTimer ("Draw");
			
		}
			
	}
}
