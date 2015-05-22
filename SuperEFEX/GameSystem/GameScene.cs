using System;
using System.Collections.Generic;
using SuperEFEX.Core;
using SuperEFEX.Kernal;
using SuperEFEX.Core.Content;
using SuperEFEX.Kernal;
using Microsoft.Xna.Framework;
namespace SuperEFEX.GameSystem
{
	public class GameScene
	{

		public static List<string> mGameScenes = new List<string>();

		//BAckground renderer
		private List<string> mGameObjectList = new List<string>();
		private FXContent Content;
		public static GameScene CurrentScene = null;

		//This is to Deserialize GameObjects
		public List<string> GameObjects {
			get{ return mGameObjectList; }
			set{ mGameObjectList = value; }
		}

		public GameScene(){
			if (GameScene.CurrentScene != null) {
				Logger.Instance.Write ("GameScene Already Exists!!\n");
				throw new ArgumentException ("Scene already exists!!");


			}

			GameScene.CurrentScene = this;


		}

		public void LoadContent(){

			GameObject.LoadGameContent (Content);


		}

		public static void SetUpContent(Game game){
			GameScene.CurrentScene.Content = new FXContent (game);
		}


		public void Destroy(){
			Content.Unload (); // Unload all the content
			//Destroy game contents here
			Coroutines.StopAll();


		}

		public void Update(GameTime gameTime){
			GameObject.GameUpdate (gameTime);
			//Do other things here like Renderer/Collision/Physics/Coroutines
		}


	}
}

