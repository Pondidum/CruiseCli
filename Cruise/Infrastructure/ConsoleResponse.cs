using System;

namespace Cruise.Infrastructure
{
	public class ConsoleResponse : IResponseWriter
	{
		public void Write(object model)
		{
			Console.WriteLine(model.ToString());
		}
	}
}
