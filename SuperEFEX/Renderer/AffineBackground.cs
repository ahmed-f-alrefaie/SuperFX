using System;
using SuperEFEX.Kernal;
using SuperEFEX.Core.Content.Graphics;
using SuperEFEX.Core.Content;
using Microsoft.Xna.Framework;
namespace SuperEFEX.Renderer
{
	public class AffineBackground : IRenderable
	{
		private string textureFile;
		bool enable = true;

		public bool Enable {
			get{ return enable; }
		}
			
		int mPriority = 0;

		public int Priority {
			get{ return mPriority; }
		}

		public RenderType RenderType {
			get{ return RenderType.BACKGROUND; }
		}

		public string TextureFile {
			get{ return textureFile; }
			set{ textureFile = value; }
		}



		private RawTexture texture;

		private float aMatrix=1.0f;
		private float bMatrix=0.0f;
		private float cMatrix=0.0f;
		private float dMatrix=1.0f;
		private float xOffset=0.0f;
		private float yOffset=0.0f;
		public int startY=0;
		public int endY=0;
		public Action<AffineBackground,int> hBlankFunc = (x,y)=>{};

		//Properties
		public float A {
			set{ aMatrix = value; }
		}

		public float B {
			set{ bMatrix = value; }
		}

		public float C {
			set{ cMatrix = value; }
		}

		public float D {
			set{ dMatrix = value; }
		}

		public float XOffset {
			set{ xOffset = value; }
		}
		public float YOffset {
			set{ yOffset = value; }
		}
		public virtual int Width {
			get{ return texture.Width; }
		}

		public virtual int Height {
			get{ return texture.Height; }
		}

		public AffineBackground (int priority)
		{
			mPriority = priority;
			Logger.Instance.Write ("Registering Affine Background\n");
			RendererBase.RegisterMeToRenderer (this);

		}

		public void LoadContent(FXContent content){
			texture = content.Load<RawTexture> (TextureFile);
		}

		//Ovverrite for tiles n shit
		public virtual Color GetColor(int x,int y){
			return texture.GetColor (x, y);
		}	
		
		public virtual void Draw(PixelPlotter plotter){
			int width = plotter.Width;
			int height = plotter.Height;
			startY = Math.Max (0, startY);

			for (int y = startY; y < height; y++) {
				for (int x = 0; x < width; x++) {
					float u = aMatrix * (x) + bMatrix * (y) + xOffset;
					float v = cMatrix * (x ) + dMatrix * (y) + yOffset;
					plotter.PlotPixel (x, y, GetColor ((int)u, (int)v));
				}
				hBlankFunc (this,y);

			}


		}


		}
		
}

