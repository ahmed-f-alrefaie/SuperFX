using System;
using System.Text;
namespace Kernal
{
	public class ProfilerLogger : IProfilerOutputer
	{
		public void BeginOutput(){
			Logger.Instance.Write("Min :   Avg :   Max :   # : Profile Name\n--------------------------------------------");
		}
		public ProfilerLogger ()
		{
		}

		public void Sample(float fMin,float fMax,float fAvg,int callCount,string name,int parentCount){
			//
			StringBuilder output = new StringBuilder ();

			for (int indent = 0; indent < parentCount; indent++)
				output.Append (" ");
			
			output.Append(fMin.ToString()+" "+fAvg.ToString()+" "+fMax.ToString()+" "+callCount.ToString()+" "+name);
			Logger.Instance.Write (output.ToString ());
		    
		}
		public void EndOutput(){
			Logger.Instance.Write("\n");
		}
	}
}

