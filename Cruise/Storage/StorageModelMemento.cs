using System;
using System.Collections.Generic;

namespace Cruise.Storage
{
	public class StorageModelMemento
	{
		public Dictionary<string, Uri> Servers { get; set; }
		public Colours Colours { get; set; }

		public StorageModelMemento()
		{
			Servers = new Dictionary<string, Uri>();
			Colours = new DefaultColours();
		}
	}
}
