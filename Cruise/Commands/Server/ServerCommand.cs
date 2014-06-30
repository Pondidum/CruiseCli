using System;
using System.Collections.Generic;
using Cruise.Infrastructure;
using Cruise.Storage;
using FubuCore.CommandLine;

namespace Cruise.Commands.Server
{
	public class ServerCommand : FubuCommand<ServerInputModel>
	{
		private readonly ISaveStorageModelCommand _saveCommand;
		private readonly IStorageModel _storage;
		private readonly IResponse _response;

		public ServerCommand(ISaveStorageModelCommand saveCommand, IStorageModel storage, IResponse response)
		{
			_saveCommand = saveCommand;
			_storage = storage;
			_response = response;
			
			Usage("Lists all registered servers")
				.Arguments()
				.ValidFlags();

			Usage("Adds a server.")
				.Arguments(a => a.Name, a => a.Url)
				.ValidFlags(a => a.AddFlag);

			Usage("Removes a server.")
				.Arguments(a => a.Name)
				.ValidFlags(a => a.RemoveFlag);
		}

		public override bool Execute(ServerInputModel input)
		{
			if (input.AddFlag)
			{
				if (_storage.IsRegistered(input.Name))
				{
					_response.Write("Server {0} ({1}) is already registered.", input.Name, input.Url);
					return false;
				}

				_storage.Register(input.Name, new Uri(input.Url));
				_saveCommand.Execute(_storage);
			}
			else if (input.RemoveFlag)
			{
				if (_storage.IsRegistered(input.Name))
				{
					_storage.UnRegister(input.Name);
					_saveCommand.Execute(_storage);
				}
			}
			else
			{
				_storage.Servers.Each(server => _response.Write("    {0}", server.Key));
			}

			return true;
		}
	}
}
