using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace SuperEFEX.Core.Components
{
	public class FirstPersonComponent : Component
	{
		public FirstPersonComponent () : base()
		{
		}

		public float RotationSpeed{ get; set; }
		public float ForwardSpeed {get; set;}

		Vector3 rotation = Vector3.Zero;
		Vector3 position = Vector3.Zero;

		public override void Initialize ()
		{
			position = owner.transform.Position;
			base.Initialize ();
		}


		public override void Update (GameTime gameTime)
		{
			KeyboardState keyboardState = Keyboard.GetState();
			//GamePadState currentState = GamePad.GetState( PlayerIndex.One );

			if (keyboardState.IsKeyDown( Keys.Left ) )
			{
				// Rotate left.
				rotation = rotation + new Vector3(0, RotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds,0);
			}
			if (keyboardState.IsKeyDown( Keys.Right ) )
			{
				// Rotate right.
				rotation = rotation - new Vector3(0, RotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds,0);
			}
			if (keyboardState.IsKeyDown( Keys.Up ) )
			{
				Matrix forwardMovement = Matrix.CreateRotationY( rotation.Y );
				Vector3 v = new Vector3( 0, 0, ForwardSpeed );
				v = Vector3.Transform( v, forwardMovement );
				position = position + new Vector3(v.X* (float)gameTime.ElapsedGameTime.TotalSeconds,0,v.Z* (float)gameTime.ElapsedGameTime.TotalSeconds);//* (float)gameTime.ElapsedGameTime.TotalSeconds;
				//cam.Position.X += v.X;
			}
			if (keyboardState.IsKeyDown( Keys.Down ) )
			{
				Matrix forwardMovement = Matrix.CreateRotationY( rotation.Y );
				Vector3 v = new Vector3( 0, 0, -ForwardSpeed );
				v = Vector3.Transform( v, forwardMovement );
				position = position + new Vector3 (v.X* (float)gameTime.ElapsedGameTime.TotalSeconds, 0, v.Z* (float)gameTime.ElapsedGameTime.TotalSeconds);//;
				//cam.Position.X += v.X;
			}

			if (keyboardState.IsKeyDown (Keys.Q)) {

				rotation = new Vector3 (rotation.X, rotation.Y, MathHelper.Lerp (rotation.Z, MathHelper.ToRadians (-10.0f), 0.05f));
			} else if (keyboardState.IsKeyDown (Keys.W)) {
				rotation = new Vector3 (rotation.X, rotation.Y, MathHelper.Lerp (rotation.Z, MathHelper.ToRadians (10.0f), 0.05f));

			} else {
				rotation = new Vector3 (rotation.X, rotation.Y, MathHelper.Lerp (rotation.Z, MathHelper.ToRadians (0.0f), 0.05f));

			}

			if (keyboardState.IsKeyDown (Keys.E)) {
				rotation = rotation + new Vector3 (-0.001f, 0, 0);
			} else if (keyboardState.IsKeyDown (Keys.D)) {
				rotation = rotation + new Vector3 (0.001f, 0, 0);
			}

			owner.transform.Position = position;
			owner.transform.EulerAngles = rotation;


			base.Update (gameTime);
		}



	}
}

