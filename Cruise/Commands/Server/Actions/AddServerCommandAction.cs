using System;
using Cruise.Infrastructure;
using Cruise.Storage;

namespace Cruise.Commands.Server.Actions
{
	public class AddServerCommandAction : IServerCommandAction
	{
		private readonly ISaveStorageModelCommand _saveCommand;
		private readonly IConfigurationModel _storage;
		private readonly IResponseWriter _writer;

		public AddServerCommandAction(ISaveStorageModelCommand saveCommand, IConfigurationModel storage, IResponseWriter writer)
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
				_writer.Write(new ServerAlreadyRegisteredViewModel(input.Name, input.Url));
				return false;
			}

			_storage.Register(input.Name, new Uri(input.Url));
			_saveCommand.Execute(_storage);

			return true;
		}
	}
}
