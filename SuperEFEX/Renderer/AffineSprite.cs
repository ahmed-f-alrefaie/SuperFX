using System;
using Microsoft.Xna.Framework;
using SuperEFEX.Renderer;
using SuperEFEX.Core.Content.Graphics;
using SuperEFEX.Core.Content;
using SuperFXContent;
using System.Collections.Generic;
namespace SuperEFEX.Renderer
{
	public class Sprite : IRenderable
	{

		public Vector3 position = Vector3.Zero;
		public Vector3 origin = Vector3.Zero;
		public Vector2 scale = Vector2.One;
		public Rectangle texturepoint = new Rectangle();
		RawTexture mTexture;
		SpriteData spriteData;
		private int loops = -1;
		private int currentLoop = 0;
		private int currentFrame=0;
		private int totalFrameCount = 0;
		private int currentFrameCount = 0;
		private float frameTimer = 0.0f;
		private float currentFrameDelay = 0.0f;
		string currentAnimationName = "Run_RIGHT";
		Point frameoffset;
		string mFilename;
		bool animate = false;
		public ISpriteRenderer render{ get; set;}
		public byte Alpha = 255;
		public bool Enable{ get; set;}
		public RenderType RenderType{get{ return RenderType.SPRITE; }}
		public int Priority{ get; set;}
		public float Falpha{ get { return 255.0f / (float)Alpha; } }

		private float frameMulti = 1.0f;
		public float FrameMultiplier{
			get{ return frameMulti; }
			set{ frameMulti = value; }
		}

		public RawTexture Texture {
			get{ return mTexture; }
		}

		public string Filename {
			get{ return mFilename; }
			set{ mFilename = value; }
		}
		//SpriteData here



		public Sprite ()
		{
			render = new BatchSpriteRenderer ();
			RendererBase.RegisterMeToRenderer (this);
			//currentAnimation = spriteData.animationData [currentAnimationName];
		}

		private Rectangle GetTextureCoords(){
			return spriteData.frames [currentFrame].textureCoords;

		}


		public void LoadContent(FXContent content){
			spriteData = content.Load<SpriteData> (Filename);
			mTexture = content.Load<RawTexture> (spriteData.textureFile);
			currentFrame = 0;
			texturepoint.X = 0;
			texturepoint.Y = 0;
			texturepoint.Width = mTexture.Width;
			texturepoint.Height = mTexture.Height;

			foreach (KeyValuePair<string,List<AnimFrameData>> kvp in spriteData.animationData) {

				currentAnimationName = kvp.Key;
				animate = true;
				break;

			}
			UpdateAnimationData ();
		}


		public void PlayAnimation(string animationName,int loop){
			if (animationName == currentAnimationName)
				return;
			currentAnimationName = animationName;
			loops = loop;
			animate = true;
			currentLoop = 0;
			currentFrameCount = 0;
			frameTimer = 0.0f;
			UpdateAnimationData ();


		}

		public void UpdateAnimationData(){
			totalFrameCount = spriteData.animationData [currentAnimationName].Count;
			currentFrame = spriteData.animationData [currentAnimationName] [currentFrameCount].SpriteIndex;
			currentFrameDelay = spriteData.animationData [currentAnimationName] [currentFrameCount].frameDelay*10.0f;
			frameoffset = spriteData.animationData [currentAnimationName] [currentFrameCount].offset;

		}


		public void SetAlpha(float alpha){
			alpha = MathHelper.Clamp (alpha, 0.0f, 1.0f);
			Alpha = (byte)(alpha * 255.0f);



		}

		public void Update(GameTime gameTime){
			//Update for animations n shit;
			if (!animate)
				return;
			frameTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds*frameMulti;
			while (frameTimer > currentFrameDelay) {
				//if(frameTimer > spriteData.animationData[
				currentFrameCount++;
				if (currentFrameCount >= totalFrameCount) {
					if (loops == -1) {
						//Set the current frame to zero
						currentFrameCount = 0;

					} else {
						currentLoop++;
						if (currentLoop > loops) {
							animate = false;
							break;
						}

					}


				}
				UpdateAnimationData ();
				//Get the current frame
				frameTimer -= currentFrameDelay;
			}
			texturepoint = GetTextureCoords ();


		}

		public Color GetColor(int x, int y){

			Color color = mTexture.GetColor (texturepoint.Left + x, texturepoint.Top + y);
			if (color.A == 255) {
				color.A = Alpha;
			}
			return color;
		}

		public void Draw(){

			origin.X = (float)texturepoint.Width / 2 + frameoffset.X;
			origin.Y = (float)texturepoint.Height / 2 + frameoffset.Y;
			render.DrawSprite (this);
		}


	}
}

