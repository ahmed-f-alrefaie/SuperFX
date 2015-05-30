using System;
using Microsoft.Xna.Framework;
using SuperEFEX.Core.Content;
namespace SuperEFEX.Renderer.Particle
{
	public class Particle
	{

		Sprite sprite;

		public string FileName{ get; set; }

		public Vector3 position;
		public Vector3 velocity;
		public Vector3 acceleration;

		float lifespan=-1.0f;
		float originalLifespan=1.0f;
		float ageRatio=0.0f;
		public Particle ()
		{
		}


		public void LoadContent(FXContent content){
			sprite = new Sprite ();
			sprite.Filename = FileName;
			sprite.LoadContent (content);

			sprite.Enable = false;


		}


		public void Wakeup(Vector3 position,Vector3 velocity,Vector3 acceleration,float lifespan,string animation){
			this.position = position;
			this.velocity = velocity;
			this.acceleration = acceleration;
			sprite.position = position;
			sprite.PlayAnimation (animation,0);
			sprite.Enable = true;
			this.ageRatio = 1.0f;
			this.originalLifespan = lifespan;
			this.lifespan = lifespan;




		}

		public void Update(GameTime gametime){


			velocity += acceleration*(float)gametime.ElapsedGameTime.TotalSeconds;
			position += velocity*(float)gametime.ElapsedGameTime.TotalSeconds;
			sprite.position = position;
			lifespan -= (float)gametime.ElapsedGameTime.TotalSeconds;
			ageRatio = lifespan / originalLifespan;
			sprite.Update (gametime);

		}




		public bool isDead(){
			if (lifespan < 0.0f) {
				sprite.Enable = false;
				return true;
			} else {
				sprite.Enable = true;
				return false;
			}
		}
	}
}

