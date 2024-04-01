using System;
using Monocle;

namespace Instrumentation
{
	public static class Debug
	{
		public static void Print(object test)
		{
			Console.WriteLine(test.ToString());
			ErrorLog.Write(new Exception(test.ToString()));
			try
			{
				ErrorLog.Open();
			}
			catch
			{
				Console.WriteLine("Failed to open the log!");
			}
		}
	}
}
