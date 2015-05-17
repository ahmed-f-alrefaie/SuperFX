using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperFXContent;
using System.Collections.Generic;
using SuperEFEX.Kernal;
using System.Diagnostics;
using SuperEFEX.Renderer;
using SuperEFEX.Core.Content.Graphics;
namespace SuperEFEX.Renderer.ModelContent
{




	public class Edge{
		public int VertexIndex1;
		public int VertexIndex2;
		public float depth0;
		public float depth1;
		public int minY;
		public int maxY;
		public int minX;
		public int maxX;
		public float m;
		public float minDepth;
		public float maxDepth;
		public Point textureCoord1;
		public Point textureCoord2;

	}

	public class Vertex{
		public Vector4[] vertex;
		public Vertex(Vector3[] pvertex){
			vertex = new Vector4[pvertex.Length];
			for(int i = 0; i < pvertex.Length; i++)
				vertex[i] = new Vector4(pvertex[i],1.0f);
		}

		public Vertex(Vector3 vertex):this(new Vector3[]{vertex}){
		}
	}


	public class EdgeTable{

		public Edge[] _edges;
		public Vertex vertices;
		public int minY;
		public int maxY;
		public int faceIndex;
		public float minDepth;
		public float maxDepth;

		public bool Textured = false;
		public RawTexture textureMap;
		public bool doLighting = true;

		public Color color;
		int nSides;
		public readonly _FaceGroup face;

		private Renderable3D mOwner=null;

		public Renderable3D Owner {
			get { return mOwner; }
		}

		public EdgeTable(Renderable3D pOwner,Vertex pVertex,int Nedges):this(pOwner,null,pVertex){
			nSides = Nedges;
		}

		public EdgeTable(Renderable3D pOwner,_FaceGroup pFace,Vertex pvertex){

			if (pFace != null) {
				face = pFace;
				nSides = face.Nsides;
				_edges = new Edge[face.Nsides];
				maxY = 0;
				minY = 99999;
				color = face.Color;
				for (int i = 0; i < face.Nsides; i++) {
					_edges [i] = new Edge ();
					_edges [i].VertexIndex1 = face.VertexIndex [i];
					if (i == face.Nsides - 1)
						_edges [i].VertexIndex2 = face.VertexIndex [0];
					else
						_edges [i].VertexIndex2 = face.VertexIndex [i + 1];


				}
				normal = face.normal;

			}
			mOwner = pOwner;
			vertices = pvertex;
		}

		private Vector3 normal;
		private Vector3 transformedNormal;
		public Vector3 Normal {
			get {
				return transformedNormal;
			}
		}



		public void UpdateEdgeTable(){
			minY = 999999;
			maxY = 0;
			if(face != null){
	//		ProfileSampler.StartTimer ("UpdateEdgeTable");
				for (int i = 0; i < nSides; i++) {
					Vector4 vert1 = vertices.vertex [_edges [i].VertexIndex1];
					Vector4 vert2 = vertices.vertex [_edges [i].VertexIndex2];
					_edges [i].minY = 9999;
					_edges [i].maxY = 0;
					_edges [i].minX = 9999;
					_edges [i].maxX = 0;
					_edges [i].minDepth = 100.0f;
					_edges [i].maxDepth = 0.0f;
					_edges [i].minY = (int)Math.Min (vert1.Y, vert2.Y);
					_edges [i].minDepth = Math.Min (vert1.Z, vert2.Z);
					_edges [i].maxDepth = Math.Max (vert1.Z, vert2.Z);
					_edges [i].maxY = (int)Math.Max (vert1.Y, vert2.Y);
					_edges [i].minX = (int)Math.Min (vert1.X, vert2.X);
					minY = (int)Math.Min (_edges [i].minY, minY);
					maxY = (int)Math.Max (_edges [i].maxY, maxY);
					_edges [i].maxX = (int)Math.Max (vert1.X, vert2.X);
					_edges [i].m = (vert2.Y - vert1.Y) / (vert2.X - vert1.X);
				}
					//if (vert1.X > vert2.X)
					//	GET [i]._edges [j].m *= -1.0f;

					Array.Sort<Edge> (_edges, (x, y) => x.minY.CompareTo (y.minY));
				}else{
				foreach(Vector4 v in vertices.vertex){
					minY = Math.Min((int)v.Y,minY);
					maxY = Math.Max((int)v.Y,maxY);
				}
			}
	//		ProfileSampler.StopTimer ("UpdateAllEdgeTables");
		}

		public void UpdateNormals(){
				transformedNormal = Vector3.TransformNormal (normal, mOwner.WorldMatrix);
			transformedNormal.Normalize ();


		}

	}

	struct XDepth{
		public int X;
		public float depth;
		public Point textureCoord;
		public bool Textured;
	}





	public class PixelModel : Renderable3D,IWorld{
		
		private Vector3 position;
		private Quaternion rotation;
		private Vector3 scale;



		PixelModelData mData;
		public Vertex transformedVertex;
		public EdgeTable[] GET;
		List<XDepth> ActiveX = new List<XDepth>();
		public override EdgeTable[] EdgeTables{ get{ return GET; } }

		public Vector3 Position {
			get {
				return position;
			}
			set {
				position = value;
			}
		}

		public Quaternion Rotation {
			get {
				return rotation;
			}
			set {
				rotation = value;
			}
		}

		public Vector3 Scale {
			get {
				return scale;
			}
			set {
				scale = value;
			}
		}



