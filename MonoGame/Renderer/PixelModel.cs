using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Renderer
{
	public class PixelModelOld
	{

		private Vector3 position;
		private Quaternion rotation;
		private Vector3 scale;
		float xrot =0.0f;
		Vector3[] vertexList;
		Vector3[] transformedVertex;
		int[] faceList;
		int vertexLength;
		int faceLength;
		bool xMirrored = false;
		Vector3 mirrorPoint;
		Matrix world;

		public PixelModelOld (Vector3 [] verticies,int[] faces,bool mirror)
		{
			/*
			vertexLength = verticies.Length;
			faceLength = faces.Length;
			faceList = faces;
			position = new Vector3(-20,0,0);
			rotation = Quaternion.CreateFromYawPitchRoll (0.0f, 0.0f, 0.0f);
			scale = new Vector3(0.5f,0.5f,0.5f);
			if (mirror){
				mirrorPoint = verticies[0];
				//mirrorPoint.X *= -1.0f;
				vertexList = new Vector3[2 * verticies.Length + 4];

				vertexList [0] = new Vector3(000, 000, -010);
				vertexList [1] = new 	Vector3 (000, 001, 000);
				vertexList [2] = new Vector3 (000, -006, 000);
				vertexList [3] = new Vector3 (000, 002, 080);


				for (int i = 0; i < verticies.Length; i++)
					vertexList [i+4] = verticies [i];

				//Now mirror
				for (int i = verticies.Length; i < 2 * verticies.Length; i++) {
					vertexList [i + 4] = (2*(vertexList [i + 4 - vertexLength]*mirrorPoint)/(mirrorPoint*mirrorPoint))*mirrorPoint - vertexList [i + 4 - vertexLength];
				//	vertexList [i + 4].X += mirrorPoint;
				}


				
			}

			*/


			vertexList = new [] {
				new Vector3 (-1.0f, -1.0f, 0), // triangle 1 : begin
				new Vector3 (1.0f, -1.0f, 0),
				new Vector3 (1.0f, 1.0f, 0), // triangle 1 : end
				new Vector3 (1.0f, 1.0f, -1.0f), // triangle 2 : begin
				new Vector3 (-1.0f, -1.0f, -1.0f),
				new Vector3 (-1.0f, 1.0f, -1.0f), // triangle 2 : end
				new Vector3 (1.0f, -1.0f, 1.0f),
				new Vector3 (-1.0f, -1.0f, -1.0f),
				new Vector3 (1.0f, -1.0f, -1.0f),
				new Vector3 (1.0f, 1.0f, -1.0f),
				new Vector3 (1.0f, -1.0f, -1.0f),
				new Vector3 (-1.0f, -1.0f, -1.0f),
				new Vector3 (-1.0f, -1.0f, -1.0f),
				new Vector3 (-1.0f, 1.0f, 1.0f),
				new Vector3 (-1.0f, 1.0f, -1.0f),
				new Vector3 (1.0f, -1.0f, 1.0f),
				new Vector3 (-1.0f, -1.0f, 1.0f),
				new Vector3 (-1.0f, -1.0f, -1.0f),
				new Vector3 (-1.0f, 1.0f, 1.0f),
				new Vector3 (-1.0f, -1.0f, 1.0f),
				new Vector3 (1.0f, -1.0f, 1.0f),
				new Vector3 (1.0f, 1.0f, 1.0f),
				new Vector3 (1.0f, -1.0f, -1.0f),
				new Vector3 (1.0f, 1.0f, -1.0f),
				new Vector3 (1.0f, -1.0f, -1.0f),
				new Vector3 (1.0f, 1.0f, 1.0f),
				new Vector3 (1.0f, -1.0f, 1.0f),
				new Vector3 (1.0f, 1.0f, 1.0f),
				new Vector3 (1.0f, 1.0f, -1.0f),
				new Vector3 (-1.0f, 1.0f, -1.0f),
				new Vector3 (1.0f, 1.0f, 1.0f),
				new Vector3 (-1.0f, 1.0f, -1.0f),
				new Vector3 (-1.0f, 1.0f, 1.0f),
				new Vector3 (1.0f, 1.0f, 1.0f),
				new Vector3 (-1.0f, 1.0f, 1.0f),
				new Vector3 (1.0f, -1.0f, 1.0f)
			};


				transformedVertex = new Vector3[vertexList.Length];

		}


		public void SetPosition(float x, float y, float z){
			position.X = x;
			position.Y = y;
			position.Z = z;


		}
		public void SetRotation(float x, float y, float z){
			rotation = Quaternion.CreateFromYawPitchRoll (x, y, z);


		}

		public void SetScale(float x, float y, float z){

			scale.X = x;
			scale.Y = y;
			scale.Z = z;


		}

		public void Draw(Matrix viewProj,PixelPlotter plotter){

			world = Matrix.CreateScale (scale) * Matrix.CreateFromQuaternion (rotation) * Matrix.CreateTranslation (position);
			Matrix worldviewProj = world*viewProj;
			//Now we'll transform all the verticies
			for (int i = 0; i < vertexList.Length; i++){
				Vector4 tempVector = new Vector4(vertexList [i],1);
				tempVector = Vector4.Transform (tempVector, worldviewProj);
				tempVector /= tempVector.W;
				transformedVertex [i].X = ((tempVector.X)) * (plotter.Width / 2) + (plotter.Height/2);
				transformedVertex[i].Y = ((tempVector.Y))*(plotter.Height/2) + (plotter.Height/2);
				//Console.Write (vertexList [i].ToString());
				//Console.Write (transformedVertex [i].ToString());

			}


			/*

			for (int face = 0; face < faceList.Length; face += 3) {
				plotter.DrawLine((int) transformedVertex[faceList[face]].X ,(int)transformedVertex[faceList[face]].Y,
					(int)( transformedVertex[faceList[face+1]].X) ,(int)transformedVertex[faceList[face+1]].Y,
					Color.White);
				
				plotter.DrawLine((int)( transformedVertex[faceList[face+1]].X),(int)transformedVertex[faceList[face+1]].Y,
					(int)( transformedVertex[faceList[face+2]].X) ,(int)transformedVertex[faceList[face+2]].Y,
					Color.White);
				
				plotter.DrawLine((int)( transformedVertex[faceList[face+2]].X) ,(int)transformedVertex[faceList[face+2]].Y,
					(int)( transformedVertex[faceList[face]].X) ,(int)(transformedVertex[faceList[face]].Y),
					Color.White);



			}
			*/
			/*int f = 0;
			while (f < faceLength) {
				int count = faceList [f];
				int g =f;
				while (g < count+f) {
					
					
				}


			}*/



			for (int i = 0; i < vertexList.Length; i +=3) {
				plotter.DrawLine ((int)transformedVertex [i].X, (int)transformedVertex [i].Y,
					(int)(transformedVertex [i + 1].X), (int)transformedVertex [i + 1].Y,
					Color.White);				
				plotter.DrawLine ((int)transformedVertex [i + 1].X, (int)transformedVertex [i + 1].Y,
					(int)(transformedVertex [i + 2].X), (int)transformedVertex [i + 2].Y,
					Color.White);	
				plotter.DrawLine((int) transformedVertex[i+2].X ,(int)transformedVertex[i+2].Y,
					(int)( transformedVertex[i].X) ,(int)transformedVertex[i].Y,
					Color.White);	

		//		plotter.PlotPixel ((int)transformedVertex [i].X, (int)transformedVertex [i].Y, Color.White);
			}


		}

		void Update(GameTime gameTime){







		}





	}
}

