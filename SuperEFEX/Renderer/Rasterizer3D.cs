﻿using System;
using SuperEFEX.Renderer.ModelContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using SuperEFEX.Kernal;
namespace SuperEFEX.Renderer
{

	public enum RasterType{
		WIREFRAME,
		FLAT_SHADED
	}

	public class Rasterizer3D : RendererBase
	{
		public override RenderType rendertype{get{ return RenderType.RENDER_3D; }}
		private List<EdgeTable> mEdgeTables = new List<EdgeTable>();
		private List<Renderable3D> mRenderables = new List<Renderable3D>();
		private PixelPlotter plotter;
		//Fixed Color
		private Vector3 lightDirection;
		private Vector3 ambientColor;
		private List<XDepth> ActiveX = new List<XDepth> ();
		private List<Vector4> ActiveVerticies = new List<Vector4> (32);
		public RasterType raster = RasterType.FLAT_SHADED;

		const float near = 0.1f;

		public Rasterizer3D (SpriteBatch pSpriteBatch,PixelPlotter pPlotter) : base(pSpriteBatch,30)
		{
			plotter = pPlotter;
		}

		public void SetRenderMode(RasterType raster){
			this.raster = raster;
		}

		public void AddPolygons(EdgeTable[] pEdgeTable){
			mEdgeTables.AddRange (pEdgeTable);

		}
		public void AddPolygon(EdgeTable pEdgeTable){
			mEdgeTables.Add (pEdgeTable);
		}

		protected override void RegisterObject (IRenderable Renderable)
		{
			if (Renderable.RenderType == RenderType.RENDER_3D) {
				mRenderables.Add ((Renderable3D)Renderable);
			//	AddPolygons (((Renderable3D)Renderable).EdgeTables);


			}
		}

		protected override void Render(){
			TransformAll ();
			if (raster == RasterType.FLAT_SHADED)
				DrawEdgeTables ();
			else
				DrawWireframe ();
			mEdgeTables.Clear ();
			plotter.Draw (spriteBatch);
		}

		private void TransformAll(){
			ProfileSampler.StartTimer ("TransformAll");
			foreach (Renderable3D r in mRenderables) {
				if (r.Enable) {
					r.Transform3D ();
					for (int i = 0; i < r.EdgeTables.Length; i++) {
						if(0>Vector3.Dot (r.EdgeTables [i].Normal, Camera.MainCamera.GetViewNormals) || r.EdgeTables[i].vertices.vertex.Length == 1)
							AddPolygon (r.EdgeTables[i]);
					}
				}
			}
			ProfileSampler.StopTimer ("TransformAll");

		}

		private void DrawWireframe(){
			for (int i = 0; i < mEdgeTables.Count; i++) {
				if (mEdgeTables [i].Owner.Enable == false)
					continue;
				
				float dot = Vector3.Dot (mEdgeTables [i].Normal, Camera.MainCamera.GetViewNormals);
				if (dot < 0.0f)
					continue;
				
				for(int j = 0; j < mEdgeTables[i]._edges.Length; j++){

					Vector4 v1;
					Vector4 v2;
					v1 = mEdgeTables [i].vertices.vertex [mEdgeTables [i]._edges [j].VertexIndex1];
					v2 = mEdgeTables [i].vertices.vertex [mEdgeTables [i]._edges [j].VertexIndex2];
					plotter.DrawLine ((int)v1.X, (int)v1.Y, (int)v2.X, (int)v2.Y, mEdgeTables [i].color);


					/*
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
					*/

					//plotter.DrawLine (x0, y0,
					//	x1, y1,
					//Color.White);


				}

			}
		}

