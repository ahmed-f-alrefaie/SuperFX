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


			yield return 0;



		}





		public override void Initialize ()
		{
			biggs = GameObject.FindGameObject ("Biggs_Actor");
			wedge = GameObject.FindGameObject ("Wedge_Actor");
			terra = GameObject.FindGameObject ("Terra_Actor");
			Coroutines.Start (IntroductionCutScene());
			base.Initialize ();
		}
	}
}