		public PixelModel (PixelModelData data) : base ()
		{
			Enabled = true;
			mData = data;
			//transformedVertex = new Vector3[mData.Vertices.Length];
			//Setup edgetable
			//faceColor = new Color[mData.Faces.Length];
			GET = new EdgeTable[mData.Faces.Length];
			transformedVertex = new Vertex (mData.Vertices);
			for (int i = 0; i < mData.Faces.Length; i++) {
				//On each edgetable initialise the face data;
				GET[i] = new EdgeTable(this,mData.Faces[i],transformedVertex);
		
			}



		}




		public void SetPosition(float x, float y, float z){
			position.X = x;
			position.Y = y;
			position.Z = z;


		}

		private void UpdateEdgeTables(){
			ProfileSampler.StartTimer ("UpdateAllEdgeTables");
			for (int i = 0; i < mData.Faces.Length; i++) {
				GET [i].UpdateEdgeTable ();
			}
			ProfileSampler.StopTimer ("UpdateAllEdgeTables");

			//Sort by y
	


		}

		float wobble = 0.9f;
		public void SetRotation(float x, float y, float z){
			rotation = Quaternion.CreateFromYawPitchRoll (x, y, z);


		}

		public void SetScale(float x, float y, float z){

			scale.X = x;
			scale.Y = y;
			scale.Z = z;


		}

		void UpdateNormals(){
			ProfileSampler.StartTimer ("UpdateNormals");
			for (int i = 0; i < GET.Length; i++)
				GET [i].UpdateNormals ();
			ProfileSampler.StopTimer ("UpdateNormals");
		}




		public void UpdateVertices(Matrix WorldViewProj,int width,int height){
			ProfileSampler.StartTimer ("UpdateVerticies");
				Vector4 screenVector = new Vector4 ();
				for (int i = 0; i < mData.Vertices.Length; i++) {
					screenVector.X = mData.Vertices [i].X;
					screenVector.Y = mData.Vertices [i].Y;
					screenVector.Z = mData.Vertices [i].Z;
					screenVector.W = 1.0f;
					screenVector = Vector4.Transform (screenVector, WorldViewProj);

					screenVector.X *= wobble;
					screenVector.Y *= wobble;
					int temp = (int)screenVector.X;
					screenVector.X = ((float)temp) / wobble;
					temp = (int)screenVector.Y;
					screenVector.Y = ((float)temp) / wobble;
				screenVector.X /= screenVector.W;
				screenVector.Y /= screenVector.W;
				screenVector.Z /= screenVector.W;
					transformedVertex.vertex [i].X = ((screenVector.X)) * (width / 2) + (width / 2);
					transformedVertex.vertex [i].Y = (-(screenVector.Y)) * (height / 2) + (height / 2);
					transformedVertex.vertex [i].Z = screenVector.Z;
					transformedVertex.vertex [i].W = screenVector.W;
				}
			ProfileSampler.StopTimer ("UpdateVerticies");
		}


		public void SetLighting(int facenumber,bool doLighting){
			if (GET.Length > facenumber)
				GET [facenumber].doLighting = doLighting;
		}
		public void SetColor(int facenumber,Color color){
			if (GET.Length > facenumber)
				GET [facenumber].color = color;
		}

		public override void Transform3D(){
			ProfileSampler.StartTimer ("Transform3D");

			//Debug.Assert (Camera.MainCamera != null, "No camera assigned as main!\n");


			//Update the world matrix
			world = Matrix.CreateScale (scale) * Matrix.CreateFromQuaternion (rotation) * Matrix.CreateTranslation (position);
			Matrix worldviewProj = world * Camera.MainCamera.ViewProjectionMatrix;
			UpdateVertices (worldviewProj, Camera.MainCamera.Width, Camera.MainCamera.Height);
			UpdateEdgeTables ();
			UpdateNormals ();

			ProfileSampler.StopTimer ("Transform3D");
		}
			/*
			GET [3].doLighting = false;
			GET [37].doLighting = false;
			GET [3].color = Color.Lerp (Color.Red, Color.Yellow,t);
			GET [37].color = Color.Lerp (Color.Blue, Color.LightBlue,t);
			GET [24].doLighting = false;
			GET [24].color = Color.Lerp (Color.Blue, Color.LightBlue,t);
			t += incr;
			if (t > 1.0f || t < 0) {
				incr *= -1.0f;
			}
			*/
			//GET [24].color = Color.White;
			//GET [24].doLighting = false;


		//	DrawEdgeTable (cam,plotter);

			/*
			for (int face = 0; face < mData.Faces.Length; face++) {
				float dot = Vector3.Dot (GET [face].Normal, cam.GetViewNormals);
				if (dot >= 0.0f)
					continue;

					for (int v = 0; v < mData.Faces [face].Nsides; v++) {

					int x0 = (int)transformedVertex.vertex  [mData.Faces [face].VertexIndex [v]].X;
					int y0 = (int)transformedVertex.vertex  [mData.Faces [face].VertexIndex [v]].Y;
						int x1, y1;
						if (v == mData.Faces [face].VertexIndex.Length - 1) {
						x1 = (int)transformedVertex.vertex  [mData.Faces [face].VertexIndex [0]].X;
						y1 = (int)transformedVertex.vertex  [mData.Faces [face].VertexIndex [0]].Y;
						} else {
						x1 = (int)transformedVertex.vertex  [mData.Faces [face].VertexIndex [v + 1]].X;
						y1 = (int)transformedVertex.vertex  [mData.Faces [face].VertexIndex [v + 1]].Y;
						}


						//plotter.DrawLine (x0, y0,
						//	x1, y1,
						//Color.White);


					}
				
			}
			*/

	//		ProfileSampler.StopTimer ("ModelDraw");


	//	}

		void Update(GameTime gameTime){







		}





	}

}

