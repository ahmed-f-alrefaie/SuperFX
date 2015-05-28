using System;
using System.Collections.Generic;
using SuperEFEX.Core;
using SuperEFEX.Kernal;
using SuperEFEX.Core.Content;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using SuperEFEX.Renderer;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SuperEFEX.Core.Components;
using SuperEFEX.Kernal.Utilities;
namespace SuperEFEX.GameSystem
{
	public class GameScene
	{

		public static List<string> mGameScenes = new List<string>();

		//BAckground renderer
		private List<string> mGameObjectList = new List<string>();
		private FXContent Content;
		public static GameScene CurrentScene = null;
		private string path;
		private Game game;
		private GraphicsDevice graphics;
		private Camera camera;
		//This is to Deserialize GameObjects
		private Type[] componentTypes;
		public string Name{ get; set; }



		public static GameScene CreateGameScene(string Filename){
			StreamReader sr = new StreamReader (Filename);
			XmlSerializer xmlsr = new XmlSerializer (typeof(GameScene));
			GameScene scene = (GameScene)xmlsr.Deserialize (sr);
			scene.path = Path.GetDirectoryName (Filename);
			scene.Begin ();
			return scene;


		}

		public List<string> GameObjects {
			get{ return mGameObjectList; }
			set{ mGameObjectList = value; }
		}

		PixelPlotter plotter;
		BackgroundRenderer bkg;
		Rasterizer3D rasterizer;

		SpriteBatch spriteBatch;


		public GameScene(){
			if (GameScene.CurrentScene != null) {
				Logger.Instance.Write ("GameScene Already Exists!!\n");
				throw new ArgumentException ("Scene already exists!!");


			}

			GameScene.CurrentScene = this;

			componentTypes = Utilities.FindAllDerivedTypes<Component> ().ToArray();
		}

		public void Begin(){

			XmlSerializer xml = new XmlSerializer(typeof(GameObject),componentTypes);

			//Go through and deciralize all GameObjects
			foreach (string go in mGameObjectList) {
				//BEgin deserializing
				StreamReader sr = new StreamReader (path+"/"+go);
				GameObject gameObj = xml.Deserialize (sr) as GameObject;
				sr.Close();
				gameObj.Start ();

			}



		}

		public void Initialize(){
			GameObject.InitializeGameObject ();
		}

		public void LoadContent(GraphicsDevice gD){

			graphics = gD;
			this.game = game;
			Content = new FXContent (game);
			//Setup rendering
			spriteBatch = new SpriteBatch (graphics);
			plotter = new PixelPlotter (graphics, 256, 244);
			plotter.SetClearColor (Color.TransparentBlack);
			rasterizer = new Rasterizer3D (spriteBatch, plotter);
			bkg = new BackgroundRenderer (spriteBatch, plotter, 256, 244);
			GameObject.LoadGameContent (Content);


		}

		public void Setup(Game game,GraphicsDevice graphics){
			this.game = game;
			camera = new Camera (256, 244);
			camera.Update ();
		}


		public void Destroy(){
			Content.Unload (); // Unload all the content

			//Destroy game contents here
			Coroutines.StopAll();


		}

		public void Update(GameTime gameTime){
			GameObject.GameUpdate (gameTime);
			//Do other things here like Renderer/Collision/Physics/Coroutines
			Coroutines.Update();
		}

		public void Draw(GameTime gameTime){

			spriteBatch.Begin (SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.CreateScale ((float)game.GraphicsDevice.Viewport.Width / 256.0f, (float)game.GraphicsDevice.Viewport.Height / 224.0f, 1.0f));
			RendererBase.Draw (gameTime);
			spriteBatch.End ();
		}





	}
}

