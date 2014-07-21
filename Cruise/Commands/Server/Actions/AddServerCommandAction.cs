using System;
using Cruise.Infrastructure;
using Cruise.Storage;

namespace Cruise.Commands.Server.Actions
{
	public class AddServerCommandAction : IServerCommandAction
	{
		private readonly ISaveStorageModelCommand _saveCommand;
		private readonly IConfigurationModel _configuration;
		private readonly IResponseWriter _writer;

		public AddServerCommandAction(ISaveStorageModelCommand saveCommand, IConfigurationModel configuration, IResponseWriter writer)
		{
			_saveCommand = saveCommand;
			_configuration = configuration;
			_writer = writer;
		}

		public bool CanHandle(ServerInputModel input)
		{
			return input.AddFlag;
		}

		public bool Execute(ServerInputModel input)
		{
			if (_configuration.IsRegistered(input.Name))
			{
				_writer.Write(new ServerAlreadyRegisteredViewModel(input.Name, input.Url));
				return false;
			}

			_configuration.Register(input.Name, new Uri(input.Url));
			_saveCommand.Execute(_configuration);

			return true;
		}
	}
}
