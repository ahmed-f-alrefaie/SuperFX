using System;
using Microsoft.Xna.Framework;
using Kernal;
namespace Renderer.ModelContent
{
	public class FloorGrid : Renderable3D,IWorld
	{
		EdgeTable[] floor = new EdgeTable[400];
		public override EdgeTable[] EdgeTables {
			get {
				return floor;
			}
		}

	

		Vector4 screenVector = new Vector4 ();
		Vector3[] raw_floor = new Vector3[400];

		Vertex[] transVerts = new Vertex[400];

		int width,height;

		public override void Transform3D ()
		{
			ProfileSampler.StartTimer ("UpdateVerticies");
			width = Camera.MainCamera.Width;
			height = Camera.MainCamera.Height;
			Matrix world = Matrix.CreateScale (3.0f,1.0f,3.0f) * Matrix.CreateFromQuaternion (Quaternion.Identity) * Matrix.CreateTranslation (0.0f, 0.0f, 0.0f);
			Vector4 screenVector = new Vector4 ();
			for (int i = 0; i < raw_floor.Length; i++) {
				screenVector.X = raw_floor[i].X;
				screenVector.Y = raw_floor[i].Y;
				screenVector.Z = raw_floor[i].Z;
				screenVector.W = 1.0f;
				screenVector = Vector4.Transform (screenVector, world*Camera.MainCamera.ViewProjectionMatrix);
				screenVector.X /= screenVector.W;
				screenVector.Y /= screenVector.W;
				screenVector.Z /= screenVector.W;
				transVerts [i].vertex [0].X = ((screenVector.X)) * (width / 2) + (width / 2);
				transVerts[i].vertex [0].Y = (-(screenVector.Y)) * (height / 2) + (height / 2);
				transVerts[i].vertex [0].Z = screenVector.Z;
				transVerts[i].vertex [0].W = screenVector.W;
			}
			ProfileSampler.StopTimer ("UpdateVerticies");			
			
		}

		public FloorGrid ():base()
		{

			Enabled = true;
			int i = 0;
			for (int x = -100; x < 100; x += 10) {
				for (int z = -100; z < 100; z += 10){
					raw_floor [i] = new Vector3 ((float)x, 0.0f, (float)z);
					transVerts [i] = new Vertex (raw_floor [i]);
					floor [i] = new EdgeTable (this, transVerts [i], 1);
					floor [i].color = Color.White;
					i++;


				}


			}



			
		}
	}
}

