using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperEFEX.Core.Content.Graphics;
namespace SuperEFEX.Core.Content
{
	//Replace the current content manager
	public class FXContent
	{
		//Create a content manager
		private ContentManager content;
		Dictionary<string,RawTexture> mRawTexture = new Dictionary<string, Core.Content.Graphics.RawTexture>();



		public FXContent (Game game)
		{
			content = new ContentManager (game.Services,game.Content.RootDirectory);
		}

		public T Load<T>(string name){
			if (typeof(T) != typeof(RawTexture)) {
				return content.Load<T> (name);
			} else { //Handle the raw texture format
				if (mRawTexture.ContainsKey (name)) {
					return (T)Convert.ChangeType (mRawTexture [name], typeof(T));

				} else {

					mRawTexture [name] = new RawTexture(content.Load<Texture2D> (name));
					return (T)Convert.ChangeType(mRawTexture[name],typeof(T));
				}



			}



		}
		public void Unload(){
			content.Unload ();
			mRawTexture.Clear ();


		}


	}
}

