using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SuperEFEX.Core.Content;
using SuperEFEX.Core.Components;
using SuperEFEX.Renderer;
using SuperEFEX.Renderer.Particle;
namespace SuperEFEX.Core.Components
{
	public class ParticleEmitterComponent : Component
	{

		List<Particle> particles = new List<Particle>();

		//Properties
		public int NumParticles { get; set;}
		public float EmissionRate{ get; set; } //In milliseconds
		public string Animation{ get; set; }
		public string SpriteName{ get; set; }
		public bool UseWorldSpace{ get; set; }
		public float ParticleLife{get; set;}
		private int activeParticles = 0;
		public Vector3 PositionVar{ get; set; }
		public Vector3 Velocity{ get; set; }
		public Vector3 VelocityVar { get; set; }
		public float ActiveTime{ get; set; }
		Random rand;
		private float emissionFactor=0.0f;
		private float rate=0.0f;
		private float duration=0.0f;
		public ParticleEmitterComponent ()
		{
			rand = new Random ();
		}

		public override void LoadContent(FXContent content){
			//
			for (int i = 0; i < NumParticles; i++) {
				Particle part = new Particle ();
				particles.Add (part);
				part.FileName = SpriteName;
				part.LoadContent (content);


			}



		}

		public override void Update (GameTime gameTime)
		{
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
			activeParticles = 0;
			if (this.ActiveTime>0.0f) {
				duration += dt;



			}


			foreach (Particle p in particles) {
				if (!p.isDead ()) {
					activeParticles++;
					p.Update (gameTime);
				
				}
			}

			//Emit a new particle
			if (EmissionRate>0.0f) {
				rate = 1.0f / EmissionRate;
				emissionFactor += dt;
				if (emissionFactor > rate && activeParticles < NumParticles) {
					AddParticle ();
					emissionFactor -= rate;
				}

			}





			base.Update (gameTime);
		}

		private void AddParticle(){
			foreach (Particle p in particles) {
				if (p.isDead ()) {
					//Setup particle
					p.Wakeup (owner.transform.Position + PositionVar * (float)rand.NextDouble (),
						Velocity + VelocityVar * (float)rand.NextDouble (),
						Vector3.Zero, ParticleLife, Animation);
					break;
				}


			}



		}

	}
}

