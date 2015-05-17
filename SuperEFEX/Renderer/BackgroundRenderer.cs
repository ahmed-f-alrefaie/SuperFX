using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace SuperEFEX.Renderer
{
	public class BackgroundRenderer : RendererBase
	{


		public override RenderType rendertype{get{ return RenderType.BACKGROUND; }}
		Background currentBackground;
		Rectangle viewRectangle,destRect;
		int mWidth,mHeight;
		int textureWidth;

		public BackgroundRenderer (SpriteBatch spriteBatch,int width,int height) : base(spriteBatch,10)
		{
			mWidth = width;
			mHeight = height;
			viewRectangle = new Rectangle (0, 0, mWidth*2, mHeight*2);
			destRect = new Rectangle (mWidth/2, mHeight/2 - 10, (int)(mWidth * 2), (int)(mHeight * 2));
			origin = new Vector2 (mWidth, (int)(mHeight));
		}

		Vector2 origin;
		float t = 0.0f;
		protected override void Render ()
		{
			t += 0.01f;
			viewRectangle.X = (int)((float)currentBackground.image.Width * (1.0f-(Camera.MainCamera.Rotation.Y) / MathHelper.TwoPi));
			spriteBatch.Draw (currentBackground.image,destRect , viewRectangle, Color.White,-Camera.MainCamera.Rotation.Z, origin, SpriteEffects.None, 0.0f);
			//spriteBatch.Draw (currentBackground.image, Vector2.Zero, new Rectangle (0, 0, mWidth, mHeight), viewRectangle, origin, 0.0f, Vector2.One, Color.White, SpriteEffects.None, 0);
		}

		protected override void RegisterObject (IRenderable Renderable)
		{
			currentBackground = (Background)Renderable;
		}
	}
}

