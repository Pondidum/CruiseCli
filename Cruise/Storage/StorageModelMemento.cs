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

		public static StorageModelMemento FromModel(IStorageModel model)
		{
			var memento = new StorageModelMemento();

			memento.Servers = model.Servers.ToDictionary(p => p.Key, p => p.Value);

			return memento;
		}
	}
}
