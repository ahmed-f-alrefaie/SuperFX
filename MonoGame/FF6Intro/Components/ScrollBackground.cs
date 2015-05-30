using System;
using Microsoft.Xna.Framework;
using SuperEFEX.Core.Content;
using SuperEFEX.Core.Components;
using SuperEFEX.Renderer;

namespace MonoGame.FF6Intro.Components
{
	public class ScrollBackground : Component
	{

		float Y = 0;
		BackgroundComponent  bgc;
		public float ScrollSpeed{ get; set; }
		public int YOffset{ get; set; }
		public ScrollBackground ()
		{
		}
			


		public override void LoadContent (FXContent content)
		{


			bgc = owner.GetComponent<BackgroundComponent> ();
			base.LoadContent (content);
		}

		public override void Update (GameTime gameTime)
		{

			Y += (float)gameTime.ElapsedGameTime.TotalSeconds*ScrollSpeed;

			bgc.YOffset = Y + YOffset;


			base.Update (gameTime);
		}
	}
}

