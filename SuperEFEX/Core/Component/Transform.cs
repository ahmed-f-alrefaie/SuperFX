using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace SuperEFEX.Core.Components
{
	[Serializable()]
	public class Transform : Component
	{
		//World transforms
		private Vector3 mPosition =new Vector3();
		private Vector3 mEulerAngle = new Vector3();
		private Vector3 mScale = Vector3.One;
		private Quaternion mQuaternion;
	

		//Local transforms
		private Vector3 mLocalPosition;
		private Vector3 mLocalEulerAngles;
		private Vector3 mLocalScale;
		private Quaternion mLocalQuaternion = Quaternion.Identity;
		private Matrix mLocalToWorld = Matrix.Identity;
		private Matrix World = Matrix.Identity;
		private Matrix mWorldToLocal = Matrix.Identity;

		int mSiblingIndex=0;

		public Vector3 Position{
			get{ return mPosition; }
			set{ 
					mPosition = value; 
			
			}
		}

		public Vector3 EulerAngles{
			get{ return mEulerAngle; }
			set{ mEulerAngle = value; 
				
			}
		}

		public Quaternion Quaternion {
			get {
				return mQuaternion;
			}
			set {
				mQuaternion = value;
			}
		}

		public Vector3 Scale {
			get {
				return mScale;
			}
			set {
				mScale = value;
			}
		}

		public Vector3 LocalPosition {
			get {
				return mLocalPosition;
			}
			set {
				mLocalPosition = value;
			}
		}


		public void SetY(float y){
			mPosition.Y = y;
		}
		public void SetZ(float y){
			mPosition.Z = y;
		}
		private List<Transform> children = new List<Transform>();

		private Transform parent = null;



		public Matrix LocalToWorldMatrix {
			get{
				return mLocalToWorld;
			}
		}

		public Matrix WorldToLocalMatrix {
			get{
				return mWorldToLocal;
			}
		}

		public override void RegisterComponent(GameObject owner){
			base.RegisterComponent (owner);
			if (owner.Parent != null)
				parent = owner.Parent.transform;


		}

		public override void Update(GameTime gameTime){
			UpdateMatrices ();

		}

		public Transform ():base()
		{
		}

		private void UpdateMatrices(){
			
			if (parent != null) {

				mPosition = Vector3.Transform( mLocalPosition,parent.mLocalToWorld);
				mQuaternion = Quaternion.CreateFromYawPitchRoll (mLocalEulerAngles.Y, mLocalEulerAngles.X, mLocalEulerAngles.Z);
				mQuaternion = parent.Quaternion * mQuaternion;

				mLocalToWorld = Matrix.CreateScale (mLocalScale) * Matrix.CreateFromQuaternion (mQuaternion) * Matrix.CreateTranslation (mLocalPosition);
				mLocalToWorld = parent.mLocalToWorld * mLocalToWorld;

			

			} else {
				mQuaternion = Quaternion.CreateFromYawPitchRoll (mEulerAngle.Y, mEulerAngle.X, mEulerAngle.Z);
				mLocalToWorld = Matrix.CreateScale (mScale) * Matrix.CreateFromQuaternion (mQuaternion) * Matrix.CreateTranslation (mPosition);
			}

			mWorldToLocal = Matrix.Invert (mLocalToWorld);

		}

	}
}

