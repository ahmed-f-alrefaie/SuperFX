using System;
using SuperEFEX.Renderer;
namespace SuperEFEX.Core.Components
{
	public class CameraComponent : Component
	{
		//Camera class
		Camera camera = new Camera(100,200);

		public int Width{ get; set; }

		public int Height{ get; set; }

		public float FOV{ get; set; }

		public float NearPlane{ get; set; }

		public float FarPlane{ get; set; }




		public CameraComponent () : base()
		{
		
		}

		public override void Initialize ()
		{
			camera.SetCamera (Width, Height, FOV, NearPlane, FarPlane);

			base.Initialize ();
		}

		public override void Update (Microsoft.Xna.Framework.GameTime gameTime)
		{

			camera.Position = owner.transform.Position;
			camera.Rotation = owner.transform.EulerAngles;

			base.Update (gameTime);
		}

		public void SetAsMain(){
			camera.SetMainCamera ();
		}

	}
}

