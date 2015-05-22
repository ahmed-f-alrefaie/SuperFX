using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
namespace SuperEFEX.Renderer
{
	public class BackgroundRenderer : RendererBase
	{


		public override RenderType rendertype{get{ return RenderType.BACKGROUND; }}
		List<AffineBackground> backgrounds = new List<AffineBackground>();
		int mWidth,mHeight;
		int textureWidth;
		PixelPlotter plotter;

		public BackgroundRenderer (SpriteBatch spriteBatch,PixelPlotter plotter,int width,int height) : base(spriteBatch,10)
		{
			mWidth = width;
			mHeight = height;
			this.plotter = plotter;
		}
	
		protected override void Render ()
		{

			foreach (AffineBackground b in backgrounds) {
				if (b.Enable) {
					b.Draw (plotter);
				}
			}

			//if (currentBackground == null)
			//	return;
			//t += 0.01f;
			//viewRectangle.X = (int)((float)currentBackground.image.Width * (1.0f-(Camera.MainCamera.Rotation.Y) / MathHelper.TwoPi));
			//spriteBatch.Draw (currentBackground.image,destRect , viewRectangle, Color.White,-Camera.MainCamera.Rotation.Z, origin, SpriteEffects.None, 0.0f);
			//spriteBatch.Draw (currentBackground.image, Vector2.Zero, new Rectangle (0, 0, mWidth, mHeight), viewRectangle, origin, 0.0f, Vector2.One, Color.White, SpriteEffects.None, 0);
		}

		protected override void RegisterObject (IRenderable Renderable)
		{
			
			backgrounds.Add ((AffineBackground)Renderable);
			backgrounds.Sort ((x, y) => {
				return x.Priority.CompareTo (y.Priority);
			});

			//currentBackground = (Background)Renderable;
		}
	}
}

