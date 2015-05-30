using System;
using SuperEFEX.Core.Components;
using SuperEFEX.Core;
using SuperEFEX.Kernal;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
namespace MonoGame.FF6Intro.Components
{
	public class IntroSceneScript : Component
	{
		public IntroSceneScript ()
		{
		}

		GameObject biggs;
		GameObject wedge;
		GameObject terra;


		IEnumerator IntroductionCutScene(){


			yield return Coroutines.Pause (1.0f);
			//Move all fellas to position
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{biggs.transform.SetY(x);},biggs.transform.Position.Y,130.0f,2.0f));
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{wedge.transform.SetY(x);},wedge.transform.Position.Y,160.0f,2.0f));
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{terra.transform.SetY(x);},terra.transform.Position.Y,187.0f,2.4f));

			yield return Coroutines.Pause (10.0f);

			//Text one

			introFont.SetText ("YOU WILL DIE BEFORE\nHUMANITY EXPLORES THE UNIVERSE", SuperEFEX.Renderer.SpriteJustify.CENTER);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},0.0f,1.0f,3.0f));
			yield return Coroutines.Pause (6.1f);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},1.0f,0.0f,3.0f));
			yield return Coroutines.Pause (3.1f);

			//Text2

			introFont.SetText ("YOU WERE AN ACCIDENT", SuperEFEX.Renderer.SpriteJustify.CENTER);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},0.0f,1.0f,3.0f));
			yield return Coroutines.Pause (6.1f);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},1.0f,0.0f,3.0f));
			yield return Coroutines.Pause (3.1f);

			//

			introFont.SetText ("THERE IS NO HEAVEN", SuperEFEX.Renderer.SpriteJustify.CENTER);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},0.0f,1.0f,3.0f));
			yield return Coroutines.Pause (6.1f);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},1.0f,0.0f,3.0f));
			yield return Coroutines.Pause (3.1f);

			introFont.SetText ("YOU ARE NOT SUGOI", SuperEFEX.Renderer.SpriteJustify.CENTER);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},0.0f,1.0f,3.0f));
			yield return Coroutines.Pause (6.1f);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},1.0f,0.0f,3.0f));
			yield return Coroutines.Pause (3.1f);

			introFont.SetText ("ANIME IS CARTOONS", SuperEFEX.Renderer.SpriteJustify.CENTER);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},0.0f,1.0f,3.0f));
			yield return Coroutines.Pause (6.1f);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},1.0f,0.0f,3.0f));
			yield return Coroutines.Pause (3.1f);

			introFont.SetText ("YOU WILL BE FORGOTTEN", SuperEFEX.Renderer.SpriteJustify.CENTER);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},0.0f,1.0f,3.0f));
			yield return Coroutines.Pause (6.1f);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},1.0f,0.0f,3.0f));
			yield return Coroutines.Pause (3.1f);

			introFont.SetText ("FFXV IS GOING TO BE SHIT\nDONT KID YOURSELF", SuperEFEX.Renderer.SpriteJustify.CENTER);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},0.0f,1.0f,3.0f));
			yield return Coroutines.Pause (6.1f);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},1.0f,0.0f,3.0f));
			yield return Coroutines.Pause (3.1f);

			introFont.SetText ("THAT GAME YOU ALWAYS WANTED TO MAKE\nWILL NEVER HAPPEN", SuperEFEX.Renderer.SpriteJustify.CENTER);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},0.0f,1.0f,3.0f));
			yield return Coroutines.Pause (6.1f);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},1.0f,0.0f,3.0f));
			yield return Coroutines.Pause (3.1f);

			introFont.SetText ("YOUR IDEAS ARE SHIT", SuperEFEX.Renderer.SpriteJustify.CENTER);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},0.0f,1.0f,3.0f));
			yield return Coroutines.Pause (6.1f);
			Coroutines.Start(Coroutines.SmoothInterpolate((x)=>{introFont.Alpha=x;},1.0f,0.0f,3.0f));
			yield return Coroutines.Pause (3.1f);


		}


		SpriteFontComponent introFont;


		public override void Initialize ()
		{
			biggs = GameObject.FindGameObject ("Biggs_Actor");
			wedge = GameObject.FindGameObject ("Wedge_Actor");
			terra = GameObject.FindGameObject ("Terra_Actor");
			introFont = GameObject.FindGameObject ("Text_Actor").GetComponent<SpriteFontComponent> ();
			Coroutines.Start (IntroductionCutScene());
			base.Initialize ();
		}

		float Z=0.0f;
		public override void Update (Microsoft.Xna.Framework.GameTime gameTime)
		{

			Z += (float)gameTime.ElapsedGameTime.TotalSeconds*10.0f;
			owner.transform.SetZ (Z);
			base.Update (gameTime);
		}

	}
}

