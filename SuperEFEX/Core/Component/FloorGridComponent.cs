using System;
using SuperEFEX.Renderer.ModelContent;
using SuperEFEX.Core.Content;
using Microsoft.Xna.Framework;
namespace SuperEFEX.Core.Components
{
	public class FloorGridComponent : Component
	{
		private FloorGrid model;


		public FloorGridComponent ()
		{
		}

		public override void LoadContent (FXContent content)
		{

			model = new FloorGrid ();
			base.LoadContent (content);
		}

		public override void Update (GameTime gameTime)
		{
			model.Enable = Enabled;
			//model.Position = owner.transform.Position;
			//model.Rotation = owner.transform.Quaternion;
			//model.Scale = owner.transform.Scale;
			model.WorldMatrix = owner.transform.LocalToWorldMatrix;
			base.Update (gameTime);
		}
	}
}

