using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using TImport = System.String;

namespace SuperFXContent
{
	
	[ContentTypeWriter]
	class PixelModelDataWriter: ContentTypeWriter<PixelModelData>
	{
		protected override void Write(ContentWriter output, PixelModelData value)
		{
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
			
		}
		public override string GetRuntimeType(TargetPlatform targetPlatform)
		{
			return typeof(PixelModelData).AssemblyQualifiedName;
		}
		public override string GetRuntimeReader(TargetPlatform targetPlatform)
		{
			return "MonoGame.PixelModelDataReader, MonoGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
		}
	}



}