		private void DrawEdgeTables_II(){
			//	ProfileSampler.StartTimer ("DrawEdgeTables");
			Vector4 v1, v2;
			//Edge Xval = new XDepth ();
			//Sort Arrays
			mEdgeTables.Sort ((x, y) => x.minY.CompareTo (y.minY));
			for (int i = 0; i < mEdgeTables.Count; i++) {
				if (mEdgeTables [i].Owner.Enable != true)
					continue;
				int minY, maxY;
				minY = mEdgeTables [i].minY;
				maxY = mEdgeTables [i].maxY;

				if (maxY < 0 || minY > plotter.Height)
					continue;

				minY = MathHelper.Clamp (minY, 0, plotter.Height);
				maxY = MathHelper.Clamp (maxY, 0, plotter.Height);

				if (mEdgeTables [i].vertices.vertex.Length == 1) {

					v1 = mEdgeTables [i].vertices.vertex [0];
					if (v1.W >= 0.1f)
						plotter.PlotPixelDepth ((int)v1.X, (int)v1.Y, mEdgeTables [i].color, v1.Z);

				}
				if (mEdgeTables [i].vertices.vertex.Length == 2) {
					//Just draw it if its a line
					v1 = mEdgeTables [i].vertices.vertex [0];
					v2 = mEdgeTables [i].vertices.vertex [1];
					plotter.DrawLineDepth ((int)v1.X, (int)v1.Y, (int)v2.X, (int)v2.Y, v1.Z, v2.Z, mEdgeTables [i].color);

				}
			
				//Triangle case
				if (mEdgeTables [i]._edges.Length == 3) {


					ActiveVerticies.Add(mEdgeTables [i].vertices.vertex[mEdgeTables [i]._edges[0].VertexIndex1]);
					ActiveVerticies.Add(mEdgeTables [i].vertices.vertex[mEdgeTables [i]._edges[1].VertexIndex1]);
					ActiveVerticies.Add(mEdgeTables [i].vertices.vertex[mEdgeTables [i]._edges[2].VertexIndex1]);

					ActiveVerticies.Sort ((x, y) => {
						return x.Y.CompareTo (y.Y);
					});

					Vector4 vt1 = ActiveVerticies [0];
					Vector4 vt2 = ActiveVerticies [1];
					Vector4 vt3 = ActiveVerticies [2];
					if (vt2.Y == vt3.Y) {
						DrawBottomFlatTriangle (vt1, vt2, vt3);

					} else if (vt1.Y == vt2.Y) {
						DrawTopFlatTriangle (ActiveVerticies [0], ActiveVerticies [1], ActiveVerticies [2]);

					} else {
						Vector4 vt4 = new Vector4((vt1.X + (vt2.Y-vt1.Y)/(vt3.Y-vt1.Y))*(vt3.X - vt1.X),vt2.Y,0.5f,1.0f);
						DrawBottomFlatTriangle(vt1,vt2,vt4);
						DrawTopFlatTriangle(vt2,vt4,vt3);
					}
					ActiveVerticies.Clear ();

				}
				//Polygon case
				else {


				}



			}


				ProfileSampler.StopTimer ("DrawEdgeTables");
		}


		private void DrawBottomFlatTriangle(Vector4 v1,Vector4 v2, Vector4 v3){



			float invslope1 = (v2.X - v1.X) / (v2.Y - v1.Y);
			float invslope2 = (v3.X - v1.X) / (v3.Y - v1.Y);
			float curx1 = v1.X;
			float curx2 = v1.X;
			for(int scanline = (int)v1.Y; scanline <= (int)v2.Y; scanline++){
				plotter.DrawScanLineDepth((int)curx1,(int)curx2,scanline,0.5f,0.5f,Color.Red);
				curx1 += invslope1;
				curx2 += invslope2;
			}

		}
		private void DrawTopFlatTriangle(Vector4 v1,Vector4 v2, Vector4 v3){
			float invslope1 = (v3.X - v1.X) / (v3.Y - v1.Y);
			float invslope2 = (v3.X - v2.X) / (v3.Y - v2.Y);
			float curx1 = v1.X;
			float curx2 = v1.X;
			for(int scanline = (int)v3.Y; scanline > (int)v1.Y; scanline--){
				curx1 -= invslope1;
				curx2 -= invslope2;
				plotter.DrawScanLineDepth((int)curx1,(int)curx2,scanline,0.5f,0.5f,Color.Red);

			}

		}

