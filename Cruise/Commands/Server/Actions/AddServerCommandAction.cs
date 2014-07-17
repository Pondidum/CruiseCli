using System;
using Cruise.Infrastructure;
using Cruise.Storage;

namespace Cruise.Commands.Server
{
	public class AddServerCommandAction : IServerCommandAction
	{
		private readonly ISaveStorageModelCommand _saveCommand;
		private readonly IStorageModel _storage;
		private readonly IResponseWriter _response;

		public AddServerCommandAction(ISaveStorageModelCommand saveCommand, IStorageModel storage, IResponseWriter response)
		{
			_saveCommand = saveCommand;
			_storage = storage;
			_response = response;
		}

		public bool CanHandle(ServerInputModel input)
		{
			return input.AddFlag;
		}

		public bool Execute(ServerInputModel input)
		{
			if (_storage.IsRegistered(input.Name))
			{
				_response.Write("Server {0} ({1}) is already registered.", input.Name, input.Url);
				return false;
			}

			_storage.Register(input.Name, new Uri(input.Url));
			_saveCommand.Execute(_storage);

			return true;
		}
	}
}