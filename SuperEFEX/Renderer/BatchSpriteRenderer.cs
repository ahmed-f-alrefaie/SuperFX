using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SuperEFEX.Renderer
{
	public class BatchSpriteRenderer : ISpriteRenderer
	{

		public SpriteBatch SpriteBatch;

		public BatchSpriteRenderer ()
		{
		}


		public void DrawSprite(Sprite sprite){

			SpriteBatch.Draw (sprite.Texture.OriginalTexture, new Vector2 (sprite.position.X, sprite.position.Y), sprite.texturepoint, Color.White, 0.0f,
				new Vector2(sprite.origin.X,sprite.origin.Y), 1.0f, SpriteEffects.None, sprite.position.Z);

		}

	}
}

