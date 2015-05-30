using System;
using SuperFXContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace SuperEFEX.Renderer
{
	class SpriteFontDataReader : ContentTypeReader<SpriteFontData>
	{
		/// <summary>
		/// Loads an imported shader.
		/// </summary>
		protected override SpriteFontData Read(ContentReader input,
			SpriteFontData existingInstance)
		{

			SpriteFontData sprite = new SpriteFontData();
			//Random r = new Random();
			//Read sprite data
			sprite.textureFile = input.ReadString();
			sprite.Width = input.ReadInt32 ();
			sprite.Height = input.ReadInt32 ();
			int fontCount = input.ReadInt32 ();
			for (int i = 0; i < fontCount; i++) {
				char key = input.ReadChar ();
				Rectangle textureCoords = new Rectangle ();
				textureCoords.X = input.ReadInt32 ();
				textureCoords.Y = input.ReadInt32 ();
				textureCoords.Width = input.ReadInt32 ();
				textureCoords.Height = input.ReadInt32 ();
				sprite.font.Add (key,textureCoords);

			}


			return sprite;
		}
	}
}

