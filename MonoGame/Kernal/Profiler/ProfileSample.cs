using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
namespace Kernal
{
	public class ProfileSampler 
	{
		public static void StartTimer(string SampleName)
		{
			int storeIndex = -1;
			int sampleLength = samples.Count;
			profileSample trySample;
			if (samples.TryGetValue (SampleName, out trySample)) {
				Debug.Assert (trySample.bIsOpen == true, "Tried to profile a sample " + SampleName + " which is already open!\n");
				trySample.parentSample = lastOpenedSample;
				lastOpenedSample = trySample;
				trySample.parentCount = openSampleCount;
				++openSampleCount;
				trySample.bIsOpen = true;
				++trySample.callCount;
				trySample.startTime = GetTime ();
				if (trySample.parentSample == null)
					rootBegin = trySample.startTime;

			} else {
				samples.Add (SampleName, new profileSample ());
				trySample = samples [SampleName];
				trySample.bIsValid = true;
				trySample.name = SampleName;
				trySample.iSampleIndex = storeIndex;
				trySample.parentSample = lastOpenedSample;
				lastOpenedSample = trySample;
				trySample.parentCount = openSampleCount;
				openSampleCount++;
				trySample.bIsOpen = true;
				trySample.callCount = 1;

				trySample.totalTime = 0.0f;
				trySample.childTime = 0.0f;
				trySample.startTime = GetTime ();
				if (trySample.parentSample == null)
					rootBegin = trySample.startTime;

			}

		}
		static public void Output(){
			
			Debug.Assert (outputer != null, "Profiler has no way of outputting\n");

			outputer.BeginOutput ();

			float totalTime = 0.0f;
			foreach(var i in samples) {
				totalTime += i.Value.totalTime - i.Value.childTime;
			}


			int sampleCount = samples.Count;
			foreach(var i in samples) {
				float sampletime, percentage;
				sampletime = i.Value.totalTime - i.Value.childTime;

				percentage = (sampletime /totalTime)*100.0f;
				float totalPc = i.Value.averagePc * i.Value.dataCount;
				totalPc += percentage;
				i.Value.dataCount++;
				i.Value.averagePc = totalPc / i.Value.dataCount;
				if ((i.Value.minPc == -1) || (percentage < i.Value.minPc))
					i.Value.minPc = percentage;
				if ((i.Value.maxPc == -1) || (percentage > i.Value.maxPc))
					i.Value.maxPc = percentage;

				outputer.Sample (i.Value.minPc, i.Value.maxPc, i.Value.averagePc, (int)i.Value.callCount,
					i.Value.name, i.Value.parentCount);

				i.Value.callCount = 0;
				i.Value.totalTime = 0;
				i.Value.childTime = 0;
				
			}
			outputer.EndOutput ();


		

		}
		static public void ResetSample(string SampleName){ throw new NotImplementedException ();}
		static public void ResetAll(){ throw new NotImplementedException ();}
		static public IProfilerOutputer outputer=null;




		protected static long GetTime(){return DateTime.Now.Ticks;}

		protected class profileSample{
			public profileSample()
			{
				dataCount=0;
				averagePc=maxPc=-1;
				minPc=-1;
			}
			public string ParentIdx;
			public bool bIsValid;    //whether or not this sample is valid to be used
			public bool bIsOpen; 	//is this sample currently being profiled?
			public long callCount; //number of times this sample has been executed
			public string name; //name of the sample
			public int iSampleIndex;
			public profileSample parentSample;
			public long startTime;  //starting time on the clock, in seconds
			public float totalTime;  //total time recorded across all executions of this sample
			public float childTime;  //total time taken by children of this sample

			public int parentCount;  //number of parents this sample has
			//(useful for neat indenting)

			public float averagePc;  //average percentage of game loop time taken up
			public float minPc;      //minimum percentage of game loop time taken up
			public float maxPc;      //maximum percentage of game loop time taken up
			public long dataCount; //number of times values have been stored since
		};
		static protected Dictionary<string,profileSample> samples = new Dictionary<string,profileSample>();
		static protected profileSample lastOpenedSample=null;
		static protected int openSampleCount=0;
		static protected long rootBegin, rootEnd;


		public static void StopTimer(string name){
			int storeIndex = -1;
			int sampleLength = samples.Count;
			profileSample trySample;
			if (samples.TryGetValue (name, out trySample)) {
				
				if (!trySample.bIsOpen)
					return;
				long fEndTime = GetTime ();
				trySample.bIsOpen = false;
				float fTimeTaken = (float)(fEndTime - trySample.startTime) / (float)TimeSpan.TicksPerSecond;
				if (trySample.parentSample != null) {
					trySample.parentSample.childTime += fTimeTaken;
				} else {
					rootEnd = fEndTime;
				}
				trySample.totalTime += fTimeTaken;
				lastOpenedSample = trySample.parentSample;
				--openSampleCount;
			}


		}
	}
}

