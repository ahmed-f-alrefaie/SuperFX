﻿using System;
using Microsoft.Xna.Framework;
namespace Core.Components
{
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

		public virtual void LoadContent(){
		}

		public virtual void Update (GameTime gameTime){
		}

		public virtual void FixedUpdate (GameTime gameTime){
		}

		public virtual void Destroy(){
		}

	}
}

