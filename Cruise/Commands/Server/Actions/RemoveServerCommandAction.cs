using Cruise.Storage;

namespace Cruise.Commands.Server.Actions
{
	public class RemoveServerCommandAction : IServerCommandAction
	{
		private readonly ISaveStorageModelCommand _saveCommand;
		private readonly IConfigurationModel _configuration;

		public RemoveServerCommandAction(ISaveStorageModelCommand saveCommand, IConfigurationModel configuration)
		{
			_saveCommand = saveCommand;
			_configuration = configuration;
		}

		public bool CanHandle(ServerInputModel input)
		{
			return input.RemoveFlag;
		}

		public bool Execute(ServerInputModel input)
		{
			if (_configuration.IsRegistered(input.Name))
			{
				_configuration.UnRegister(input.Name);
				_saveCommand.Execute(_configuration);
			}

			return true;
		}
	}
}