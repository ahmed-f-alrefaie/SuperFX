using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Renderer.ModelContent;
namespace Core.Components
{
	public class PixelModelComponent : Component
	{
		PixelModel model;

		private string filename;

		public string Filename {
			get {
				return filename;
			}
			set{
				filename = value;
			}
		}



		public PixelModelComponent ()
		{
		}

		public override void LoadContent ()
		{


			base.LoadContent ();
		}
	}
}

