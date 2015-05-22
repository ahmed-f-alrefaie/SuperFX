using System;
using SuperFXContent;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace SuperEFEX.Renderer
{
	class SpriteDataReader : ContentTypeReader<SpriteData>
	{
		/// <summary>
		/// Loads an imported shader.
		/// </summary>
		protected override SpriteData Read(ContentReader input,
			SpriteData existingInstance)
		{

			SpriteData sprite = new SpriteData();
			//Random r = new Random();
			//Read sprite data
			sprite.textureFile = input.ReadString();
			sprite.Width = input.ReadInt32 ();
			sprite.Height = input.ReadInt32 ();
			int frameCount = input.ReadInt32 ();
			for (int i = 0; i < frameCount; i++) {
				FrameData frame = new FrameData ();
				frame.textureCoords.X = input.ReadInt32 ();
				frame.textureCoords.Y = input.ReadInt32 ();
				frame.textureCoords.Width = input.ReadInt32 ();
				frame.textureCoords.Height = input.ReadInt32 ();
				sprite.frames.Add (frame);

			}

			int animationCount = input.ReadInt32 ();
			for (int i = 0; i < animationCount; i++) {
				string animationName = input.ReadString ();
				sprite.animationData.Add (animationName, new System.Collections.Generic.List<AnimFrameData> ());
				int animFrames = input.ReadInt32 ();
				for (int a = 0; a < animFrames; a++) {
					AnimFrameData anim = new AnimFrameData ();
					anim.SpriteIndex = input.ReadInt32 ();
					anim.frameDelay = input.ReadSingle ();
					anim.offset = new Microsoft.Xna.Framework.Point ();
					anim.offset.X = input.ReadInt32 ();
					anim.offset.Y = input.ReadInt32 ();
					sprite.animationData [animationName].Add (anim);

				}
			
			}


			return sprite;
		}
	}
}

