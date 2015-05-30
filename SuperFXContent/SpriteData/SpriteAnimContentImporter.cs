using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;


using TImport = System.String;

namespace SuperFXContent
{
	[ContentImporter (".sprites", DisplayName = "SpriteAnimImporter", DefaultProcessor = "SpriteAnimProcessor")]
	public class SpriteAnimContentImporter : ContentImporter<XDocument>
	{
		public override XDocument Import (string filename, ContentImporterContext context)
		{
			XDocument doc = XDocument.Load(filename);
			System.Console.WriteLine ("BUTTS"+filename);
			doc.Document.Root.Add(new XElement("File", 
				new XAttribute("name", Path.GetFileName(filename)),
				new XAttribute("path", Path.GetDirectoryName(filename))));


			return doc;
		}
	}
}


