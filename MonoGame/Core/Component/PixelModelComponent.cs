using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Renderer.ModelContent;
using Renderer;
using SuperFXContent;
namespace Core.Components
{
	[Serializable]
	public class PixelModelComponent : Component
	{
		private PixelModel model;

		private string filename;

		public string Filename {
			get {
				return filename;
			}
			set{
				filename = value;
			}
		}

		public PixelModel Model {
			get {
				return model;
			}
		}


		public PixelModelComponent ()
		{
		}

		public override void LoadContent (ContentManager content)
		{

			model = new PixelModel (content.Load<PixelModelData> (filename));
			base.LoadContent (content);
		}

		public override void Update (GameTime gameTime)
		{

			model.Position = owner.transform.Position;
			model.Rotation = owner.transform.Quaternion;
			model.Scale = owner.transform.Scale;

			base.Update (gameTime);
		}

		public void SetLighting(int facenumber,bool doLighting){
			model.SetLighting (facenumber, doLighting);
		}
		public void SetColor(int facenumber,Color color){
			model.SetColor (facenumber, color);
		}


	}
}

