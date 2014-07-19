using System.Collections.Generic;
using Cruise.Infrastructure;

namespace Tests
{
	public class LogResponse : IResponseWriter
	{
		public IEnumerable<string> Log { get { return _log; }}

		private readonly List<string> _log;

		public LogResponse()
		{
			_log = new List<string>();
		}

		public void Write(object model)
		{
			_log.Add(model.ToString());
		}
	}
}
