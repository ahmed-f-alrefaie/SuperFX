using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperEFEX.Kernal;
using SuperEFEX.Core.Content;
using SuperEFEX.Core.Content.Graphics;
namespace SuperEFEX.Renderer
{
	public class PixelPlotter
	{
		private int mWidth;
		private int mHeight;
		private int mDepth;
		private Vector2 pos;
		private Texture2D mPixel;
		private Texture2D screen;

		int[] BayerMatrix = {1,3,
			4,2

			};




		public int Width {
			get {
				return mWidth;
			}
		}

		public int Height {
			get {
				return mHeight;
			}
		}

		private GraphicsDevice mDevice;

		private Color[] mFrameBuffer;
		private float[] mDepthBuffer;
		//private Color[] mDitheredBuffer;
		private Color mClearColor;

		public PixelPlotter (GraphicsDevice device,int width,int height)
		{
			mDevice = device;
			mWidth = width;
			mHeight = height;
			Color[] color = new Color[1];
			color [0] = Color.White;
			//mPixel = new Texture2D (device,1, 1);
		
			screen = new Texture2D (device,width, height);
		
			//mPixel.SetData (color);

			mFrameBuffer = new Color[width * height];
			mDepthBuffer = new float[width * height];
	//		mDitheredBuffer = new Color[width * height];
			mClearColor = Color.Blue;

		}




		//Plot into frame buffer
		public void PlotPixel(int x, int y, Color color){
			if (x < mWidth && x >= 0 && y < mHeight && y >= 0)
				mFrameBuffer [x + y * mWidth] = color;//downsample_color(color);
		}

		private void AddColors(ref Color c1, ref Color c2){

			c1.R = (byte)MathHelper.Min ((int)c1.R + (int)c2.R, 255);
			c1.G = (byte)MathHelper.Min ((int)c1.G + (int)c2.G, 255);
			c1.B = (byte)MathHelper.Min ((int)c1.B + (int)c2.B, 255);
			c1.A = (byte)MathHelper.Min ((int)c1.A + (int)c2.A, 255);
		}

		private void AlphaBlend(ref Color c2, ref Color c1){
			Vector4 col1 = c1.ToVector4 ();
			Vector4 col2 = c2.ToVector4 ();
			Vector4 outColor = Vector4.Zero;
			outColor.W = col2.W + col1.W * (1.0f - col2.W);
			outColor.X = (col2.X*col2.W + col1.X*col1.W*(1.0f-col2.W))/outColor.W;
			outColor.Y = (col2.Y*col2.W + col1.Y*col1.W*(1.0f-col2.W))/outColor.W;
			outColor.Z = (col2.Z*col2.W + col1.Z*col1.W*(1.0f-col2.W))/outColor.W;
			c2 =  new Color (outColor);
		}

		//Plot into frame buffer
		public void PlotPixelDepth(int x, int y, Color color,float depth){
			if (x < mWidth && x >= 0 && y < mHeight && y >= 0) {
				//mDepthBuffer [x + y * mWidth] = depth;

				if (mDepthBuffer [x + y * mWidth] > depth && 0.1f < depth) {
					PlotPixel (x, y, color);
					mDepthBuffer [x + y * mWidth] = depth;
				} else if (mFrameBuffer [x + y * mWidth].A != 255) {
					AddColors (ref mFrameBuffer [x + y * mWidth], ref color);


				}
					

			}
		}

		public void SetClearColor(Color color)
		{
			mClearColor = color;
			mClearColor.A = 0;
		}

		public void Clear(Color color){
			for (int h = 0; h < mHeight; h++)
				for (int w = 0; w < mWidth; w++)
					mFrameBuffer [w + h*mWidth] = color;
		}

		public void ClearDepth(){
			for (int h = 0; h < mHeight; h++)
				for (int w = 0; w < mWidth; w++)
					mDepthBuffer [w + h*mWidth] = 1.0f;
		}

		private  Color downsample_color(Color pColor){

			int R = (int)pColor.R / 8;
			int G = (int)pColor.G / 4;
			int B = (int)pColor.B / 8;
			R *= 8;
			G *= 4;
			B *= 8;


			return new Color (R, G, B);


		}

		private void AddColor(Color color1, Color color2,float scalar,ref Color color3){

			color3.R = (byte)((int)color2.R + (int)((float)color1.R*scalar));
			color3.G = (byte)((int)color2.G + (int)((float)color1.G*scalar));
			color3.B = (byte)((int)color2.B + (int)((float)color1.B*scalar));




		}

		private Color subtract_color(Color color1, Color color2){

			Color color3 = new Color();

			color3.R = (byte)(((int)color2.R - (int)color1.R));
			color3.G = (byte)(((int)color2.G - (int)color1.G));
			color3.B = (byte)(((int)color2.B - (int)color1.B));

			return color3;

		}

