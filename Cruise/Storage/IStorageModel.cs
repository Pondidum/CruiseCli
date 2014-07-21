using System;
using System.Collections.Generic;
using Cruise.Transport;

namespace Cruise.Storage
{
	public interface IStorageModel
	{
		IEnumerable<IServerDetails> Servers { get; }
		Colors Colors { get; }

		bool IsRegistered(string serverName);
		void Register(string serverName, Uri serverUrl);
		void UnRegister(string serverName);

		IServerDetails GetServerByName(string serverName);

		ConsoleColor GetColorForProject(IProject project);

		StorageModelMemento ToMemento();
	}
}
