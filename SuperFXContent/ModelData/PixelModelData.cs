using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
namespace SuperFXContent
{
	public class _FaceGroup{
		public int Nsides;
		public Color Color;
		public int[] VertexIndex;
		public Vector3 normal;

	}




	public class PixelModelData
	{

		//ADD BSP TREE Compiling

		private Vector3[] mVertices;

		private _FaceGroup[] mFaces;

		int numVertices;

		public Vector3[] Vertices {
			get {
				return mVertices;
			}
		}

		public _FaceGroup[] Faces {
			get {
				return mFaces;
			}
		}
		//public PixelModelData(){}
		public PixelModelData (Vector3[] vertices,_FaceGroup[] faces)
		{
			mVertices = new Vector3[vertices.Length];
			mFaces = new _FaceGroup[faces.Length];
			for(int i=0 ; i < vertices.Length; i++)
				mVertices[i] = vertices[i];
			for (int i = 0; i < faces.Length; i++) {
				mFaces [i]= new _FaceGroup ();
				mFaces [i].Nsides = faces [i].Nsides;
				mFaces [i].VertexIndex = new int[mFaces [i].Nsides];
				for(int j = 0; j < mFaces[i].Nsides; j++)
					mFaces [i].VertexIndex[j] = faces [i].VertexIndex[j];
				mFaces [i].normal = faces [i].normal;
				mFaces [i].Color = faces [i].Color;
			}
		}
	}
}

