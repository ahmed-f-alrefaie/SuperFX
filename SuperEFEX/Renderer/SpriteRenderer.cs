using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
namespace SuperEFEX.Renderer
{
	public interface ISpriteRenderer
	{
		void DrawSprite (Sprite sprite);

	}


	public class SpriteRenderer : RendererBase{

		public override RenderType rendertype{get{ return RenderType.SPRITE; }}
		List<Sprite> sprites = new List<Sprite>();
		List<SFXSpriteFont> spritefonts = new List<SFXSpriteFont>();
		public SpriteRenderer (SpriteBatch spriteBatch,PixelPlotter plotter,int width,int height) : base(spriteBatch,100)
		{
			//this.plotter = plotter;
			this.spriteBatch = spriteBatch;
		
		}

		protected override void Render ()
		{

			foreach (Sprite b in sprites) {
				if (b.Enable) {
					b.Draw ();
				}
			}
			foreach (SFXSpriteFont b in spritefonts) {
				if (b.Enable) {
					b.Draw ();
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
			if (Renderable is Sprite) {
				sprites.Add ((Sprite)Renderable);

				((BatchSpriteRenderer)((Sprite)Renderable).render).SpriteBatch = spriteBatch;
				sprites.Sort ((x, y) => {
					return x.Priority.CompareTo (y.Priority);
				});
			} else if (Renderable is SFXSpriteFont) {

				((SFXSpriteFont)Renderable).batch = spriteBatch;
				spritefonts.Add ((SFXSpriteFont)Renderable);


			}

			//currentBackground = (Background)Renderable;
		}
	}



}

