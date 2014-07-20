using Cruise.Storage;

namespace Cruise.Commands.Server.Actions
{
	public class RemoveServerCommandAction : IServerCommandAction
	{
		private readonly ISaveStorageModelCommand _saveCommand;
		private readonly IStorageModel _storage;

		public RemoveServerCommandAction(ISaveStorageModelCommand saveCommand, IStorageModel storage)
		{
			_saveCommand = saveCommand;
			_storage = storage;
		}

		public bool CanHandle(ServerInputModel input)
		{
			return input.RemoveFlag;
		}

		public bool Execute(ServerInputModel input)
		{
			if (_storage.IsRegistered(input.Name))
			{
				_storage.UnRegister(input.Name);
				_saveCommand.Execute(_storage);
			}

			return true;
		}
	}
}