using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;

namespace Cruise.Storage
{
	public class StorageModel : IStorageModel
	{
		private const StringComparison Ignore = StringComparison.InvariantCultureIgnoreCase;

		private readonly List<IServerDetails> _servers;
		private readonly Colours _colours;

		public StorageModel(StorageModelMemento memento)
		{
			_servers = memento
				.Servers
				.Select(pair => new ServerDetails(pair.Key, pair.Value))
				.Cast<IServerDetails>()
				.ToList();

			_colours = memento.Colours;
		}

		public  IEnumerable<IServerDetails> Servers
		{
			get { return _servers; }
		}

		public bool IsRegistered(string serverName)
		{
			return _servers.Any(server => server.Name.Equals(serverName, Ignore));
		}

		public void Register(string serverName, Uri serverUrl)
		{
			if (IsRegistered(serverName))
			{
				throw new ServerAlreadyRegisteredException(serverName, serverUrl);
			}

			_servers.Add(new ServerDetails( serverName, serverUrl));
		}

		public void UnRegister(string serverName)
		{
			_servers.RemoveAll(server => server.Name.Equals(serverName, Ignore));
		}

		public IServerDetails GetServerByName(string serverName)
		{
			return _servers.FirstOrDefault(s => s.Name.EqualsIgnoreCase(serverName));
		}

		public StorageModelMemento ToMemento()
		{
			var memento = new StorageModelMemento
			{
				Servers = Servers.ToDictionary(server => server.Name, server => server.Url),
				Colours = _colours,
			};

			return memento;
		}
	}
}
