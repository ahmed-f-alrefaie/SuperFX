using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Kernal;
namespace Renderer
{
	public abstract class RendererBase
	{
		
		public abstract RenderType rendertype{get;}
		private int mRenderOrder;
		protected SpriteBatch spriteBatch;
		static private List<RendererBase> renderList = new List<RendererBase>();

		public RendererBase (SpriteBatch spriteBatch,int renderOrder)
		{
			this.spriteBatch = spriteBatch;
			mRenderOrder = renderOrder;
			renderList.Add (this); //Add the renderer to the list
			renderList.Sort((x,y)=> x.mRenderOrder.CompareTo(y.mRenderOrder));
			//Higher numbers have lower priority

		}

		protected abstract void Render ();

		public static void RegisterMeToRenderer(IRenderable Renderable){
			foreach (RendererBase r in renderList) {
				Logger.Instance.Write (Renderable.RenderType.ToString () + " + " + r.rendertype.ToString ());
				if (r.rendertype == Renderable.RenderType) {
					r.RegisterObject (Renderable);
					break;
				}
			}
		}

		protected abstract void RegisterObject (IRenderable Renderable);

		static public void Draw(GameTime gameTime){

			foreach (RendererBase r in renderList)
				r.Render ();


		}

	}
}