		private void DrawEdgeTables(){
			ProfileSampler.StartTimer ("DrawEdgeTables");
			Vector4 v1, v2;
			XDepth Xval = new XDepth ();
			//Sort Arrays
			mEdgeTables.Sort ((x, y) => x.minY.CompareTo (y.minY));
			for (int i = 0; i < mEdgeTables.Count; i++) {
				if (mEdgeTables [i].Owner.Enable != true)
					continue;
				int minY, maxY;
				minY = mEdgeTables [i].minY;
				maxY = mEdgeTables [i].maxY;
				minY = MathHelper.Clamp (minY, 0, plotter.Height);
				maxY = MathHelper.Clamp (maxY, 0, plotter.Height);
				if (mEdgeTables [i].vertices.vertex.Length == 1) {

					v1 = mEdgeTables [i].vertices.vertex [0];
					if(v1.W >= 0.1f)
						plotter.PlotPixelDepth ((int)v1.X, (int)v1.Y, mEdgeTables [i].color, v1.Z);

				}
				if (mEdgeTables [i].vertices.vertex.Length == 2) {
					//Just draw it if its a line
					v1 = mEdgeTables [i].vertices.vertex [0];
					v2 = mEdgeTables [i].vertices.vertex [1];
					plotter.DrawLineDepth ((int)v1.X, (int)v1.Y, (int)v2.X, (int)v2.Y, v1.Z, v2.Z, mEdgeTables [i].color);

				}


				float dot = Vector3.Dot (mEdgeTables[i].Normal, Camera.MainCamera.GetViewNormals);
				if (dot > 0.0f)
					continue;

				for (int y = minY; y < maxY; y++) {
					
					bool parity = false;
					//Create active edgetable
					for (int j = 0; j < mEdgeTables [i]._edges.Length; j++) {

						if (mEdgeTables [i]._edges [j].minY <= y && mEdgeTables [i]._edges [j].maxY > y) {

							Edge edge = mEdgeTables [i]._edges [j];
							if (edge.m != 0.0) {
								
								v1 = mEdgeTables [i].vertices.vertex [mEdgeTables [i]._edges [j].VertexIndex1];
								v2 = mEdgeTables [i].vertices.vertex [mEdgeTables [i]._edges [j].VertexIndex2];
								if (v1.W < near || v2.W < near)
									continue;
							
								/*
								//Transform the verticies if they are near or far
								if (v2.W < near) {
									float n = (v1.W - near)/ (v1.W - v2.W);
									v2.X = (n * v1.X) + ((1.0f - n) * v2.X);
									v2.Y = (n * v1.Y) + ((1.0f - n) * v2.Y);
									v2.Z = (n * v1.Z) + ((1.0f - n) * v2.Z);

								} else if (v1.W < near) {
									float n = (v2.W - near)/ (v2.W - v1.W);
									v1.X = (n * v2.X) + ((1.0f - n) * v1.X);
									v1.Y = (n * v2.Y) + ((1.0f - n) * v1.Y);
									v1.Z = (n * v2.Z) + ((1.0f - n) * v1.Z);
								}
								*/
								float amount = (((float)(y - v1.Y) / edge.m));

								Xval.X = (int)(amount + (float)v1.X);

								float beta = (y - v1.Y) / (v2.Y - v1.Y);
								Xval.X = MathHelper.Clamp (Xval.X, edge.minX, edge.maxX);
								float d0, d1;
								d0 = v1.Z;
								d1 = v2.Z;
								float dz_dy = (d1 - d0) / (v2.Y - v1.Y);
								Xval.depth = dz_dy * (float)(y - v1.Y) + d0;
								Xval.Textured = mEdgeTables [i].Textured;
								if (Xval.Textured) {
									Point t1 = mEdgeTables [i]._edges [j].textureCoord1;
									Point t2 = mEdgeTables [i]._edges [j].textureCoord2;
									float dv_dy = (float)(t2.Y - t1.Y) / (v2.Y - v1.Y);
									float du_dy = (float)(t2.X - t1.X) / (v2.Y - v1.Y);
									Xval.textureCoord.X = (int)(du_dy * (y - v1.Y) + (float)t1.X);
									Xval.textureCoord.Y = (int)(dv_dy * (y - v1.Y) + (float)t1.Y);
									//float alpha = (float)(mEdgeTables [i]._edges [j].textureCoord2.X - mEdgeTables [i]._edges [j].textureCoord1.X) / (v2.Y - v1.Y);
									//Xval.textureCoord.X = (int)(alpha * (float)y + (float)mEdgeTables [i]._edges [j].textureCoord1.X);
									//Xval.textureCoord.Y = (int)((float)mEdgeTables [i]._edges [j].textureCoord1.Y * (1 - beta) + beta * (float)mEdgeTables [i]._edges [j].textureCoord2.Y);
								}
								//Xval.depth = MathHelper.Clamp (Xval.depth, edge.minDepth, edge.maxDepth);
								//		if(Xval.depth < 1.0f && Xval.depth > -1.0f)
								ActiveX.Add (Xval);
							}
						}
						//X = (int)Math.Min (GET [i]._edges [j].minX, X);
					}
					if (ActiveX.Count == 0)
						continue;
					//Collected X now sort by X
					ActiveX.Sort ((a, b) => a.X.CompareTo (b.X));
					Color rendColor = mEdgeTables [i].color;
					if (mEdgeTables [i].doLighting) {
						float intensity = Math.Max (0, Vector3.Dot (Vector3.Up, mEdgeTables [i].Normal));
						intensity = MathHelper.Clamp (intensity, 0.0f, 1.0f);
						Vector3 inte = mEdgeTables [i].color.ToVector3 ();
						inte *= intensity;
						inte += new Vector3 (0.2f, 0.2f, 0.2f);
						rendColor = new Color (inte);
					}



				//	ProfileSampler.StartTimer ("Raster Edges");
					//Now we begin drawing
					for (int x = 0; x < ActiveX.Count; x++) {



						if (x != ActiveX.Count - 1) {
							if (Keyboard.GetState ().IsKeyDown (Keys.A))
								plotter.DrawLine (ActiveX [x].X, y, ActiveX [x + 1].X, y, mEdgeTables [i].color);
							else {
								if (ActiveX [x].Textured) {
									plotter.DrawTexturedScanLineDepth (ActiveX [x].X, ActiveX [x + 1].X, y, ActiveX [x].depth, ActiveX [x + 1].depth, ActiveX [x].textureCoord, ActiveX [x + 1].textureCoord, mEdgeTables [i].textureMap);
								} else {
									plotter.DrawScanLineDepth (ActiveX [x].X, ActiveX [x + 1].X, y, ActiveX [x].depth, ActiveX [x + 1].depth, rendColor);
								}
							}

						}
						parity = !parity;	


					}
			//		ProfileSampler.StopTimer ("Raster Edges");
					parity = false;
					ActiveX.Clear ();







				}



			}


			ProfileSampler.StopTimer ("DrawEdgeTables");
		}

	}
}

