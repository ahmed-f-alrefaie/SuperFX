using System;
using Microsoft.Xna.Framework;
using SuperEFEX.Core.Content;
using SuperEFEX.Core.Components;
using SuperEFEX.Renderer;
namespace SuperEFEX.Core.Components
{
	public class Mode7Component : Component
	{

		Camera cam;
		//Turn these into constants
		const int M7_FAR_BG = 50;
		const int M7_LEFT=-120;
		const int M7_RIGHT=120;
		const int M7_TOP=80;
		const int M7_BOTTOM=-80;
		const int M7_NEAR=24;
		const int M7_FAR=100;
		const int m7D = 80;

		const float fallOff = (2.0f);

		BackgroundComponent bgc;

		public void m7_HBLANK(AffineBackground m7,int y){

			float xc = cam.Position.X;
			float yc = cam.Position.Y;
			float zc = cam.Position.Z;
			float yb = (y - M7_TOP) * cam.Up.Y + m7D * cam.Forward.Y;
			float startY = M7_TOP -(int)((((float)M7_FAR_BG*cam.Forward.Y-cam.Position.Y)*m7D)/((float)M7_FAR_BG*cam.Up.Y));
			float lam = yc / yb;
			float lcf = lam * cam.Left.X;
			float lsf = lam * cam.Left.Z;
			float bga = lcf;
			float bgc = lsf;
			float zb = (y - M7_TOP) * cam.Forward.Y - m7D*cam.Up.Y;
			m7.A = bga;
			m7.B = -lsf;
			m7.C = lsf;
			m7.D = lcf;
			m7.XOffset = xc + lcf * M7_LEFT - lsf * zb;
			m7.YOffset = zc + lsf * M7_LEFT + lcf * zb;
			m7.colorMulti = MathHelper.Clamp ((float)(y - startY)*0.1f, 0.0f, 1.0f);
		}


		public Mode7Component ()
		{
		}


		public override void LoadContent (FXContent content)
		{
			base.LoadContent (content);

			bgc = owner.GetComponent<BackgroundComponent> ();
			bgc.SetHBLank (m7_HBLANK);
			base.Initialize ();
		}

		public override void FixedUpdate (GameTime gameTime)
		{
			cam = Camera.MainCamera;
			int startY=0;
			//Do other computes
			if(cam.Up.Y != 0.0f){
				startY = M7_TOP -(int)((((float)M7_FAR_BG*cam.Forward.Y-cam.Position.Y)*m7D)/((float)M7_FAR_BG*cam.Up.Y));
			}

			bgc.StartY = MathHelper.Clamp (startY, 0, cam.Height);
			base.Update (gameTime);
		}
	}
}

