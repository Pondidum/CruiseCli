using System;
using System.Collections.Generic;
using System.Linq;

namespace Cruise.Storage
{
	public class StorageModelMemento
	{
		public Dictionary<string, Uri> Servers { get; set; }

		public StorageModelMemento()
		{
			Servers = new Dictionary<string, Uri>();
		}
	}
}
