using System;
using System.Collections.Generic;
using System.Linq;
using Cruise.Transport;
using FubuCore;
using FubuCore.Util;

namespace Cruise.Storage
{
	public class StorageModel : IStorageModel
	{
		private const StringComparison Ignore = StringComparison.InvariantCultureIgnoreCase;

		public Colors Colors { get; private set; }
		public IEnumerable<IServerDetails> Servers { get { return _servers; } }

		private readonly List<IServerDetails> _servers;
		
		public StorageModel(StorageModelMemento memento)
		{
			_servers = memento
				.Servers
				.Select(pair => new ServerDetails(pair.Key, pair.Value))
				.Cast<IServerDetails>()
				.ToList();

			Colors = memento.Colors;
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

		public ConsoleColor GetColorForProject(IProject project)
		{
			var map = new Cache<String, ConsoleColor>();
			map.OnMissing = key => Colors.Default;

			return map[project.Status];
		}

		public StorageModelMemento ToMemento()
		{
			var memento = new StorageModelMemento
			{
				Servers = Servers.ToDictionary(server => server.Name, server => server.Url),
				Colors = Colors,
			};

			return memento;
		}
	}
}
