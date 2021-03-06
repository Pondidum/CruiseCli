﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cruise.Storage;
using Cruise.Transport;
using FubuCore;

namespace Tests
{
	public class FakeConfigurationModel : IConfigurationModel
	{
		private readonly List<IServerDetails> _servers;

		public FakeConfigurationModel()
		{
			_servers = new List<IServerDetails>();
			Colors = new DefaultColors();
		}

		public void Insert(IServerDetails server)
		{
			_servers.Add(server);
		}

		public IEnumerable<IServerDetails> Servers { get { return _servers;  } }
		public Colors Colors { get; private set; }

		public bool IsRegistered(string serverName)
		{
			return _servers.Any(server => server.Name.EqualsIgnoreCase(serverName));
		}

		public void Register(string serverName, Uri serverUrl)
		{
			_servers.Add(new ServerDetails(serverName, serverUrl));
		}

		public void UnRegister(string serverName)
		{
			_servers.RemoveAll(server => server.Name.EqualsIgnoreCase(serverName));
		}

		public IServerDetails GetServerByName(string serverName)
		{
			return _servers.FirstOrDefault(server => server.Name.EqualsIgnoreCase(serverName));
		}

		public ConsoleColor GetColorForProject(IProject project)
		{
			return Colors.Default;
		}

		public StorageModelMemento ToMemento()
		{
			throw new NotImplementedException();
		}
	}
}
