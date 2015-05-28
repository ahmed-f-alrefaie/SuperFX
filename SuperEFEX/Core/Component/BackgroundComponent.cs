using System;
using Microsoft.Xna.Framework;
using SuperEFEX.Core.Content;
using SuperEFEX.Core.Components;
using SuperEFEX.Renderer;
namespace SuperEFEX.Core.Components
{
	public class BackgroundComponent : Component
	{

		AffineBackground background;

		public string Filename{ get; set; }
		public int Priority{ get; set; }
		public BackgroundComponent ()
		{
			
		}

		public float A {
			set{ background.A = value; }
		}
		public float B {
			set{ background.B = value; }
		}
		public float C {
			set{ background.C = value; }
		}
		public float D {
			set{ background.D = value; }
		}
		public float XOffset {
			set{ background.XOffset = value; }
		}
		public float YOffset {
			set{ background.YOffset = value; }
		}

		public int StartY {
			set{ background.startY = value; }
		}

		public override void LoadContent (FXContent content)
		{
			background = new AffineBackground (Priority);
			background.TextureFile = Filename;
			background.LoadContent(content);
		
			base.LoadContent (content);
		}

		public void SetMatrices(float A, float B, float C, float D){
			background.A = A;
			background.B = B;
			background.C = C;
			background.D = D;


		}

		public void SetHBLank(Action<AffineBackground,int> hblank){
			background.hBlankFunc = hblank;
		}

	}
}

