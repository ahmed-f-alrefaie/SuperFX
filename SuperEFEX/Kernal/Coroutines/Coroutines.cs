using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
namespace Kernal
{
	public class Coroutines
	{
		static List<IEnumerator> routines = new List<IEnumerator>();

		static public void Start(IEnumerator routine)
		{
			routines.Add(routine);
		}

		static public void StopAll()
		{
			routines.Clear();
		}

		static public void Update()
		{
			for (int i = 0; i < routines.Count; i++)
			{
				if (routines[i].Current is IEnumerator)
				if (MoveNext((IEnumerator)routines[i].Current))
					continue;
				if (!routines[i].MoveNext())
					routines.RemoveAt(i--);
			}
		}

		static bool MoveNext(IEnumerator routine)
		{
			if (routine.Current is IEnumerator)
			if (MoveNext((IEnumerator)routine.Current))
				return true;
			return routine.MoveNext();
		}

		static public int Count
		{
			get { return routines.Count; }
		}

		static public bool Running
		{
			get { return routines.Count > 0; }
		}
		static IEnumerator Pause(float time)
		{
			var watch = Stopwatch.StartNew();
			while (watch.Elapsed.TotalSeconds < time)
				yield return 0;
		}
	}
}

