using System.Collections.Generic;
using Cruise.Infrastructure;

namespace Tests
{
	public class LogResponse : IResponse
	{
		public List<string> Log { get; private set; }

		public LogResponse()
		{
			Log = new List<string>();
		}

		public void Write(string format, params object[] args)
		{
			Log.Add(string.Format(format, args));
		}
	}
}
