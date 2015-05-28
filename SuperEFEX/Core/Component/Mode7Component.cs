﻿using System;
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
		const int M7_FAR_BG = 3000;
		const int M7_LEFT=-120;
		const int M7_RIGHT=120;
		const int M7_TOP=80;
		const int M7_BOTTOM=-80;
		const int M7_NEAR=24;
		const int M7_FAR=3000;
		const int m7D = 400;
		BackgroundComponent bgc;

		public void m7_HBLANK(AffineBackground m7,int y){

			float xc = cam.Position.X;
			float yc = cam.Position.Y;
			float zc = cam.Position.Z;
			float yb = (y - M7_TOP) * cam.Up.Y + m7D * cam.Forward.Y;;
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
		}


		public Mode7Component ()
		{
		}


		public override void Initialize ()
		{

			bgc = owner.GetComponent<BackgroundComponent> ();

			base.Initialize ();
		}

		public override void FixedUpdate (GameTime gameTime)
		{
			cam = Camera.MainCamera;
			//Do other computes
			if(cam.Up.Y != 0.0f){
				bgc.StartY = M7_TOP -(int)((((float)M7_FAR_BG*cam.Forward.Y-cam.Position.Y)*m7D)/((float)M7_FAR_BG*cam.Up.Y)) +1;
			}

			bgc.StartY = MathHelper.Clamp (bgc.startY, 0, cam.Height);
			base.Update (gameTime);
		}
	}
}

