using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SuperFXContent;
namespace SuperEFEX.Renderer.ModelContent
{
	
	class PixelModelDataReader : ContentTypeReader<PixelModelData>
	{
		/// <summary>
		/// Loads an imported shader.
		/// </summary>
		protected override PixelModelData Read(ContentReader input,
			PixelModelData existingInstance)
		{
			//Random r = new Random();
			int vertices = input.ReadInt32();
			Vector3[] vertex = new Vector3[vertices];

			for(int i = 0 ; i < vertices; i++)
				vertex[i]= input.ReadVector3 ();
			
			int numFaces = input.ReadInt32 ();
			_FaceGroup[] faces = new _FaceGroup[numFaces];
			for (int i = 0; i < numFaces; i++) {
				int numSides = input.ReadInt32 ();
				faces [i] = new _FaceGroup ();
				faces [i].Nsides = numSides;
				faces [i].VertexIndex = new int[numSides];
				for (int j = 0; j < numSides; j++) {
					faces [i].VertexIndex [j] = input.ReadInt32 ();
				}
				faces [i].normal = input.ReadVector3 ();
				faces [i].Color = input.ReadColor ();
			}

			return new PixelModelData(vertex,faces);
		}
	}

}

