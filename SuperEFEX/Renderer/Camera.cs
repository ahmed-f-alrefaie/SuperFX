using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperEFEX.Renderer
{
	public class Camera
	{

		//We need our Matrices
		private Vector3 position;
		public static Camera MainCamera = null;
		private Vector3 rotation;
		private Matrix viewMatrix;
		private Matrix viewProjectionMatrix;
		private Matrix projectionMatrix;
		Vector3 referencePoint = new Vector3 (0, 0, 10);
		Vector3 newReference;
		Vector3 up = Vector3.Up;
		Matrix rotationMat;
		public Vector3 Forward {
			get{ return rotationMat.Forward; }
		}

		public Vector3  Up {
			get{ return rotationMat.Up; }
		}
		public Vector3  Left {
			get{ return rotationMat.Left; }
		}
		int mWidth;
		int mHeight;

		public int Height {
			get {
				return mHeight;
			}
		}

		public int Width {
			get {
				return mWidth;
			}
		}

		Vector3 normalDirection;
		public Vector3 Position {
			get {
				return position;
			}
			set {
				position = value;
			}
		}

		public Vector3 GetViewNormals{
			get{
				return normalDirection;
			}
		}

		public Vector3 Rotation {
			get {
				return rotation;
			}
			set {
				rotation = value;
			}
		}
		//Vectors for camera positioning


		public Matrix ViewProjectionMatrix {
			get {
				return viewProjectionMatrix;
			}
		}
			

		public Camera (int width,int height)
		{
			mWidth = width;
			mHeight = height;
			if (MainCamera == null)
				SetMainCamera ();
			ResetCamera ();

		}

		//Set the main camera
		public void SetMainCamera(){
			MainCamera = this;
		}

		public void Update()
		{
			UpdateViewMatrix();
			UpdateNormals ();

		}

		private void UpdateNormals(){
			normalDirection = newReference - position;
			normalDirection.Normalize ();


		}

		void ChangeProjection(int width,int height){
			mWidth = width;
			mHeight = height;
			ResetCamera ();
			Update ();
		}


		private void UpdateViewMatrix()
		{
			Quaternion rot = Quaternion.CreateFromYawPitchRoll (rotation.Y, rotation.X, rotation.Z);

			Vector3.Transform (ref referencePoint, ref rot, out newReference);
			//up = Vector3.Transform (Vector3.Up, Matrix.CreateRotationZ (Rotation.Z));
			newReference += position;

			rotationMat = Matrix.CreateFromQuaternion (rot);


			viewMatrix = Matrix.CreateLookAt(position,newReference,rotationMat.Up);
			viewProjectionMatrix = viewMatrix*projectionMatrix;
		}




		public void ResetCamera(){
			//position = Vector3.Zero;
			//position.Z = -10.0f;
			viewMatrix = Matrix.Identity;
			projectionMatrix = Matrix.CreatePerspectiveFieldOfView (MathHelper.ToRadians (45.0f), (float)mWidth/(float)mHeight, 0.1f, 200f);


		}

	}
}

