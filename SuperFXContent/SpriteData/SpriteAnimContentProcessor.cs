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
	[ContentProcessor (DisplayName = "SpriteAnimProcessor")]
	class SpriteAnimContentProcessor : ContentProcessor<XDocument, SpriteData>
	{
		public override SpriteData Process (XDocument input, ContentProcessorContext context)
		{
			SpriteData sprite = new SpriteData ();
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
				FrameData frame = new FrameData ();
				frame.textureCoords = new Rectangle ();
				frame.textureCoords.X = Convert.ToInt32(elm.Attribute ("x").Value);
				frame.textureCoords.Y = Convert.ToInt32(elm.Attribute ("y").Value);
				frame.textureCoords.Width = Convert.ToInt32(elm.Attribute ("w").Value);
				frame.textureCoords.Height = Convert.ToInt32(elm.Attribute ("h").Value);
				sprite.frames.Add (frame);

				System.Console.WriteLine (frame.textureCoords.ToString ());


			}
			string filename = Path.GetFileNameWithoutExtension(input.Document.Root.Element("File").Attribute("name").Value);
			if (File.Exists (filename + ".anim")) {
				System.Console.WriteLine ("ANIMATION EXISTS!!");
				//Now lets check for animation data
				XDocument animationDoc = XDocument.Load (filename + ".anim");
				System.Console.WriteLine ("Loading naimations");
				foreach (XElement anims in animationDoc.Document.Element("animations").Elements()) {
					System.Console.WriteLine ("BEGIN");
					string AnimName = anims.Attribute ("name").Value.Trim();
					System.Console.WriteLine (AnimName);
					sprite.animationData.Add (AnimName, new List<AnimFrameData> ());
					foreach(XElement animCell in anims.Elements()){
						//Read animation frame data
						AnimFrameData animationFrame = new AnimFrameData ();
						animationFrame.frameDelay = Convert.ToSingle (animCell.Attribute ("delay").Value);
						animationFrame.SpriteIndex = Convert.ToInt32( (animCell.Element ("spr").Attribute("name").Value.Remove(0,1)));
						animationFrame.offset = new Point ();
						//Add offsets here
						sprite.animationData [AnimName].Add (animationFrame);
					}



				}
			}




			return sprite;
		}
	}
}


