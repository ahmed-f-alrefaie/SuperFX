using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SuperEFEX.Core.Content.Graphics
{

	public class RawTexture
	{
		private Texture2D mTexture;
		private Color[] mTextureMap;
		private int mWidth;
		private int mHeight;

		//U sampleing method
		private SamplerState mUSample = SamplerState.PointWrap;
		//V Sampling method
		private SamplerState mVSample = SamplerState.PointWrap;

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

		public Texture2D OriginalTexture {
			get{ return mTexture; }
		}
		public RawTexture (Texture2D texture)
		{
			mTexture = texture;
			mWidth = texture.Width;
			mHeight = texture.Height;
			mTextureMap = new Color[mWidth * mHeight];
			texture.GetData<Color> (mTextureMap);
			//Store color data
		}

		public Color GetColor(int u,int v){
			//if (u > Width || u < 0 || v > Width || v < 0)
			//	return Color.Transparent;
			if (mUSample == SamplerState.PointWrap) {
				while (u >= mWidth)
					u -= mWidth;
				while (u < 0)
					u += mWidth;
			} else if (mUSample == SamplerState.PointClamp) {
				u = MathHelper.Clamp (u, 0, mWidth - 1);
			} else {
				throw new ArgumentException ();
			}
			if (mVSample == SamplerState.PointWrap) {
				while (v >= mHeight)
					v -= mHeight;
				while (v < 0)
					v += mHeight;
			} else if (mVSample == SamplerState.PointClamp) {
				v = MathHelper.Clamp (v, 0, mHeight - 1);
			} else {
				throw new ArgumentException();
			}
				
			return mTextureMap [u + v * mWidth];


		}

		public Color GetColor(float u, float v){
			return GetColor ((int)(u * mWidth), (int)(v * mHeight));
		}


	}
}

