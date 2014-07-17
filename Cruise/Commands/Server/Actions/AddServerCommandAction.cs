using System;
using Cruise.Infrastructure;
using Cruise.Storage;

namespace Cruise.Commands.Server
{
	public class AddServerCommandAction : IServerCommandAction
	{
		private readonly ISaveStorageModelCommand _saveCommand;
		private readonly IStorageModel _storage;
		private readonly IResponseWriter _writer;

		public AddServerCommandAction(ISaveStorageModelCommand saveCommand, IStorageModel storage, IResponseWriter writer)
		{
			_saveCommand = saveCommand;
			_storage = storage;
			_writer = writer;
		}

		public bool CanHandle(ServerInputModel input)
		{
			return input.AddFlag;
		}

		public bool Execute(ServerInputModel input)
		{
			if (_storage.IsRegistered(input.Name))
			{
				_writer.Write("Server {0} ({1}) is already registered.", input.Name, input.Url);
				return false;
			}

			_storage.Register(input.Name, new Uri(input.Url));
			_saveCommand.Execute(_storage);

			return true;
		}
	}
}