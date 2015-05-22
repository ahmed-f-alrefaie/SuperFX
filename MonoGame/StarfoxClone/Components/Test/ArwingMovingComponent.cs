using System;
using SuperEFEX.Core.Components;
using Microsoft.Xna.Framework;
using SuperEFEX.Kernal;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
namespace StarfoxClone.Components.Test
{
	[Serializable]
	public class ArwingMovingComponent : Component
	{
		private PixelModelComponent pixelModel;
		private float flashTimer = 0.0f;
		private float flashSpeed = 10.0f;

		public float FlashSpeed {
			get {
				return flashSpeed;
			}
			set {
				flashSpeed = value;
			}
		}
			

		IEnumerator DoPositionChange(){

			yield return Coroutines.Pause (10.0f);
			//Now we do a lerp
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{position.X = x;},owner.transform.Position.X,-50.0f,3.0f));
			yield return Coroutines.Pause (5.0f);

		}


		private float movingTimer = 0.0f;

		Vector3 position = Vector3.Zero;
		Vector3 rotation = Vector3.Zero;
		public ArwingMovingComponent ()
		{
		}

		public override void Initialize ()
		{

			pixelModel = owner.GetComponent<PixelModelComponent> ();
			position = owner.transform.Position;
			Coroutines.Start (DoPositionChange());
			base.Initialize ();
		}

		public override void Update (GameTime gameTime)
		{
			pixelModel.SetLighting (3, false);
			pixelModel.SetLighting (37, false);
			pixelModel.SetLighting (24, false);

			
			rotation = owner.transform.EulerAngles;

			//Flashing
			flashTimer += flashSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (flashTimer > 1.0f || flashTimer < 0.0f) {
				flashSpeed *= -1.0f;
			}
			pixelModel.SetColor(3,Color.Lerp (Color.Red, Color.Yellow,flashTimer));
			pixelModel.SetColor(37,Color.Lerp (Color.Blue, Color.LightBlue,flashTimer));
			pixelModel.SetColor(24,Color.Lerp (Color.Blue, Color.LightBlue,flashTimer));

			movingTimer += 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

			//Moving action
			position.Z = 80.0f + ((float)Math.Sin ((float)movingTimer) * 30.0f);

			rotation.Z = 1.0f * ((float)Math.Sin ((float)movingTimer));
			rotation.Y -= 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;


			owner.transform.Position = new Vector3(position.X,owner.transform.Position.Y,position.Z);
			owner.transform.EulerAngles = rotation;

			base.Update (gameTime);
		}

	}
}