		public void Draw(SpriteBatch spriteBatch){
			
			ProfileSampler.StartTimer ("PixelPlotterDraw");

				//for (int h = 0; h < mHeight; h++) {
				//	for (int w = 0; w < mWidth; w++) {
				//		mFrameBuffer [w + h * mWidth]=new Color(mDepthBuffer[w + h * mWidth],mDepthBuffer[w + h * mWidth],mDepthBuffer[w + h * mWidth]);
				//	}
				//}
				screen.SetData<Color> (mFrameBuffer);
				spriteBatch.Draw (screen, Vector2.Zero);
				/*
			for (int h = 0; h < mHeight; h++) {
				for (int w = 0; w < mWidth; w++) {
					pos.X = w;
					pos.Y = h;
					spriteBatch.Draw (mPixel, pos, mFrameBuffer [w, h]);
				}
			}
			*/



				Clear (mClearColor);
				ClearDepth ();
			ProfileSampler.StopTimer ("PixelPlotterDraw");

		}

		private void swap(ref int x1,ref int x2){
			int temp = x1;
			x1 = x2;
			x2 = temp;
		}

		public void DrawScanLineDepth(int x1, int x2, int y,float d1,float d2,Color color){
			x1 = MathHelper.Clamp (x1, 0, mWidth);
			x2 = MathHelper.Clamp (x2, 0, mWidth);
			float depth = 0.0f;
			float dz_dx = (d2 - d1) / (float)(x2 - x1);
			int minX = Math.Min (x1, x2);
			int maxX = Math.Max (x1, x2);

			for(int x=minX; x<maxX; x++)
			{

				depth = (dz_dx * (float)(x-x1) + d1);
				PlotPixelDepth (x, y, color, depth);
			}
		}


		public void DrawTexturedScanLineDepth(int x1, int x2, int y,float d1,float d2,Point tex1,Point tex2,RawTexture texture){
			//x1 = MathHelper.Clamp (x1, 0, mWidth);
			//x2 = MathHelper.Clamp (x2, 0, mWidth);
			float depth = 0.0f;
			float dz_dx = (d2 - d1) / (float)(x2 - x1);

			float du_dx = (float)(tex2.X - tex1.X) / (float)(x2 - x1);
			float dv_dx = (float)(tex2.Y - tex1.Y) / (float)(x2 - x1);
			float u = (float)tex1.X;
			float v = (float)tex1.Y; //(float)(tex2.Y - tex1.Y) / (float)(x2 - x1);
			int minX = Math.Min (x1, x2);
			int maxX = Math.Max (x1, x2);

			for(int x=minX; x<maxX; x++)
			{

				depth = (dz_dx * (float)(x-x1) + d1);
			//	u = du_dx * (float)(x - x1) + (float)tex1.X; 
			//	v = dv_dx * (float)(x - x1) + (float)tex1.Y; 
				PlotPixelDepth (x, y, texture.GetColor((int)u,(int)v), depth);
				u += du_dx;
				v += dv_dx;

			}
		}
	

		public void DrawLineDepth(int x1,int y1, int x2, int y2,float d1,float d2,Color color){
			x1 = MathHelper.Clamp (x1, 0, mWidth);
			x2 = MathHelper.Clamp (x2, 0, mWidth);
			y1 = MathHelper.Clamp (y1, 0, mHeight);
			y2 = MathHelper.Clamp (y2, 0, mHeight);
			// Bresenham's line algorithm
			bool steep = (Math.Abs(y2 - y1) > Math.Abs(x2 - x1));
			if(steep)
			{
				swap(ref x1,ref y1);
				swap(ref x2,ref y2);
			}

			if(x1 > x2)
			{
				swap(ref x1,ref x2);
				swap(ref y1,ref y2);
			}

			float dx = x2 - x1;
			float dy = Math.Abs(y2 - y1);
			float depth = 0.0f;

	

			float dz_dx = (d2 - d1) / (float)(x2 - x1);
			float error = dx / 2.0f;
			int ystep = (y1 < y2) ? 1 : -1;
			int y = (int)y1;

			int maxX = (int)x2;

			for(int x=(int)x1; x<maxX; x++)
			{

					depth = (dz_dx * (float)(x-x1) + d1);
					if (steep) {
					
						PlotPixelDepth (y, x, color, 1.0f/depth);
					} else {
						PlotPixelDepth (x, y, color, 1.0f/depth);
					}


				error -= dy;
				if(error < 0)
				{
					y += ystep;
					error += dx;
				}
			}
		}



		public void DrawLine(int x1,int y1, int x2, int y2,Color color){
				// Bresenham's line algorithm
				bool steep = (Math.Abs(y2 - y1) > Math.Abs(x2 - x1));
				if(steep)
				{
					swap(ref x1,ref y1);
					swap(ref x2,ref y2);
				}

				if(x1 > x2)
				{
					swap(ref x1,ref x2);
					swap(ref y1,ref y2);
				}

				float dx = x2 - x1;
				float dy = Math.Abs(y2 - y1);

				float error = dx / 2.0f;
				int ystep = (y1 < y2) ? 1 : -1;
				int y = (int)y1;

				int maxX = (int)x2;

				for(int x=(int)x1; x<maxX; x++)
				{
					if(steep)
					{
						PlotPixel(y,x, color);
					}
					else
					{
						PlotPixel(x,y, color);
					}

					error -= dy;
					if(error < 0)
					{
						y += ystep;
						error += dx;
					}
				}
		}


	}
}

