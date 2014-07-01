using System;
using System.Collections.Generic;

namespace Cruise.Storage
{
	public interface IStorageModel
	{
		IEnumerable<IServerDetails> Servers { get; }

		bool IsRegistered(string serverName);
		void Register(string serverName, Uri serverUrl);
		void UnRegister(string serverName);

		StorageModelMemento ToMemento();
	}
}
