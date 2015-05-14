using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.IO;

using TImport = System.String;

namespace SuperFXContent
{
	[ContentImporter (".obj", DisplayName = "WaveFront OBJ Importer", DefaultProcessor = "ObjProcessor")]
	public class OBJImporter : ContentImporter<OBJData>
	{
		public override OBJData Import (string filename, ContentImporterContext context)
		{
			//Lets start reading the file
			string fileData = System.IO.File.ReadAllText(filename);
			return new OBJData (fileData);
		}
	}
}


