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
#endregion

namespace MonoGame
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		
		Sprite fencer = new Sprite ();
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		PixelPlotter plotter,mode7plot;
		//PixelModel arwing;
		//PixelModel arwing2;
		Vector3 position = Vector3.Zero;
		Vector3 rotation = Vector3.Zero;
		Vector3 scale = Vector3.One;
		Camera cam;
		GameObject[] arwingClone = new GameObject[50];
		PixelModelData arwingData;
		Rasterizer3D raster;
		AffineBackground titania;
		BackgroundRenderer bkgRenderer;
		TexturedPlane andross;
		GameObject arwingGameObject;
		PixelModelComponent modelComponent;
		ArwingMovingComponent arwingMove;
		FloorGrid floor;
		FXContent content;
		Mode7 testMode7;
		int horizon = 0;
		float m7D = (256.0f/2.0f)/((float)Math.Tan(MathHelper.ToRadians(45.0f/2.0f)));
		SpriteData testSprite;
		PixelSpriteRenderer renderSprite;
		BatchSpriteRenderer batchSprite;
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

			//modelComponent = new PixelModelComponent ();
			//arwingMove = new ArwingMovingComponent ();
			//modelComponent.Filename = "arwing_color";
			XmlSerializer xml = new XmlSerializer (typeof(GameObject),new Type[]{typeof(PixelModelComponent),typeof(ArwingMovingComponent)});
			StreamReader sr = new StreamReader("../../../../../Test2.xml");
			arwingGameObject = (GameObject)xml.Deserialize (sr);

			arwingGameObject.Start ();


			//arwingGameObject.AddComponent (modelComponent);
			//arwingGameObject.AddComponent (arwingMove);
			//sw.AutoFlush = true;
			//xml.Serialize (sw, arwingGameObject);
			content = new FXContent(this);

			sr.Close ();

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
			arwingGameObject.Initialise ();

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
			mode7plot = new PixelPlotter (GraphicsDevice,256, 224);
			mode7plot.SetClearColor(Color.Transparent);
			renderSprite = new PixelSpriteRenderer (mode7plot);
			raster = new Rasterizer3D (spriteBatch,plotter);
			andross = new TexturedPlane (content);
			plotter.SetClearColor (Color.Black);
			arwingGameObject.LoadContent (content);
			batchSprite = new BatchSpriteRenderer ();
			batchSprite.SpriteBatch = spriteBatch;
			testSprite = content.Load<SpriteData> ("newSpriteSheet");
			//testMode7.hBlankFunc = HblankTest;
			fencer.Filename = "newSpriteSheet";
			fencer.LoadContent (content);
			//fencer.origin = new Vector3 (20, 20);
			bkgRenderer = new BackgroundRenderer (spriteBatch,plotter,256,224);
			titania = new AffineBackground (2);
			titania.TextureFile = "62819";
			titania.LoadContent (content);
			fencer.PlayAnimation ("Run_RIGHT",-1);
			//testMode7 = new Mode7(10);
			//testMode7.TextureFile = "F-Zero-KnightLeague-MuteCityI";
			//testMode7.LoadContent (content);

			//TODO: use this.Content to load your game content here 
			//arwing = new PixelModel(verticies,faces,true);
			//Console.Write(typeof(PixelModelDataReader).AssemblyQualifiedName);
			arwingData = Content.Load<PixelModelData>("arwing_color");
			//arwing = new PixelModel (arwingData);
			//raster.AddPolygons (arwing.GET);
			//raster.AddPolygons (arwing2.GET);
			position.X = 10.0f;
			position.Y = 0.0f;
			position.Z = 80.0f + ((float)Math.Sin ((float)t1 * 2.0f));
			fencer.position = new Vector3 (30, 30, 0);
			fencer.render = renderSprite;
			fencer.render = batchSprite;
			//fencer.origin = new Vector3 (36.0f, 36.0f,0);
			scale.X = 2.0f;
			scale.Y = 2.0f;
			scale.Z = 2.0f;
		//	arwing.SetLighting (3, false);
		//	arwing.SetLighting (37, false);
		//	arwing.SetLighting (24, false);
			//titania = new Background ();
			//titania.Filename = "62819";
			//titania.LoadContent (Content);
			floor = new FloorGrid ();
			floor.Enable = true;
			//arwing2.SetPosition (0.0f, 5.0f, 50.0f);
			//arwing2.SetRotation (0.0f, 5.0f, 700.0f);
			//arwing2.SetScale (3.0f, 3.0f, 3.0f);
			cam.Position = new Vector3(0, 10,0);
			//cam.Rotation = new Vector3(MathHelper.ToRadians(-2.0f), 0,0.0f);
		}
		float rotationSpeed = 1.0f;
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
				//if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
			if((  Keyboard.GetState ().IsKeyDown (Keys.Escape))) {

					Exit ();
				}
			//if(Keyboard.GetState ().IsKeyDown (Keys.A))
			//	raster.SetRenderMode(RasterType.WIREFRAME);
			//else
				raster.SetRenderMode(RasterType.FLAT_SHADED);
				//plotter.DrawLine(0,0,256,256,Color.Red);

				#endif
				cam.Update ();
				position.Z = 80.0f + ((float)Math.Sin ((float)t1) * 30.0f);
				t1 += 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
				rotation.Z = MathHelper.Pi + 1.0f * ((float)Math.Sin ((float)t1));
				//	position.Y -= 1.0f;
				rotation.Y -= 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
				//rotation.Y -= 0.2f*(float)gameTime.ElapsedGameTime.TotalSeconds;
				// TODO: Add your update logic here			
			//arwingGameObject.Update(gameTime);

			GameObject.GameUpdate (gameTime);
			//arwing.SetPosition (position.X, position.Y, position.Z);
			//arwing.SetRotation (rotation.X, rotation.Y, rotation.Z);
			//arwing.SetScale (scale.X, scale.Y, scale.Z);

			t += incr* (float)gameTime.ElapsedGameTime.TotalSeconds;;
			if (t > 1.0f || t < 0) {
				incr *= -1.0f;
			}


			KeyboardState keyboardState = Keyboard.GetState();
			//GamePadState currentState = GamePad.GetState( PlayerIndex.One );

			if (keyboardState.IsKeyDown( Keys.Left ) )
			{
				// Rotate left.
				cam.Rotation = cam.Rotation + new Vector3(0, rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds,0);
			}
			if (keyboardState.IsKeyDown( Keys.Right ) )
			{
				// Rotate right.
				cam.Rotation = cam.Rotation - new Vector3(0, rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds,0);
			}
			if (keyboardState.IsKeyDown( Keys.Up ) )
			{
				Matrix forwardMovement = Matrix.CreateRotationY( cam.Rotation.Y );
				Vector3 v = new Vector3( 0, 0, forwardSpeed );
				v = Vector3.Transform( v, forwardMovement );
				cam.Position = cam.Position + new Vector3(v.X* (float)gameTime.ElapsedGameTime.TotalSeconds,0,v.Z* (float)gameTime.ElapsedGameTime.TotalSeconds);//* (float)gameTime.ElapsedGameTime.TotalSeconds;
				//cam.Position.X += v.X;
			}
			if (keyboardState.IsKeyDown( Keys.Down ) )
			{
				Matrix forwardMovement = Matrix.CreateRotationY( cam.Rotation.Y );
				Vector3 v = new Vector3( 0, 0, -forwardSpeed );
				v = Vector3.Transform( v, forwardMovement );
				cam.Position = cam.Position + new Vector3 (v.X* (float)gameTime.ElapsedGameTime.TotalSeconds, 0, v.Z* (float)gameTime.ElapsedGameTime.TotalSeconds);//;
				//cam.Position.X += v.X;
			}
		
			if (keyboardState.IsKeyDown (Keys.Q)) {
				
				cam.Rotation = new Vector3 (cam.Rotation.X, cam.Rotation.Y, MathHelper.Lerp (cam.Rotation.Z, MathHelper.ToRadians (-10.0f), 0.05f));
			} else if (keyboardState.IsKeyDown (Keys.W)) {
				cam.Rotation = new Vector3 (cam.Rotation.X, cam.Rotation.Y, MathHelper.Lerp (cam.Rotation.Z, MathHelper.ToRadians (10.0f), 0.05f));

			} else {
				cam.Rotation = new Vector3 (cam.Rotation.X, cam.Rotation.Y, MathHelper.Lerp (cam.Rotation.Z, MathHelper.ToRadians (0.0f), 0.05f));

			}
				
			if (keyboardState.IsKeyDown (Keys.E)) {
				cam.Rotation = cam.Rotation + new Vector3 (-0.001f, 0, 0);
			} else if (keyboardState.IsKeyDown (Keys.D)) {
				cam.Rotation = cam.Rotation + new Vector3 (0.001f, 0, 0);
			}

			titania.A = (float)Math.Cos (cam.Rotation.Z);
			titania.B = -(float)Math.Sin (cam.Rotation.Z);
			titania.C = (float)Math.Sin (cam.Rotation.Z);
			titania.D = (float)Math.Cos (cam.Rotation.Z);
			float centerX = plotter.Width / 2.0f;
			float centerY = plotter.Height / 2.0f;

			float xImage = ((float)titania.Width * (1.0f-(Camera.MainCamera.Rotation.Y) / MathHelper.TwoPi));
			float yImage = 232.0f;
			titania.XOffset =xImage - ( centerX*(float)Math.Cos (cam.Rotation.Z) - centerY*(float)Math.Sin (cam.Rotation.Z));
			titania.YOffset =yImage - (centerX*(float)Math.Sin (cam.Rotation.Z) + centerY*(float)Math.Cos (cam.Rotation.Z));
			//titania.YOffset = 232.0f;
			/*
			testMode7.xOffset = cam.Position.X;
			testMode7.yOffset = cam.Position.Z;
			testMode7.A = 1.0f*(float)Math.Cos(cam.Rotation.Y);
			testMode7.B = -1.0f*(float)Math.Sin(cam.Rotation.Y);
			testMode7.C = 1.0f*(float)Math.Sin(cam.Rotation.Y);
			testMode7.D = 1.0f*(float)Math.Cos(cam.Rotation.Y);

*/
			fencer.Update (gameTime);
			Coroutines.Update ();
			//testMode7.startY = horizon;//cam.Height/2;
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
			spriteBatch.Begin (SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, Matrix.CreateScale ((float)graphics.GraphicsDevice.Viewport.Width/256.0f,(float)graphics.GraphicsDevice.Viewport.Height/224.0f,1.0f));
		
				//arwing.Draw (cam, plotter);
				//raster.Draw (gameTime);
		//	testMode7.Draw (mode7plot);

			//
			   RendererBase.Draw(gameTime);
			fencer.Draw();
		//	mode7plot.Draw (spriteBatch);
				spriteBatch.End ();
				base.Draw (gameTime);
			ProfileSampler.StopTimer ("Draw");
			
		}
			
	}
}

