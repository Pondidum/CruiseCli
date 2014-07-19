using System.Collections.Generic;
using System.Linq;
using Cruise.Infrastructure;

namespace Tests
{
	public class LogResponse : IResponseWriter
	{
		public object LastModel { get { return _models.Last();  } }
		public IEnumerable<object> Models { get { return _models; } }

		private readonly List<object> _models;

		public LogResponse()
		{
			_models = new List<object>();
		}

		public T Last<T>()
		{
			return _models.OfType<T>().Last();
		}

		public void Write(object model)
		{
			_models.Add(model);
		}
	}
}
