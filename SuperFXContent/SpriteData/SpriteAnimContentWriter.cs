using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace SuperFXContent
{
	[ContentTypeWriter]
	class SpriteDataWriter: ContentTypeWriter<SpriteData>
	{
		protected override void Write(ContentWriter output, SpriteData value)
		{
			output.Write (value.textureFile);
			output.Write (value.Width);
			output.Write (value.Height);
			output.Write (value.frames.Count);
			for (int i = 0; i < value.frames.Count; i++) {
				output.Write(value.frames [i].textureCoords.X);
				output.Write(value.frames [i].textureCoords.Y);
				output.Write(value.frames [i].textureCoords.Width);
				output.Write(value.frames [i].textureCoords.Height);
			}
			output.Write (value.animationData.Count); // Number of animations
			foreach (KeyValuePair<string,List<AnimFrameData>> kvp in value.animationData) {
				output.Write (kvp.Key); // Animation name
				output.Write(kvp.Value.Count);
				for (int i = 0; i < kvp.Value.Count; i++) {
					output.Write (kvp.Value [i].SpriteIndex);
					output.Write (kvp.Value [i].frameDelay);
					output.Write (kvp.Value [i].offset.X);
					output.Write (kvp.Value [i].offset.Y);
				}


			}
			/*
			output.Write(value.Vertices.Length);
			for (int i = 0; i < value.Vertices.Length; i++)
				output.Write (value.Vertices [i]);
			output.Write(value.Faces.Length);
			for (int i = 0; i < value.Faces.Length; i++) {
				output.Write (value.Faces [i].Nsides);
				for (int j = 0; j < value.Faces [i].Nsides; j++)
					output.Write (value.Faces [i].VertexIndex [j]);
				System.Console.Write (value.Faces [i].normal.ToString () + "\n");
				output.Write (value.Faces [i].normal);
				output.Write (value.Faces [i].Color);
			}
			*/

		}
		public override string GetRuntimeType(TargetPlatform targetPlatform)
		{
			return typeof(PixelModelData).AssemblyQualifiedName;
		}
		public override string GetRuntimeReader(TargetPlatform targetPlatform)
		{
			Console.WriteLine ("SuperEFEX.Renderer.SpriteDataReader, MonoGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			return "SuperEFEX.Renderer.SpriteDataReader, MonoGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
		}
	}
}

