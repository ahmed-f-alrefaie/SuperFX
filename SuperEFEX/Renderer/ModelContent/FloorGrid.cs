using System;
using Microsoft.Xna.Framework;
using SuperEFEX.Kernal;
namespace SuperEFEX.Renderer.ModelContent
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
		float scale = 2.0f;
		int width,height;
		Vector3 position = Vector3.Zero;
		public override void Transform3D ()
		{
			ProfileSampler.StartTimer ("UpdateVerticies");
			width = Camera.MainCamera.Width;
			height = Camera.MainCamera.Height;
			float xDif = Camera.MainCamera.Position.X - position.X;
			float zDif = Camera.MainCamera.Position.Z - position.Z;
			if (Math.Abs (xDif) > 10.0f*scale)
				position.X = Camera.MainCamera.Position.X;
			if (Math.Abs (zDif) > 10.0f*scale)
				position.Z = Camera.MainCamera.Position.Z;

			Matrix world = Matrix.CreateScale (scale,1.0f,scale) * Matrix.CreateFromQuaternion (Quaternion.Identity) * Matrix.CreateTranslation (position);
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

