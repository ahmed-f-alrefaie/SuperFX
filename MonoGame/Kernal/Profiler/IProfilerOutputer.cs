using System;

namespace Kernal
{
	public interface IProfilerOutputer
	{
		void BeginOutput();
		void EndOutput();
		void Sample(float fMin,float fMax,float fAvg,int callCount,string name,int parentCount);
	}
}

