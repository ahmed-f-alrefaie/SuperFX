using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SuperEFEX.Core.Content;
namespace SuperEFEX.Core.Components
{
	[Serializable]
	public abstract class Component
	{

		public bool Enabled = true;
		protected GameObject owner;
		public Component ()
		{
		}




		public virtual void RegisterComponent(GameObject owner){
			this.owner = owner; 
		}

		public virtual void Initialize (){
		}

		public virtual void LoadContent(FXContent content){
		}

		public virtual void Update (GameTime gameTime){
		}

		public virtual void FixedUpdate (GameTime gameTime){
		}

		public virtual void Destroy(){
		}

		public Component Clone(){
			return (Component)this.MemberwiseClone ();
		}

	}
}

