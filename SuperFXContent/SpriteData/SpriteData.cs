using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace SuperFXContent
{

	public struct AnimFrameData{
		public int SpriteIndex;
		public float frameDelay; 
		public Point offset;
	}

	public struct FrameData{
		public Rectangle textureCoords;

	}


	public class SpriteData
	{
		public int Width,Height;
		public string textureFile;
		public Dictionary<string,List<AnimFrameData>> animationData = new Dictionary<string, List<AnimFrameData>>();
		public List<FrameData> frames = new List<FrameData>();

		public SpriteData ()
		{
		}




	}
}

