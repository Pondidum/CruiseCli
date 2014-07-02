﻿using System.Collections.Generic;
using Cruise.Infrastructure;

namespace Tests
{
	public class LogResponse : IResponse
	{
		public IEnumerable<string> Log { get { return _log; }}

		private readonly List<string> _log;

		public LogResponse()
		{
			_log = new List<string>();
		}

		public void Write(string format, params object[] args)
		{
			_log.Add(string.Format(format, args));
		}
	}
}