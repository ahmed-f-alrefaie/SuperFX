using System;
using SuperEFEX.Renderer;
using Microsoft.Xna.Framework;
using SuperEFEX.Core.Content;
namespace SuperEFEX.Core.Components
{
	public class SpriteFontComponent : Component
	{

		SFXSpriteFont sprite;

		public string SpriteName{ get; set; }
		public string DefaultText{ get; set; }
		public float Alpha{ get; set; }
		public SpriteFontComponent ()
		{
		}

		public override void LoadContent (FXContent content)
		{

			sprite = new SFXSpriteFont ();
			sprite.Filename = SpriteName;
			sprite.LoadContent (content);
			sprite.Alpha = Alpha;
			sprite.SetString (DefaultText, SpriteJustify.LEFT);
			base.LoadContent (content);
		}

		public override void Update (GameTime gameTime)
		{
			sprite.Enable = this.Enabled;
			sprite.Alpha = Alpha;
			sprite.position = owner.transform.Position;
			base.Update (gameTime);
		}

		public void SetText(string text,SpriteJustify just){
			sprite.SetString (text, just);
		}


	}
}

