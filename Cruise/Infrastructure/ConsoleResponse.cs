using System;

namespace Cruise.Infrastructure
{
	public class ConsoleResponse : IResponseWriter
	{
		public void Write(string format, params object[] args)
		{
			Console.WriteLine(format, args);
		}
	}
}
