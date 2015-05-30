using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

using TInput = System.String;
using TOutput = System.String;

namespace SuperFXContent
{
	[ContentProcessor (DisplayName = "SpriteFontProcessor")]
	class SpriteFontContentProcessor : ContentProcessor<XDocument, SpriteFontData>
	{
		public override SpriteFontData Process (XDocument input, ContentProcessorContext context)
		{
			SpriteFontData sprite = new SpriteFontData ();
			System.Console.WriteLine ("HELLO");
			//Reade the image attribute
			sprite.textureFile = Path.GetFileNameWithoutExtension(input.Document.Element("img").Attribute("name").Value);


			System.Console.WriteLine (sprite.textureFile);
			sprite.Width = Int32.Parse(input.Document.Element("img").Attribute("w").Value);
			sprite.Height = Int32.Parse(input.Document.Element("img").Attribute("h").Value);
			System.Console.WriteLine (sprite.Width.ToString());
			//Now lets get the sprite Frames;
			foreach (XElement elm in input.Document.Element("img").Element("definitions").Element("dir").Elements()) {
				//Read Frame data;
				if(elm.Name !="spr")
					continue;
		
				Rectangle textureCoords = new Rectangle ();
				string letter = elm.Attribute ("name").Value;
				textureCoords.X = Convert.ToInt32(elm.Attribute ("x").Value);
				textureCoords.Y = Convert.ToInt32(elm.Attribute ("y").Value);
				textureCoords.Width = Convert.ToInt32(elm.Attribute ("w").Value);
				textureCoords.Height = Convert.ToInt32(elm.Attribute ("h").Value);
				sprite.font.Add (letter [0], textureCoords);

			}




			return sprite;
		}
	}
}


