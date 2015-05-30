using System;
using System.Collections.Generic;
using SuperEFEX.Core.Content;
using SuperEFEX.Core.Content.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperFXContent;
namespace SuperEFEX.Renderer
{
	public enum SpriteJustify{
		LEFT,
		CENTER
	}



	public class SFXSpriteFont : IRenderable
	{
		public bool Enable{get; set;}
		public RenderType RenderType{get{ return RenderType.SPRITE; }}
		public int Priority{ get; set; }
		string currentText;
		SpriteFontData spriteFontData;
		public string Filename{get; set;}
		public SpriteBatch batch;
		SpriteJustify justify;
		RawTexture texture;
		public Vector3 position;
		int textCount = 0;
		public float Alpha { get; set; }
		private float spriteHeight;
		private float avgSpriteWidth = 0.0f;
		public SFXSpriteFont ()
		{
			this.Enable = false;
			this.Alpha = 255.0f;
			RendererBase.RegisterMeToRenderer (this);
		}

		public void LoadContent(FXContent content){

			spriteFontData = content.Load<SpriteFontData> (Filename);
			texture = content.Load<RawTexture> (spriteFontData.textureFile);
			foreach (KeyValuePair<char,Rectangle> kvp in spriteFontData.font) {
				avgSpriteWidth += (float)kvp.Value.Width;
				spriteHeight = MathHelper.Max (kvp.Value.Height, spriteHeight);

			}
			spriteHeight *= 1.1f;
			avgSpriteWidth /= (float)spriteFontData.font.Count;


		}

		public void SetString(string text,SpriteJustify justify){
			this.justify = justify;
			this.currentText = text;
			textCount = currentText.Length;


		}

		public float ComputeLineLengths(int start){
			float lineLength = 0.0f;

			if (justify == SpriteJustify.LEFT)
				return 0;


			for (int i = start; i < currentText.Length; i++) {
				char letter = currentText [i];
				if (letter == '\n')
					break;
				if (letter == ' ') {
					lineLength += avgSpriteWidth;
					continue;
				}
				lineLength += spriteFontData.font [letter].Width;
			}

			return -lineLength/2.0f;
		}


		public void Draw(){

			if (currentText == null)
				return;
			if (currentText.Length == 0)
				return;

			float xposition_offset = ComputeLineLengths (0);
			float yposition_offset = 0.0f;

				for (int i = 0; i < textCount; i++) {
					char letter = currentText [i];
					if (letter == '\n') {
					xposition_offset = ComputeLineLengths (i+1);
						yposition_offset += spriteHeight;
						continue;
					}
					if (letter == ' ') {
						xposition_offset += avgSpriteWidth;
						continue;
					}

					Rectangle texCoord = spriteFontData.font [letter];//Get the texture coordinate
					batch.Draw (texture.OriginalTexture, new Vector2 (position.X + xposition_offset, position.Y + yposition_offset),texCoord, Color.White*Alpha, 0.0f,
						new Vector2(0,0),Vector2.One, SpriteEffects.None, position.Z);


					xposition_offset += (float)texCoord.Width;
				}



				
	



		}


	}
}

