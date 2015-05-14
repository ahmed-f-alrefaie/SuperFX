using System;
using System.IO;
namespace SuperFXContent
{


	public class OBJData
	{
		private string rawOBJData;
		public string RawObjData { get { return rawOBJData; } }

		private string rawMtlData;
		public string RawMtlData { get { return rawMtlData; } }

		private bool hasMaterial;
		public bool HasMaterial{ get { return hasMaterial; } }

		private int numFaces;
		private int numVertices;
		private int numMaterials;
		public int NumFaces {
			get {
				return numFaces;
			}
		}

		public int NumVertices {
			get {
				return numVertices;
			}
		}

		public int NumMaterials {
			get {
				return numMaterials;
			}
		}

		public OBJData (string OBJFile):
		this(OBJFile, null){
		}


		public OBJData (string OBJFile,string MTLFile)
		{
			this.rawOBJData = OBJFile;
			StringReader reader = new StringReader (this.rawOBJData);
			string line;
			while ((line = reader.ReadLine())!= null) {



				//Read the first letter
				line = line.Trim();
				if (line.Length < 2)
					continue;




				if (line [0] == 'v' && line [1] == ' ')
					numVertices++;
				else if (line [0] == 'f' && line [1] == ' ')
					numFaces++;
				else {
					string[] splitLine = line.Split (new[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries);
					if (splitLine [0] == "mtllib") {
						rawMtlData = System.IO.File.ReadAllText (splitLine [1]);
						System.Console.Write ("GOT MATERIAL");
						hasMaterial = true;
					}
				}


			}








		}


	}
}

