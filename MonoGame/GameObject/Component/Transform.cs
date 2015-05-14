using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace Core.Components
{
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
		private Quaternion mLocalQuaternion;
		private Matrix mLocalToWorld;
		private Matrix World;
		private Matrix mWorldToLocal;

		int mSiblingIndex=0;

		public Vector3 Position{
			get{ return mPosition; }
			set{ mPosition = value; }
		}

		public Vector3 EulerAngles{
			get{ return mEulerAngle; }
			set{ mEulerAngle = value; 
				mQuaternion = Quaternion.CreateFromYawPitchRoll (mEulerAngle.Y, mEulerAngle.X, mEulerAngle.Z);
			}
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
				parent = owner.Parent.Transform;


		}

		public override void Update(GameTime gameTime){
			UpdateMatrices ();

		}

		public Transform ():base()
		{
		}

		private void UpdateMatrices(){
			if (parent != null) {
				//mLocalToWorld = Matrix.CreateScale (mLocalScale) * Matrix.CreateFromQuaternion (mLocalRotation) * Matrix.CreateTranslation (mLocalPosition);
				//mLocalToWorld = parent.mLocalToWorld * mLocalToWorld;
			} else {
				mLocalToWorld = Matrix.CreateScale (mScale) * Matrix.CreateFromQuaternion (mQuaternion) * Matrix.CreateTranslation (mPosition);

			}

		}

	}
}

