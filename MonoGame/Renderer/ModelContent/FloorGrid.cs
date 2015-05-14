using System;

namespace Renderer.ModelContent
{
	public class FloorGrid //: Renderable3D,IWorld
	{
		EdgeTable[] floor = new EdgeTable[100];
		public EdgeTable[] EdgeTables {
			get {
				return floor;
			}
		}
		public FloorGrid ()
		{
		}
	}
}

