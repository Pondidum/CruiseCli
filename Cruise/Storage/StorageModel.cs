using System;
using System.Collections.Generic;
using System.Linq;

namespace Cruise.Storage
{
	public class StorageModel : IStorageModel
	{
		private readonly Dictionary<string, Uri> _servers;

		public StorageModel(StorageModelMemento memento)
		{
			_servers = memento.Servers.ToDictionary(
				m => m.Key,
				m => m.Value,
				StringComparer.InvariantCultureIgnoreCase);
		}

		public  IEnumerable<KeyValuePair<string, Uri>> Servers
		{
			get { return _servers; }
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

		public void UnRegister(string serverName)
		{
			_servers.Remove(serverName);
		}
	}
}
