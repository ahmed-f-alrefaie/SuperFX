using System;
using SuperEFEX.Renderer;
using Microsoft.Xna.Framework;
using SuperEFEX.Core.Content;
namespace SuperEFEX.Core.Components
{
	public class SpriteComponent : Component
	{

		Sprite sprite;

		public string SpriteName{ get; set; }
		public float FrameSpeed{ get; set; }
		public string DefaultAnimation{ get; set; }
		public SpriteComponent ()
		{
		}

		public override void LoadContent (FXContent content)
		{

			sprite = new Sprite ();
			sprite.Filename = SpriteName;
			sprite.LoadContent (content);
			sprite.PlayAnimation (DefaultAnimation, 0);
			base.LoadContent (content);
		}

		public override void Update (GameTime gameTime)
		{
			sprite.Enable = this.Enabled;
			sprite.position = owner.transform.Position;
			sprite.scale.X = owner.transform.Scale.X;
			sprite.scale.Y = owner.transform.Scale.Y;
			sprite.FrameMultiplier = FrameSpeed;
			sprite.Update (gameTime);
			base.Update (gameTime);
		}

		public void PlayAnimation(string name,int loops){
			sprite.PlayAnimation (name, loops);
		}

		public void SetAlpha(float alpha){
			sprite.SetAlpha (alpha);
		}


	}
}

