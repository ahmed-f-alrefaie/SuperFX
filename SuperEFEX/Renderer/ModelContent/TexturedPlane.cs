using System;
using Microsoft.Xna.Framework;
using SuperEFEX.Kernal;
using SuperEFEX.Core.Content.Graphics;
using SuperEFEX.Core.Content;
namespace SuperEFEX.Renderer.ModelContent
{
	public class TexturedPlane : Renderable3D,IWorld
	{
		EdgeTable[] mSquare = new EdgeTable[2];
		public override EdgeTable[] EdgeTables {
			get {
				return mSquare;
			}
		}
		Vertex mVerts;
		Vector3[] rawVerts = new Vector3[4];
		SuperFXContent._FaceGroup[] faces = new SuperFXContent._FaceGroup[2];

		RawTexture mTextureMap;


		Vector4 screenVector = new Vector4 ();
		float scale = 10.0f;
		int width,height;
		Vector3 position = new Vector3(40,20,50);
		float t1 = 0.1f;
		public override void Transform3D ()
		{
			ProfileSampler.StartTimer ("UpdateVerticies");
			width = Camera.MainCamera.Width;
			height = Camera.MainCamera.Height;
			t1 += 0.01f;

			world = Matrix.CreateScale (scale,scale,scale) * Matrix.CreateFromQuaternion (Quaternion.CreateFromYawPitchRoll(0,0,t1)) * Matrix.CreateTranslation (position);
			Vector4 screenVector = new Vector4 ();
			for (int i = 0; i < rawVerts.Length; i++) {
				screenVector.X = rawVerts[i].X;
				screenVector.Y = rawVerts[i].Y;
				screenVector.Z = rawVerts[i].Z;
				screenVector.W = 1.0f;
				screenVector = Vector4.Transform (screenVector, world*Camera.MainCamera.ViewProjectionMatrix);
				screenVector.X /= screenVector.W;
				screenVector.Y /= screenVector.W;
				screenVector.Z /= screenVector.W;
				mVerts.vertex[i].X = ((screenVector.X)) * (width / 2) + (width / 2);
				mVerts.vertex[i].Y = (-(screenVector.Y)) * (height / 2) + (height / 2);
				mVerts.vertex [i].Z = screenVector.Z;
				mVerts.vertex [i].W = screenVector.W;
			//	transVerts [i].vertex [0].X = ((screenVector.X)) * (width / 2) + (width / 2);
			//	transVerts[i].vertex [0].Y = (-(screenVector.Y)) * (height / 2) + (height / 2);
			//	transVerts[i].vertex [0].Z = screenVector.Z;
			//	transVerts[i].vertex [0].W = screenVector.W;
			}
			mSquare [0].UpdateNormals ();
			mSquare [0].UpdateEdgeTable ();
			mSquare [0].Textured = true;
			mSquare [1].UpdateNormals ();
			mSquare [1].UpdateEdgeTable ();
			mSquare [1].Textured = true;
			ProfileSampler.StopTimer ("UpdateVerticies");			

		}

		public TexturedPlane (FXContent content):base()
		{
			mTextureMap = content.Load<RawTexture> ("andross_cube");
			rawVerts[0] = new Vector3 (-1.0f, 1.0f, 0.0f);
			rawVerts[1] = new Vector3 (1.0f, 1.0f, 0.0f);
			rawVerts[2] = new Vector3 (1.0f, -1.0f, 0.0f);
			rawVerts[3] = new Vector3 (-1.0f, -1.0f, 0.0f);
			mVerts = new Vertex (rawVerts);
			faces [0] = new SuperFXContent._FaceGroup ();
			faces [0].Nsides = 3;
			faces [0].normal = Vector3.Forward;
			faces [0].VertexIndex = new int[]{ 0, 1, 2 };

			faces [1] = new SuperFXContent._FaceGroup ();
			faces [1].Nsides = 3;
			faces [1].normal = Vector3.Forward;
			faces [1].VertexIndex = new int[]{ 0, 2, 3 };

			mSquare [0] = new EdgeTable (this, faces[0], mVerts);

			mSquare [0]._edges [0].textureCoord1 = new Point (0, 0);
			mSquare [0]._edges [0].textureCoord2 = new Point (mTextureMap.Width-1, 0);

			mSquare [0]._edges [1].textureCoord1 = new Point (mTextureMap.Width-1, 0);
			mSquare [0]._edges [1].textureCoord2 = new Point (mTextureMap.Width-1, mTextureMap.Height-1);

			mSquare [0]._edges [2].textureCoord1 = new Point (mTextureMap.Width-1, mTextureMap.Height-1);
			mSquare [0]._edges [2].textureCoord2 = new Point (0,0);


			mSquare [0].textureMap = mTextureMap;

			mSquare [1] = new EdgeTable (this, faces[1], mVerts);

			mSquare [1]._edges [0].textureCoord1 = new Point (0, 0);
			mSquare [1]._edges [0].textureCoord2 = new Point (mTextureMap.Width-1, mTextureMap.Height-1);

			mSquare [1]._edges [1].textureCoord1 = new Point (mTextureMap.Width-1, mTextureMap.Height-1);
			mSquare [1]._edges [1].textureCoord2 = new Point (0, mTextureMap.Height-1);

			mSquare [1]._edges [2].textureCoord1 = new Point (0, mTextureMap.Height-1);
			mSquare [1]._edges [2].textureCoord2 = new Point (0,0);

			mSquare [1].textureMap = mTextureMap;

			Enabled = true;
		}
	}
}