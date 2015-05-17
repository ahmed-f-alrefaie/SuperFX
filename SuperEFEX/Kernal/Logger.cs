using System;
using System.IO;
namespace SuperEFEX.Kernal
{
	public class Logger : SingletonBase<Logger>
	{
		//Member variables
		StreamWriter logFile;
		public bool Init(string filename,string path){
			string fullName = Path.Combine (path, filename);
			//Console.WriteLine ("superfx_" + System.DateTime.Now.ToString ("MM-dd-yyyy-h:mm-tt") + ".txt");
			logFile = new StreamWriter(fullName);
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

