using System;

namespace SuperEFEX.Renderer
{
	public class PixelSpriteRenderer : ISpriteRenderer
	{
		PixelPlotter plotter;


		public PixelSpriteRenderer (PixelPlotter plot)
		{
			plotter = plot;
		}

		public void DrawSprite(Sprite sprite){
			for (int y = 0; y < sprite.texturepoint.Height; y++) {
				for (int x = 0; x < sprite.texturepoint.Width; x++) {
					plotter.PlotPixel (x + (int)(sprite.position.X - sprite.origin.X), y + (int)(sprite.position.Y - sprite.origin.Y), sprite.GetColor (x, y));

				}
			}
		}


	}
}

