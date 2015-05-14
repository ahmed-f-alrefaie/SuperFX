using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
namespace Core.Content
{
	public class FXContent
	{
		//Create a content manager
		private ContentManager content;




		public FXContent (Game game)
		{
			content = new ContentManager (game.Services);
		}
	}
}

