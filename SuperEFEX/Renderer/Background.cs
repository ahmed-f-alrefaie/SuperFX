using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace SuperEFEX.Renderer
{
	public class Background //: IRenderable
	{


		public bool Enable{get{return true;}}
		public RenderType RenderType{ get{return RenderType.BACKGROUND;}}

		public string Filename;
		public Texture2D image;



		public Background ()
		{
			//RendererBase.RegisterMeToRenderer (this);
		}


			
		public void LoadContent(ContentManager content){

			image = content.Load<Texture2D> (Filename);
		}


	}
}

