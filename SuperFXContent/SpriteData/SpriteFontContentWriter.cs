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
	class SpriteFontDataWriter: ContentTypeWriter<SpriteFontData>
	{
		protected override void Write(ContentWriter output, SpriteFontData value)
		{
			output.Write (value.textureFile);
			output.Write (value.Width);
			output.Write (value.Height);
			output.Write (value.font.Count);
			foreach (KeyValuePair<char,Rectangle> kvp in value.font) {
				output.Write (kvp.Key); // Animation name
				output.Write(kvp.Value.X);
				output.Write(kvp.Value.Y);
				output.Write(kvp.Value.Width);
				output.Write(kvp.Value.Height);
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
			return typeof(SpriteFontData).AssemblyQualifiedName;
		}
		public override string GetRuntimeReader(TargetPlatform targetPlatform)
		{
			Console.WriteLine ("SuperEFEX.Renderer.SpriteFontDataReader, MonoGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			return "SuperEFEX.Renderer.SpriteFontDataReader, MonoGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
		}
	}
}

