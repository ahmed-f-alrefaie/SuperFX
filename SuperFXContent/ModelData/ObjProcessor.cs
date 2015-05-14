using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;


using TInput = System.String;
using TOutput = System.String;

namespace SuperFXContent
{
	
	[ContentProcessor (DisplayName = "ObjProcessor")]
	class ObjProcessor : ContentProcessor<OBJData, PixelModelData>
	{
		public override PixelModelData Process (OBJData input, ContentProcessorContext context)
		{

			_FaceGroup[] faces;
			Vector3[] vertices;
			Dictionary<string,Color> mtl = new Dictionary<string, Color>();
			faces = new _FaceGroup[input.NumFaces];
			vertices = new Vector3[input.NumVertices];
			bool material = false;

			Console.WriteLine (input.HasMaterial.ToString ());
			if(input.HasMaterial==true){
				material = true;
				StringReader mtlreader = new StringReader (input.RawMtlData);
				string mline;
				while ((mline = mtlreader.ReadLine ()) != null) {
					mline = mline.Trim ();

					if (mline.Length < 2)
						continue;
					//Read material name
					string[] vals = mline.Split(new[]{' '},StringSplitOptions.RemoveEmptyEntries);
					if (vals [0] == "newmtl") {
						mline = mtlreader.ReadLine ();
						mline = mtlreader.ReadLine ();
						mline = mtlreader.ReadLine ();
						Console.WriteLine (mline);
						string[] mtlvals = mline.Split(new[]{' '},StringSplitOptions.RemoveEmptyEntries);
						mtl [vals [1]] = new Color (Single.Parse (mtlvals [1]), Single.Parse (mtlvals [2]), Single.Parse (mtlvals [3]));
						Console.WriteLine (vals[1]+" "+mtl[vals[1]].ToString());
					}




				}

			}


			StringReader reader = new StringReader (input.RawObjData);
			string line;
			//Lets begin readding
			int vertexCount = 0;
			int faceCount = 0;
			Color currentcolor = Color.White;
			while ((line = reader.ReadLine ()) != null) {
				line = line.Trim ();
				//System.Console.Write (line+"\n");
				if (line.Length < 2)
					continue;
				string[] vals = line.Split(new[]{' '},StringSplitOptions.RemoveEmptyEntries);

					//System.Console.Write (vals.Length);
					if (vals.Length < 2)
						continue;
					if (vals [0] == "v") {
					Console.WriteLine (vals[0]);
						vertices [vertexCount] = new Vector3 (Single.Parse (vals [1]), Single.Parse (vals [2]), Single.Parse (vals [3]));
						vertexCount++;



					} else if (vals [0] == "f") {
					Console.WriteLine (vals[0]);
						//Count vals as sides
						//System.Console.Write (line+"\n");
						faces[faceCount] = new _FaceGroup();
						//Minus the F character
						faces [faceCount].Nsides = vals.Length-1;
						//System.Console.Write (faces [faceCount].Nsides.ToString()+"\n");
						faces [faceCount].VertexIndex = new int[faces [faceCount].Nsides];
						for (int i = 1; i < vals.Length; i++) {
							//Further splitAlong /
							string[] faceSplit = vals[i].Split('/');
							faces [faceCount].VertexIndex [i - 1] = int.Parse (faceSplit [0])-1;


						}
						Vector3 v1;
						Vector3 v2;
						Console.WriteLine ("NORMALS!");
						//Compute normal data
						//for (int i = 0; i < input.NumFaces; i++) {
						faces [faceCount].normal = Vector3.Zero;
							//Compute normals
						if (faces [faceCount].Nsides > 2) {
							v1 = vertices [faces [faceCount].VertexIndex [1]] - vertices [faces [faceCount].VertexIndex [0]];
							v2 = vertices [faces [faceCount].VertexIndex [2]] - vertices [faces [faceCount].VertexIndex [0]];
							faces [faceCount].normal = Vector3.Cross (v1, v2);
							faces [faceCount].normal.Normalize ();
							//	System.Console.Write (faces [i].normal.ToString()+"\n");

							//}


						}


						
						faces[faceCount].Color = currentcolor;
						faceCount++;


					}else if(vals[0]== "usemtl"){
					Console.WriteLine (vals[0]);
						System.Console.WriteLine (vals[1]);
						currentcolor = mtl[vals[1]];
						System.Console.WriteLine (vals[1]);
						System.Console.WriteLine (currentcolor.ToString());
					}



	

			}
			//System.Console.Write ("FUCKYOU");
			//System.Console.Write (vertexCount);
			//System.Console.Write (vertexCount);


			return new PixelModelData(vertices,faces);
		}
	}
}


