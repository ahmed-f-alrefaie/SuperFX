using System;
using System.IO;
namespace Kernal
{
	public class Logger : SingletonBase<Logger>
	{
		//Member variables
		StreamWriter logFile;
		public bool Init(){
			string filename = "../../../../../superfx_profile.txt";
			//Console.WriteLine ("superfx_" + System.DateTime.Now.ToString ("MM-dd-yyyy-h:mm-tt") + ".txt");
			logFile = new StreamWriter (filename);
			//
			logFile.AutoFlush=true;
			return logFile.BaseStream != null;

		}
		public void Write(string tag,string message){
			logFile.WriteLine (tag +" "+ message);

		}
		public void Write(string message){
			logFile.WriteLine (message);

		}
			
	}
}

