using System;
using System.Collections.Generic;
using System.Linq;

namespace Cruise.Storage
{
	public class StorageModel
	{
		private readonly Dictionary<string, Uri> _servers;

		public StorageModel(StorageController.StorageModelMemento memento)
		{
			_servers = memento.Servers.ToDictionary(
				m => m.Key,
				m => m.Value,
				StringComparer.InvariantCultureIgnoreCase);
		}

		public bool IsRegistered(string serverName)
		{
			return _servers.ContainsKey(serverName);
		}

		public void Register(string serverName, Uri serverUrl)
		{
			if (IsRegistered(serverName))
			{
				throw new ServerAlreadyRegisteredException(serverName, serverUrl);
			}

			_servers.Add(serverName, serverUrl);
		}
	}
}
