using System;
using Microsoft.Xna.Framework;
namespace SuperEFEX.Renderer
{
	public class WackyBackground : AffineBackground
	{

		protected void WackyHBL(AffineBackground back, int y){

			XOffset = 100.0f * (float)Math.Sin ((double)MathHelper.ToRadians((float)y*2.0f + timer));





		}


		public WackyBackground () : base(2)
		{
			hBlankFunc = WackyHBL;
		}

		float timer = 0.0f;


		public override void Draw (PixelPlotter plotter)
		{

			timer += 10.0f;

			base.Draw (plotter);
		}



	}
}

